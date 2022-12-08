import { DatePipe } from '@angular/common';
import { Component, Injector, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { Guid, SalaryStructure, TaskDetail, TaskDetailExport, TaskDetailLog } from 'src/app/shared/models';
import { CompanyService, TaskDetailLogService, TaskDetailService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateTaskDetailChildComponent } from './create-task-detail-child/create-task-detail-child.component';
import { CreateTaskDetailLogComponent } from './create-task-detail-log/create-task-detail-log.component';
import { CreateTaskDetailComponent } from './create-task-detail/create-task-detail.component';
@Component({
  selector: 'app-task-detail',
  templateUrl: './task-detail.component.html',
  styleUrls: ['./task-detail.component.css']
})
export class TaskDetailComponent extends BaseAuthorizedComponent implements OnInit {
  companies: any;
  companyId: any;
  loading: boolean;
  taskCategories: any[];
  taskDetailExport: TaskDetailExport = new TaskDetailExport();
  submitted: boolean;
  @Input() isLoading: any;
  @Input() data: any;
  taskDetailFilterFormGroup: FormGroup
  salarystructures: SalaryStructure[];
  salarystructureId: any;
  childList: any = [];
  hideme = [];
  currentData: any;
  Index: any;
  isInitialCall: boolean = true;
  tasksDetail: TaskDetail[];
  showAddButton: boolean = true;
  taskDetailLogs: any;
  hideDetailLogs: any[] = [];
  statuses: any = ['In progress', 'In queue', 'Done'];
  employeeId: string;
  userId: string;
  isAdmin:any;
  endDate: any;
  stardDate: any;
  
  constructor(
    injector: Injector,
    private companyService: CompanyService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private taskDetailLogService: TaskDetailLogService,
    private taskDetailService: TaskDetailService,
    private datePipe: DatePipe,
  ) { super(injector);
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.userId = this.authService.getLoggedInUserInfo().userId;
    this.isAdmin = this.authService.isAdmin;
    this.buildForm();
   // this.employeeId = this.isAdmin === true ? Guid.empty: this.employeeId;
  }
  ngOnInit(): void {
    this.tasksDetail = this.data;
    this.setDates();
    if (this.tasksDetail != undefined) {
      this.isInitialCall = false;
    }
    this.getAllCompanies();
    //this.getAllTaskDetails();
   
    if (this.isInitialCall) {
      //this.getAllParentTasksDetailByCompany();
      this.isInitialCall = false;
      this.getAllParentTasksDetailByCompany();
    }
    
    
  }
  getAllCompanies(): void {
    this.loading = true;
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  getAllTaskDetails(): void {
  }
  buildForm() {
    this.taskDetailFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      startDate: [this.stardDate, Validators.required],
      endDate: [this.endDate, Validators.required]
    });
  }
  get f() { return this.taskDetailFilterFormGroup.controls; }
  public showChildList(index, taskDetailId) {
    this.showAddButton = true;
    if (taskDetailId) {
      this.getAllChildTaskDetail(index, taskDetailId);
    }
  }
  createTaskDetailChild(taskDetail: TaskDetail): void {
    const createTaskDetaiChildlDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createTaskDetaiChildlDialogConfig.disableClose = true;
    createTaskDetaiChildlDialogConfig.autoFocus = true;
    createTaskDetaiChildlDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createTaskDetaiChildlDialogConfig.width = `${dialogWidth}%`;
    var newTaskDetail = new TaskDetail();
    newTaskDetail.companyId = this.companyId;
    newTaskDetail.parentTaskId = taskDetail.id;
    //console.log( newTaskDetail.parentTaskId)
    newTaskDetail.assigneeId = taskDetail.assigneeId;
    newTaskDetail.taskTypeId = taskDetail.taskTypeId;
    createTaskDetaiChildlDialogConfig.data = newTaskDetail;
    const createTaskDetailChildDialog = this.dialog.open(CreateTaskDetailChildComponent, createTaskDetaiChildlDialogConfig);
    createTaskDetailChildDialog.afterClosed().subscribe(() => {
      this.refreshPage();
    });
  }
  editTaskDetailChild(taskDetail: TaskDetail): void {
    const editTaskDetaiChildlDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editTaskDetaiChildlDialogConfig.disableClose = true;
    editTaskDetaiChildlDialogConfig.autoFocus = true;
    editTaskDetaiChildlDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editTaskDetaiChildlDialogConfig.width = `${dialogWidth}%`;
    taskDetail.companyId = this.companyId;
    editTaskDetaiChildlDialogConfig.data = taskDetail;
    const editTaskDetailChildDialog = this.dialog.open(CreateTaskDetailChildComponent, editTaskDetaiChildlDialogConfig);
    editTaskDetailChildDialog.afterClosed().subscribe(() => {
      this.refreshPage();
    });
  }
  getAllParentTasksDetailByCompany(): void {
    this.loading = true;
  // this.employeeId = this.isAdmin === true? Guid.empty: this.employeeId;

  let stDate = this.datePipe.transform(new Date( localStorage.getItem('taskStartDate')), 'MM-dd-yyyy');
  let edDate = this.datePipe.transform(new Date(localStorage.getItem('taskEndDate')), 'MM-dd-yyyy');
 this.submitted = true;
    this.taskDetailService.getAllParentTasksDetailByCompany(this.employeeId, stDate, edDate,this.userId,).subscribe(result => {
      this.totalItems = 0;
      this.tasksDetail = result;
      this.totalItems = this.tasksDetail.length;
      this.generateTotalItemsText();
    
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  getAllChildTaskDetail(index: any, taskDetailId: any) {
    if (this.companyId) {
      this.loading = true;
      this.taskDetailService.getAllTasksDetailByCompanyAndParent(taskDetailId).subscribe(result => {
        this.currentData = result;
        this.hideme[index] = !this.hideme[index];
        this.Index = index;
        this.tasksDetail[index].listTaskDetail = [];
        if (this.tasksDetail[index].listTaskDetail == null || this.tasksDetail[index].listTaskDetail == undefined) {
          this.tasksDetail[index].listTaskDetail = new Array();
        }
        if (this.currentData != undefined || this.currentData != null) {
          this.tasksDetail[index].listTaskDetail = this.currentData;
        }
        this.loading = false;
      }, () => {
        this.loading = false;
      })
    }
  }
  getTaskDetailListExcel():void{
    this.taskDetailExport.employeeId = this.employeeId;
    this.taskDetailExport.userId = this.userId;
    this.taskDetailExport.startDate = this.datePipe.transform(new Date( localStorage.getItem('taskStartDate')), 'MM-dd-yyyy');
    this.taskDetailExport.endDate = this.datePipe.transform(new Date(localStorage.getItem('taskEndDate')), 'MM-dd-yyyy');
    this.taskDetailExport.employeeId = this.employeeId;
    this.taskDetailExport.userId = this.userId;
    this.loading = true;
    this.taskDetailService.getTaskListExcel(this.taskDetailExport).subscribe(res=>{
      this.generateExcel(res, 'TaskList-Report');
      this.loading = false;
      this.submitted = false;
    },()=>{
      this.loading = false;
      this.submitted = false;
    });
    //this.taskDetailExport.startDate = 
  }
  createTaskDetail(): void {
    const createTaskDetailDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createTaskDetailDialogConfig.disableClose = true;
    createTaskDetailDialogConfig.autoFocus = true;
    createTaskDetailDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createTaskDetailDialogConfig.width =  `${dialogWidth}%`;
    var taskDetail = new TaskDetail();
    taskDetail.companyId = this.companyId;
    createTaskDetailDialogConfig.data = taskDetail;
    const createTaskDetailDialog = this.dialog.open(CreateTaskDetailComponent, createTaskDetailDialogConfig);
    createTaskDetailDialog.afterClosed().subscribe(() => {
      this.refreshPage();
    });
  }
  editTaskDetail(taskDetail: TaskDetail): void {
    const editTaskDetailDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editTaskDetailDialogConfig.disableClose = true;
    editTaskDetailDialogConfig.autoFocus = true;
    editTaskDetailDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editTaskDetailDialogConfig.width = `${dialogWidth}%`;
    taskDetail.companyId = this.companyId;
    editTaskDetailDialogConfig.data = taskDetail;
    const editTaskDetailDialog = this.dialog.open(CreateTaskDetailComponent, editTaskDetailDialogConfig);
    editTaskDetailDialog.afterClosed().subscribe(() => {
      this.refreshPage();
    });
  }
  onDeleteTaskDetail(taskDetail: TaskDetail): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the task detail " + taskDetail.taskName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.deleteTaskDetail(taskDetail);
      }
    });
  }
  deleteTaskDetail(taskDetail: TaskDetail): void {
    this.taskDetailService.deleteTaskDetail(taskDetail).subscribe(() => {
      this.alertService.success('Task Detail successfully deleted');
      this.refreshPage();
    })
  }
  showTaskDetailLogs(index: any, taskDetailId: any): void {
    this.loading = true;
    this.taskDetailLogService.getAllTaskDetailLogsByTaskDetailId(taskDetailId).subscribe(result => {
      this.taskDetailLogs = result;
      this.childList[index] = result;
      this.hideDetailLogs[index] = !this.hideDetailLogs[index];
      //this.refreshPage();
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  createTaskDetailLog(taskDetail: TaskDetail): void {
    const createTaskDetailLogDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createTaskDetailLogDialogConfig.disableClose = true;
    createTaskDetailLogDialogConfig.autoFocus = true;
    createTaskDetailLogDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createTaskDetailLogDialogConfig.width = `${dialogWidth}%`;
    var taskDetailLog = new TaskDetailLog();
    taskDetailLog.taskDetailId = taskDetail.id;
    createTaskDetailLogDialogConfig.data = taskDetailLog;
    const createTaskDetailLogDialog = this.dialog.open(CreateTaskDetailLogComponent, createTaskDetailLogDialogConfig);
    createTaskDetailLogDialog.afterClosed().subscribe(() => {
      this.refreshPage();
    });
  }
  editTaskDetailLog(taskDetailLog: TaskDetailLog): void {
    const editTaskDetailLogDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editTaskDetailLogDialogConfig.disableClose = true;
    editTaskDetailLogDialogConfig.autoFocus = true;
    editTaskDetailLogDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editTaskDetailLogDialogConfig.width = `${dialogWidth}%`;
    editTaskDetailLogDialogConfig.data = taskDetailLog;
    const editTaskDetailLogDialog = this.dialog.open(CreateTaskDetailLogComponent, editTaskDetailLogDialogConfig);
    editTaskDetailLogDialog.afterClosed().subscribe(() => {
      this.refreshPage();
    });
  }
  refreshPage(): void {
    this.router.navigate(['/task/task-detail-base',], { state: { taskDetailPage: 'taskDetail' } });
  }
 
  setDates(): void {
    var oneMonthAgoDate = new Date();
    var oneDayAgoDate = new Date();
    var today = new Date();
    oneDayAgoDate.setDate(oneDayAgoDate.getDate() - 1);
    oneMonthAgoDate.setMonth(oneMonthAgoDate.getMonth() - 1);
    this.f.startDate.setValue(oneMonthAgoDate);
    this.f.endDate.setValue(today);
  }
  generateExcel(result: any, fileName: any): void {
    if (window.navigator.msSaveOrOpenBlob) {
      window.navigator.msSaveBlob(result.body, fileName);
    } else {
      const link = window.URL.createObjectURL(result.body);
      const a = document.createElement('a');
      document.body.appendChild(a);
      a.setAttribute('style', 'display: none');
      a.href = link;
      a.download = fileName;
      a.click();
      window.URL.revokeObjectURL(link);
      a.remove();
    }
  }
}
