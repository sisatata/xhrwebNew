import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TaskCategory, TaskDetail, TaskDetailLog } from 'src/app/shared/models';
import { EmployeeService, TaskDetailLogService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateTaskCategoryComponent } from '../../task-category/create-task-category/create-task-category.component';
@Component({
  selector: 'app-create-task-detail-log',
  templateUrl: './create-task-detail-log.component.html',
  styleUrls: ['./create-task-detail-log.component.css']
})
export class CreateTaskDetailLogComponent implements OnInit {
  onTaskDetailLogCreateEvent: EventEmitter<any> = new EventEmitter();
  onTaskDetailLogEditEvent: EventEmitter<any> = new EventEmitter();
  taskDetailLogCreateForm: FormGroup;
  submitted: boolean;
  isEditMode: boolean;
  isFormValid: boolean;
  incomeTaxPayerTypes: any[] = [];
  errorMessages: any;
  loading: boolean;
  companies: any;
  isDateOverLap: boolean;
  dateOverLapErrorMessage: string = 'Dates overlapping for parameter name';
  taskCategory: TaskCategory;
  taskDetail: TaskDetail;
  companyId: any;
  taskCategories: TaskCategory[];
  taskDetailLogs: TaskDetailLog[];
  employees: any;
  taskDetails: TaskDetail[];
  employeeId: any;
  startDate: any;
  endDate: any;
  statuses: any = [{ id: 1, value: 'In queue' }, { id: 0, value: 'In progress' }, { id: 2, value: 'Done' }];
  statusId: any;
  taskDetailLog: TaskDetailLog;
  errorMessage: any;
  taskDetailLogId:any;
  constructor(
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateTaskCategoryComponent>,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private employeeService: EmployeeService,
    private taskDetailLogService: TaskDetailLogService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.taskDetailLog = new TaskDetailLog();
    if (isNaN(data)) {
      this.taskDetailLog = new TaskDetailLog(data);
    }
    if (this.taskDetailLog.id) {
      this.isEditMode = true;
      this.taskDetailLogId = this.taskDetailLog.id;
    }
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllEmployees();
  }
  buildForm(): void {
    this.taskDetailLogCreateForm = this.formBuilder.group({
      employeeId: [this.taskDetailLog.employeeId],
      dateUpdated: [this.taskDetailLog.dateUpdated],
      spendTime: [this.taskDetailLog.spendTime],
      taskDetailId: [this.taskDetailLog.taskDetailId, [Validators.required]],
      startDate: [this.taskDetailLog.startDate, [Validators.required]],
      endDate: [this.taskDetailLog.endDate, [Validators.required]],
      updateInfo: [this.taskDetailLog.updateInfo, [Validators.maxLength(50)]],
    });
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  get f() { return this.taskDetailLogCreateForm.controls; }
  onSubmit(): void {
    let userId = localStorage.getItem('userId');
    this.submitted = true;
    if (this.taskDetailLogCreateForm.invalid)
      return
    if(this.taskDetailLog.id === null || this.taskDetailLog.id === undefined){
      this.createTaskDetailLog();
    }
    else
    this.editTaskDetailLog();
  }
  close(): void {
    this.dialogRef.close();
  }
  createTaskDetailLog(): void {
    this.loading = true;
    this.taskDetailLog = new TaskDetailLog(this.taskDetailLogCreateForm.value);
    this.taskDetailLog.startDate = this.datePipe.transform(new Date(this.taskDetailLog.startDate), 'yyyy-MM-dd');
    this.taskDetailLog.endDate = this.datePipe.transform(new Date(this.taskDetailLog.endDate), 'yyyy-MM-dd');
    this.taskDetailLogService.createTaskDetailLogByTaskDetailId(this.taskDetailLog).subscribe(result => {
      this.loading = false;
      this.onTaskDetailLogCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Task detail log created successfully');
        
       
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessage = (result as any).message;
      }
    }, () => {
      this.loading = false;
    })
  }
  editTaskDetailLog(): void {
    this.loading = false;
    let newData = new TaskDetailLog(this.taskDetailLogCreateForm.value);
    this.taskDetailLog = Object.assign({}, newData);
    this.taskDetailLog.id = this.taskDetailLogId;
    this.taskDetailLog.startDate = this.datePipe.transform(new Date(this.taskDetailLog.startDate), 'yyyy-MM-dd');
    this.taskDetailLog.endDate = this.datePipe.transform(new Date(this.taskDetailLog.endDate), 'yyyy-MM-dd');
   
    this.taskDetailLogService.editTaskDetailLog(this.taskDetailLog).subscribe(result => {
      this.onTaskDetailLogEditEvent.emit((result as any).id);
      this.loading = false;
      if ((result as any).status === true) {
        this.alertService.success('Task detail log edited successfully');
        this.isFormValid = true;
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (result as any).message;
      }
    }, () => {
      this.loading = false;
    })
  }
}
