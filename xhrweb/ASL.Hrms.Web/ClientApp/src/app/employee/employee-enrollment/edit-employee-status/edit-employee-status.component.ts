import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeStatusHistory } from 'src/app/shared/models/employee/employee-status-history';
import { EmployeeStatusHistoryService } from 'src/app/shared/services/employee/employee-status-history.service';
import { CommonLookUpTypeService, EmployeeStatusService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { DatePipe } from '@angular/common';
import { AuthService } from '../../../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-employee-status',
  templateUrl: './edit-employee-status.component.html',
  styleUrls: ['./edit-employee-status.component.css']
})

export class EditEmployeeStatusComponent implements OnInit {
  onEmployeeStatusHistoryCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeStatusHistoryEditEvent: EventEmitter<any> = new EventEmitter();
  employeeFullName: any;
  employeeStatusHistoryCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = [];
  employeeStatuses: any;
  employeeStatusHistory: EmployeeStatusHistory;
  isEditMode = false;
  companyId: any;
  statusId: any;
  isFormValid: boolean;
  errorMessages: any ;
  loading: boolean = false;
  constructor(
    private dialogRef: MatDialogRef<EditEmployeeStatusComponent>,
    private formBuilder: FormBuilder,
    private employeeStatusHistoryService: EmployeeStatusHistoryService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private employeeStatusService: EmployeeStatusService,
    private alertService: AlertService,
    private authService: AuthService,
    private datePipe: DatePipe,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeStatusHistory = new EmployeeStatusHistory();
    if (isNaN(data)) {
      this.employeeStatusHistory = new EmployeeStatusHistory(data);
      this.statusId = this.employeeStatusHistory.statusId;
      this.employeeId = this.employeeStatusHistory.employeeId;
    }

    if (this.employeeStatusHistory.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
  }

  ngOnInit() {
    if (!this.authService.checkAuthenticated()) {
      this.router.navigateByUrl('/auth/login');
    }
    //
    this.setLoggedInUserInformation();
    this.getAllEmployeeStatuses();
    this.statusId = this.employeeStatusHistory.statusId;
    this.buildForm();
  }


  getAllEmployeeStatuses() {
    this.employeeStatusService.getAllEmployeeStatusByCompanyId(this.companyId).subscribe(result => {
      this.employeeStatuses = result;
      
    });
  }
  setLoggedInUserInformation() {
    var loggedInUserInfo = this.authService.getLoggedInUserInfo();

    this.employeeFullName = loggedInUserInfo.displayName;
    this.companyId = loggedInUserInfo.companyId;

    //this.employeeId = employeeId;//loggedInUserInfo.employeeId;
    //this.employeeEnrollment.employeeId = employeeId;// loggedInUserInfo.employeeId;
    //this.imagePreviewPath

  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }


  buildForm() {
    this.employeeStatusHistoryCreateForm = this.formBuilder.group({
      statusId: [this.employeeStatusHistory.statusId],
      remarks: [this.employeeStatusHistory.remarks, [Validators.required,  Validators.maxLength(150)]]
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeeStatusHistoryCreateForm.invalid) {
      return;
    }
    if (this.employeeStatusHistory.id === undefined) {
      this.createEmployeeStatusHistory();
    }
    else {
      this.editEmployeeStatusHistory();
    }
    this.dialogRef.close();
  }
 
  createEmployeeStatusHistory() {
    this.submitted = true;
    if (this.employeeStatusHistoryCreateForm.invalid) {
      return;
    }

    this.employeeStatusHistory = new EmployeeStatusHistory(this.employeeStatusHistoryCreateForm.value);
    this.employeeStatusHistory.employeeId = this.employeeId;
    this.loading = true;
    this.employeeStatusHistoryService.createEmployeeStatusHistory(this.employeeStatusHistory).subscribe((data: any) => {

      if(data.status === true){
      this.onEmployeeStatusHistoryCreateEvent.emit(this.employeeStatusHistory.employeeId);
      this.alertService.success("Employee Status History added successfully");
      this.isFormValid = true;
      }
      else{
        this.isFormValid = false;
        this.errorMessages = data.message;
      }
      this.loading = false;

    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;

    });
  }


  editEmployeeStatusHistory() {
    let newData = new EmployeeStatusHistory(this.employeeStatusHistoryCreateForm.value);
    if (this.employeeStatusHistory !== null) {
      this.employeeStatusHistory.employeeId = localStorage.getItem('employeeId');
      
      this.employeeStatusHistory.remarks = newData.remarks;
      this.loading = true;
      this.employeeStatusHistoryService.editEmployeeStatusHistory(this.employeeStatusHistory).subscribe((data: any) => {
        
        this.onEmployeeStatusHistoryEditEvent.emit(this.employeeStatusHistory.id)
        if(data.status === true){
          this.alertService.success("Employee Status History edited successfully");
          this.isFormValid = true;
          }
          else{
            this.isFormValid = false;
            this.errorMessages = data.message;
          }
        this.loading = false
      }, (error: any) => {
        this.showErrorMessage(error);
        this.loading = false

      });
    }
  }

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }
  get f() { return this.employeeStatusHistoryCreateForm.controls; }
}



