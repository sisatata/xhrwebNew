import { Component, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { TaskCategory } from 'src/app/shared/models';
import { CompanyService, TaskCategoryService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateTaskCategoryComponent } from './create-task-category/create-task-category.component';

@Component({
  selector: 'app-task-category',
  templateUrl: './task-category.component.html',
  styleUrls: ['./task-category.component.css']
})
export class TaskCategoryComponent extends BaseAuthorizedComponent implements OnInit {
  taskCategoryFilterFormGroup: FormGroup;
  taskCategory: TaskCategory;
  companies: any;
  companyId: any;
  loading: boolean;
  taskCategories: any[];
 submitted: boolean;
  constructor(private injector: Injector,
    private companyService: CompanyService,
    private taskCategoryService: TaskCategoryService,
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
  ) {

    super(injector);
  }

  ngOnInit(): void {
    this.getAllCompanies();
    this.buildForm();
    this.getAllTaskCategories();
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
  get f() { return this.taskCategoryFilterFormGroup.controls; }
  buildForm() {
    this.taskCategoryFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      
    });
  }
  getAllTaskCategories():void{
    this.loading = true;
    this.taskCategoryService.getAllTaskCategory(this.companyId).subscribe(result=>{
        this.taskCategories = result;
        this.loading = false;      
       
    },()=>{
      this.loading = false;
    })
  }
  createTaskCategory():void{
    const createTaskCategoryDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createTaskCategoryDialogConfig.disableClose = true;
    createTaskCategoryDialogConfig.autoFocus = true;
    createTaskCategoryDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createTaskCategoryDialogConfig.width = `${dialogWidth}%`;
    var taskCategory = new TaskCategory();
    taskCategory.companyId = this.companyId;
    createTaskCategoryDialogConfig.data = taskCategory;
    const createIncomeTaxParameterDialog = this.dialog.open(CreateTaskCategoryComponent, createTaskCategoryDialogConfig);
    const successfullCreate = createIncomeTaxParameterDialog.componentInstance.onTaskCategoryCreateEvent.subscribe((data) => {
      this.getAllTaskCategories();
    });
    createIncomeTaxParameterDialog.afterClosed().subscribe(() => {
    });
  }

  editTaskCategory(taskCategory:TaskCategory):void{
    const editTaskCategoryDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editTaskCategoryDialogConfig.disableClose = true;
    editTaskCategoryDialogConfig.autoFocus = true;
    editTaskCategoryDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editTaskCategoryDialogConfig.width = `${dialogWidth}%`;
    taskCategory.companyId = this.companyId;
    editTaskCategoryDialogConfig.data = taskCategory;
    const editTasCategoryDialog = this.dialog.open(CreateTaskCategoryComponent, editTaskCategoryDialogConfig);
    const successfullCreate = editTasCategoryDialog.componentInstance.onTaskCategoryEditEvent.subscribe((data) => {
      this.getAllTaskCategories();
    });
    editTasCategoryDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteTaskCategory(taskCategory: TaskCategory): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the task category " +taskCategory.taskCategoryName;  
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result !== undefined) {
        this.deleteTaskCategory(taskCategory);
      }
    });
  }
  deleteTaskCategory(taskCategory: TaskCategory):void{
    this.taskCategoryService.deleteTaskCategory(taskCategory).subscribe(result=>{
          this.alertService.success('Task Category successfully deleted');
    })
  }
  

}
