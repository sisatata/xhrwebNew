import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TaskCategory } from 'src/app/shared/models';
import { CompanyService, TaskCategoryService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-task-category',
  templateUrl: './create-task-category.component.html',
  styleUrls: ['./create-task-category.component.css']
})
export class CreateTaskCategoryComponent implements OnInit {
  onTaskCategoryCreateEvent: EventEmitter<any> = new EventEmitter();
  onTaskCategoryEditEvent: EventEmitter<any> = new EventEmitter();
  taskCategoryCreateForm: FormGroup;
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
  constructor(
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateTaskCategoryComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private datePipe: DatePipe,
    private taskCategoryService: TaskCategoryService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.taskCategory = new TaskCategory();
    if (isNaN(data)) {
      this.taskCategory = new TaskCategory(data);
    }
    if (this.taskCategory.id) {
      this.isEditMode = true;
      this.buildForm();
    }
  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
  }
  buildForm(): void {
    this.taskCategoryCreateForm = this.formBuilder.group({
      companyId: [this.taskCategory.companyId],
      taskCategoryName: [this.taskCategory.taskCategoryName, [Validators.required, Validators.maxLength(50)]],
      remarks: [this.taskCategory.remarks, [Validators.maxLength(50)]],
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
  get f() { return this.taskCategoryCreateForm.controls; }
  onSubmit(): void {
    this.submitted = true;
    if (this.taskCategoryCreateForm.invalid) {
      return;
    }
    else if (this.taskCategory.id === null || this.taskCategory.id === undefined)
      this.createTaskCategory();
    else {
      this.editTaskCategory();
    }
  }
  close(): void {
    this.dialogRef.close();
  }
  createTaskCategory(): void {
    this.taskCategory = new TaskCategory(this.taskCategoryCreateForm.value);
    this.loading = true;
    this.taskCategoryService.createTaskCategory(this.taskCategory).subscribe(result => {
      this.onTaskCategoryCreateEvent.emit((result as any).id);
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
  editTaskCategory(): void {
    let newData = new TaskCategory(this.taskCategoryCreateForm.value);
    if (this.taskCategory !== null) {
      this.taskCategory.remarks = newData.remarks;
      this.taskCategory.taskCategoryName = newData.taskCategoryName;
      this.taskCategoryService.editTaskCategory(this.taskCategory).subscribe(result => {
        this.onTaskCategoryEditEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('Task category successfully edited');
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
  }
}
