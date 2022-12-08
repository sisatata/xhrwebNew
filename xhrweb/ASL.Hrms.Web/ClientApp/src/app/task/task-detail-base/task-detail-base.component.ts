import { ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { TaskDetail } from 'src/app/shared/models';
import { CompanyService } from 'src/app/shared/services';
import { CreateTaskDetailComponent } from '../task-detail/create-task-detail/create-task-detail.component';
import {TaskDetailComponent} from '../task-detail/task-detail.component'
@Component({
  selector: 'app-task-detail-base',
  providers: [TaskDetailComponent],
  templateUrl: './task-detail-base.component.html',
  styleUrls: ['./task-detail-base.component.css']
})
export class TaskDetailBaseComponent implements OnInit {
companyId:any = localStorage.getItem('companyId');
@ViewChild(TaskDetailComponent) child:TaskDetailComponent;
taskDetaiBsaelFilterFormGroup: FormGroup;
submitted:boolean=false;
  stardDate: any;
  endDate: any;
  companies: any;
  constructor(
    private dialog: MatDialog,
    private router: Router,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private taskDetailComponent : TaskDetailComponent
  ) { 
    let isFromTakDetailPage = this.router.getCurrentNavigation();
    if(isFromTakDetailPage.extras.state){
         this.router.navigate(['/task/task-detail']);
    }
    this.buildForm();
    this.getAllCompanies();
  }
  ngOnInit(): void {
  this.setDates();
  }
  buildForm() {
    this.taskDetaiBsaelFilterFormGroup = this.formBuilder.group({
      companyId:[this.companyId],
      startDate: [this.stardDate, Validators.required],
      endDate: [this.endDate, Validators.required]
    });
  }
  get f() { return this.taskDetaiBsaelFilterFormGroup.controls; }
  createTaskDetail(): void {
    const createTaskDetailDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createTaskDetailDialogConfig.disableClose = true;
    createTaskDetailDialogConfig.autoFocus = true;
    createTaskDetailDialogConfig.panelClass = 'xHR-dialog';
    createTaskDetailDialogConfig.width = '50%';
    var taskDetail = new TaskDetail();
    taskDetail.companyId = this.companyId;
    createTaskDetailDialogConfig.data = taskDetail;
    const createIncomeTaxParameterDialog = this.dialog.open(CreateTaskDetailComponent, createTaskDetailDialogConfig);
    const successfullCreate = createIncomeTaxParameterDialog.componentInstance.onTaskDetailCreateEvent.subscribe((data) => {
      this.taskDetailComponent.refreshPage();
    
    });
    createIncomeTaxParameterDialog.afterClosed().subscribe(() => {
    });
  }
  sendValue():void{
    this.submitted = true;
    var dates = this.taskDetaiBsaelFilterFormGroup.value;
    if(dates.startDate !== null && dates.endDate !== null){
      localStorage.setItem('taskStartDate', dates.startDate);
      localStorage.setItem('taskEndDate', dates.endDate);
      this.child.getAllParentTasksDetailByCompany();
    }
    
  }
  exportTaskList():void{
    this.submitted = true;
    var dates = this.taskDetaiBsaelFilterFormGroup.value;
    if(dates.startDate !== null && dates.endDate !== null){
      localStorage.setItem('taskStartDate', dates.startDate);
      localStorage.setItem('taskEndDate', dates.endDate);
      this.child.getTaskDetailListExcel();
    }
  }
   setDates(): void {
    var oneMonthAgoDate = new Date();
    var oneDayAgoDate = new Date();
    var today = new Date();
    oneDayAgoDate.setDate(oneDayAgoDate.getDate() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    const stDate = localStorage.getItem('taskStartDate');
    const edDate = localStorage.getItem('taskEndDate');
    if(localStorage.getItem('taskStartDate') && localStorage.getItem('taskEndDate')){
    this.f.startDate.setValue(new Date(stDate));
    this.f.endDate.setValue(new Date(edDate));
   
    }
    else{
      this.f.startDate.setValue(oneMonthAgoDate);
      this.f.endDate.setValue(oneDayAgoDate);
    }
  }
  getAllCompanies(): void {
   
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
     
    }, () => {
      
    })
  }
}
