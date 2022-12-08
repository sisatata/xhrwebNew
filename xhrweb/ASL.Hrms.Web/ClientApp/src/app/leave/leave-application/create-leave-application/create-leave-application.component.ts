import { Component, OnInit, EventEmitter, Inject, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LeaveApplication } from 'src/app/shared/models';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyService, LeaveApplicationService, EmployeeService, LeaveTypeService, BranchService } from 'src/app/shared/services';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { DatePipe } from '@angular/common';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { timeStamp } from 'console';


@Component({
  selector: 'app-create-leave-application',
  templateUrl: './create-leave-application.component.html',
  styleUrls: ['./create-leave-application.component.css']
})
export class CreateLeaveApplicationComponent extends BaseAuthorizedComponent implements OnInit {
  onLeaveApplicationApplyEvent: EventEmitter<any> = new EventEmitter();
  onLeaveApplicationEditEvent: EventEmitter<any> = new EventEmitter();
  onLeaveApproveEvent: EventEmitter<any> = new EventEmitter();
  applyLeaveForm: FormGroup
  submitted = false;
  companies: any;
  leaveCalender: any;
  employees: any;
  employeeId: any;
  isAdmin: boolean = false;
  leaveTypes: any;
  leaveTypeId: any;
  loading: boolean = false;
  leaveApplication: LeaveApplication;
  leaveApplicationId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  startDate: any;
  endDate: any;
  showApplyButton : boolean = false;
  showReApplyButton:boolean = false;
  editMode:boolean = false;
  id:any;
  constructor(
    private companyService: CompanyService,
    private employeeService: EmployeeService,
    private leaveTypeService: LeaveTypeService,
    private dialogRef: MatDialogRef<CreateLeaveApplicationComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
   
    private leaveApplicationService: LeaveApplicationService,
    private datePipe: DatePipe,
    @Inject(MAT_DIALOG_DATA) data,
    injector: Injector
  ) {
    super(injector);
    this.leaveApplication = new LeaveApplication();

    if (isNaN(data)) {
      this.leaveApplication = new LeaveApplication(data);
      this.companyId = this.leaveApplication.companyId;
     if(this.leaveApplication.id){
       // if declined then should reappy
       if(this.leaveApplication.approvalStatus ==='9'){
           this.showReApplyButton = true;
       }
     }
      
   
    } 
    if(this.leaveApplication.id && !this.showReApplyButton){
      this.isEditMode = true;
    }
  
    this.buildForm();
    this.getAllCompanies();
    this.getAllEmployees();

    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.isAdmin = this.authService.isAdmin;
    this.onChangeCompany();

  }
  ngOnInit() {
    this.getAllCompanies();
    this.getAllEmployees();
    this.leaveApplication.employeeId = this.employeeId;

    this.applyLeaveForm.patchValue({
      companyId: this.companyId,
      employeeId: this.employeeId
    });
    this.applyLeaveForm.updateValueAndValidity();

    //this.onChangeEmployee();
    //this.leaveCalender = new Date().getFullYear();
    if(this.leaveApplication.id){
    this.startDate  = new Date(this.leaveApplication.startDate);
    this.endDate = new Date(this.leaveApplication.endDate);
    this.id = this.leaveApplication.id;
     this.calculateDiffOfDays();
    }
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    if (this.companyId) {
      this.getAllLeaveTypeByEmployeeId();
    }
  }
  getAllLeaveTypeByCompanyId(companyId) {
    this.leaveTypeService.getAllLeaveTypeByCompanyIdForCombo(companyId).subscribe((result: any) => {
      this.leaveTypes = result;
    });
  }
  getAllLeaveTypeByEmployeeId() {
   
    this.loading = true;
    this.leaveTypeService.getAllLeaveTypeByEmployeeId(this.employeeId).subscribe(result => {
      this.leaveTypes = result;
      this.loading = false;
      
    }, () => {
     
      this.loading = false;
    })
  }

  onChangeLeaveType() {
    this.leaveTypeId = this.f.leaveTypeId.value;
  }
  onChangeEmployee() {
    this.employeeId = this.f.employeeId.value;
    this.getAllLeaveTypeByEmployeeId();
  }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  onChangeStartDate(event: any): void {
    let date = event.value;
    this.startDate = new Date(date);
    this.calculateDiffOfDays();


  }
  onChangeEndDate(event: any): void {
    let date = event.value;
    this.endDate = new Date(date);
    this.calculateDiffOfDays();


  }
  buildForm() {
    this.applyLeaveForm = this.formBuilder.group({
      companyId: [this.leaveApplication.companyId, [Validators.required]],
      employeeId: [this.leaveApplication.employeeId, [Validators.required]],
      leaveTypeId: [this.leaveApplication.leaveTypeId, [Validators.required]],
      startDate: [this.leaveApplication.startDate, [Validators.required]],
      endDate: [this.leaveApplication.endDate, [Validators.required]],
      leaveDays: [this.leaveApplication.noOfDays, [Validators.required]],
      reason: [this.leaveApplication.reason, [Validators.required, Validators.maxLength(50)]]
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    // console.log(this.leaveApplication);
    if (this.applyLeaveForm.invalid) {
      return;
    }
    if (this.leaveApplication.id === undefined || this.showReApplyButton) {
      this.saveLeaveApplication();
    }
    else {
      //approve method here
      this.editLeaveApplication();
    }

  }
  saveLeaveApplication() {
    let year = "";
    if(this.leaveApplication.id){
      year = new Date(this.leaveApplication.startDate).getFullYear().toString();
     
    }
    else{
      year = new Date(this.applyLeaveForm.value.startDate).getFullYear().toString();
    
    }
    this.leaveApplication = new LeaveApplication(this.applyLeaveForm.value);
    
    //this.startDate = this.datePipe.transform(new Date(this.startDate), 'yyyy-MM-dd');
  this.leaveApplication.leaveCalendar = year;
    this.leaveApplication.startDate = this.datePipe.transform(this.leaveApplication.startDate, "yyyy-MM-dd hh:mm");
    this.leaveApplication.endDate = this.datePipe.transform(this.leaveApplication.endDate, "yyyy-MM-dd hh:mm");
    this.loading = true;
    this.leaveApplicationService.applyLeave(this.leaveApplication).subscribe((data) => {
      this.onLeaveApplicationApplyEvent.emit(this.leaveApplication.id);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Leave applied successfully");

        this.dialogRef.close();

      } else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });

  }
  editLeaveApplication():void{
    let year = "";
    if(this.leaveApplication.id){
      year = new Date(this.leaveApplication.startDate).getFullYear().toString();
     
    }
    else{
      year = new Date(this.applyLeaveForm.value.startDate).getFullYear().toString();
    
    }
    let newData = new LeaveApplication(this.applyLeaveForm.value);
    this.leaveApplication = Object.assign({}, newData);
    if(newData!== null){
    this.leaveApplication.leaveCalendar = year;
    //this.startDate = this.datePipe.transform(new Date(this.startDate), 'yyyy-MM-dd');
    this.leaveApplication.id = this.id;
    this.leaveApplication.startDate = this.datePipe.transform(newData.startDate, "yyyy-MM-dd hh:mm");
    this.leaveApplication.endDate = this.datePipe.transform(newData.endDate, "yyyy-MM-dd hh:mm");
    this.loading = true;
    this.leaveApplicationService.updateLeave(this.leaveApplication).subscribe((data) => {
      this.onLeaveApplicationEditEvent.emit(this.leaveApplication.id);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Leave updated successfully");

        this.dialogRef.close();

      } else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });
  }
  }

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }
  get f() { return this.applyLeaveForm.controls; }

  calculateDiffOfDays(): void {
    if (this.startDate && this.endDate) {
      let startDate = this.startDate;
      let endDate = this.endDate;
      let diffTime = Math.abs(endDate - startDate );
      let diffDays = (diffTime / (1000 * 60 * 60 * 24));
    
      this.f.leaveDays.setValue(diffDays + 1);


    }
  }

}
