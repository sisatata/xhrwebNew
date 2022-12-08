import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AuthService } from 'src/app/auth/services/auth.service';
import { TaskCategory, TaskDetail } from 'src/app/shared/models';
import { CompanyService, EmployeeService, TaskCategoryService, TaskDetailService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateTaskCategoryComponent } from '../../task-category/create-task-category/create-task-category.component';
@Component({
  selector: 'app-create-task-detail-child',
  templateUrl: './create-task-detail-child.component.html',
  styleUrls: ['./create-task-detail-child.component.css']
})
export class CreateTaskDetailChildComponent implements OnInit {
  onTaskDetailChildCreateEvent: EventEmitter<any> = new EventEmitter();
  onTaskDetailChildEditEvent: EventEmitter<any> = new EventEmitter();
  taskDetailChildCreateForm: FormGroup;
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
  employees: any;
  taskDetails: TaskDetail[];
  employeeId: any;
  endDate: any;
  statuses: any = [{ id: 1, value: 'In queue' }, { id: 0, value: 'In progress' }, { id: 2, value: 'Done' }];
  statusId: any;
  userId: string;
  constructor(
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateTaskCategoryComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private taskCategoryService: TaskCategoryService,
    private taskDetailService: TaskDetailService,
    private authService: AuthService,
     private datePipe: DatePipe,
    private employeeService: EmployeeService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.taskDetail = new TaskDetail();
    if (isNaN(data)) {
      this.taskDetail = new TaskDetail(data);
      
    }
    if (this.taskDetail.id) {
      this.isEditMode = true;
      this.buildForm();
    }
    this.userId = this.authService.getLoggedInUserInfo().userId;
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllTaskCategories();
    this.getAllEmployees();
   // this.getAllTasksDetailByCompany();
    this.getAllTasksDetailByCompany();
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  getAllTasksDetailByCompany(): void {
    this.loading = true;
    // getAllTasksDetailByCompanyId return all tasks detail 
    this.taskDetailService.getAllTasksDetailByCompanyId(this.taskDetail.companyId).subscribe(result => {
      this.taskDetails = result
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  buildForm(): void {
    this.taskDetailChildCreateForm = this.formBuilder.group({
      companyId: [this.taskDetail.companyId],
      taskTypeId: [this.taskDetail.taskTypeId, [Validators.required, Validators.maxLength(50)]],
      taskName: [this.taskDetail.taskName, [Validators.required, Validators.maxLength(50)]],
      taskDescription: [this.taskDetail.taskDescription, [Validators.maxLength(150)]],
      startDate: [this.taskDetail.startDate, [Validators.required]],
      endDate: [this.taskDetail.endDate, [Validators.required]],
      assigneeId: [this.taskDetail.assigneeId],
      parentTaskId: [this.taskDetail.parentTaskId],
      statusId: [this.taskDetail.statusId],
      progress: [this.taskDetail.progress]
    });
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
  get f() { return this.taskDetailChildCreateForm.controls; }
  onSubmit(): void {
    this.submitted = true;
    if (this.taskDetailChildCreateForm.invalid) {
      return;
    }
    else if (this.taskDetail.id === null || this.taskDetail.id === undefined)
      this.createTaskDetail();
    else {
      this.editTaskDetail();
    }
  }
  close(): void {
    this.dialogRef.close();
  }
  createTaskDetail(): void {
    this.taskDetail = new TaskDetail(this.taskDetailChildCreateForm.value);
    this.loading = true;
    this.taskDetail.startDate = this.datePipe.transform(new Date(this.taskDetail.startDate), 'yyyy-MM-dd');
    this.taskDetail.endDate = this.datePipe.transform(new Date(this.taskDetail.endDate), 'yyyy-MM-dd');
    this.taskDetail.taskCreator = this.userId;
    this.taskDetailService.createTaskCategory(this.taskDetail).subscribe(result => {
      this.onTaskDetailChildCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Task category successfully created');
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  editTaskDetail(): void {
    let newData = new TaskDetail(this.taskDetailChildCreateForm.value);
    let id = this.taskDetail.id;
    this.loading = true;
    this.taskDetail = Object.assign({}, newData);
    this.taskDetail.id = id;
    this.taskDetail.startDate = this.datePipe.transform(new Date(this.taskDetail.startDate), 'yyyy-MM-dd');
    this.taskDetail.endDate = this.datePipe.transform(new Date(this.taskDetail.endDate), 'yyyy-MM-dd');
    this.taskDetailService.editTaskDetail(this.taskDetail).subscribe(result => {
      this.onTaskDetailChildEditEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Task detail successfully edited');
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  getAllTaskCategories(): void {
    this.loading = true;
    this.taskCategoryService.getAllTaskCategory(this.taskDetail.companyId).subscribe(result => {
      this.taskCategories = result;
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  onChangeEmployee(data: any): void {
    this.taskDetail.assigneeId = data.value;
  }
  onChangeParentTask(data: any): void {
    this.taskDetail.parentTaskId = data.value;
  }
}
