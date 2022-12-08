import { Component, OnInit, Input } from '@angular/core';
import { EmployeeEnrollmentService, CommonLookUpTypeService, EmployeeStatusService, FileUploadService, LeaveGroupService } from '../../shared/services';
import { EmployeeEnrollment, EmployeeStatus, LeaveGroup } from '../../shared/models';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ConfirmationRuleService } from '../../shared/services/company/confirmation-rule.service';
import { GradeService } from '../../shared/services/company/grade.service';
import { SalaryRuleService } from '../../shared/services/company/salary-rule.service';
import { AlertService } from '../../shared/services/alert.service';
import { AuthService } from '../../auth/services/auth.service';
import { Router } from '@angular/router';
import { logging } from 'protractor';
import { EditEmployeeStatusComponent } from './edit-employee-status/edit-employee-status.component';
import { EmployeeStatusHistory } from '../../shared/models/employee/employee-status-history';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-employee-enrollment',
  templateUrl: './employee-enrollment.component.html',
  styleUrls: ['./employee-enrollment.component.css']
})
export class EmployeeEnrollmentComponent implements OnInit {
  @Input() employeeId: any;
  @Input() employeeFullName: any;
  panelOpenState = true;
  @BlockUI('employee-enrollment-section') blockUI: NgBlockUI;
  employeeEnrollment: EmployeeEnrollment = new EmployeeEnrollment();
  confirmationRules: any;
  grades: any;
  salaryRules: any;
  employeeStatuses: any;
  employeeEnrollmentForm: FormGroup;
  disableBtn = false;
  companyId: any;
  submitted: boolean;
  imagePreviewPath: any;
  profilePictureSrc: string = 'assets/images/avatar.png';
  signatureSrc: string = 'assets/images/sign.png';
  defaultProfilePictureSrc: string = 'assets/images/avatar.png';
  fileToUpload: File;
  leaveTypeGroupName:string ='';
  fileName: any;
  uploadedFiles: any;
  fileValidationError: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  isNotEnrolled: boolean;
  isEnrolled: boolean = true;
  leaveGroups: LeaveGroup[] = [];
  leaveGroup:any;
  isHRManager:boolean;
  isAdmin:boolean;
  constructor(
    private formBuilder: FormBuilder,
    private employeeEnrollmentService: EmployeeEnrollmentService,
    private alertService: AlertService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private confirmationRuleService: ConfirmationRuleService,
    private gradeService: GradeService,
    private salaryRuleService: SalaryRuleService,
    private employeeStatusService: EmployeeStatusService,
    private fileUploadService: FileUploadService,
    private leaveGroupService: LeaveGroupService,
    private authService: AuthService,
    private datePipe: DatePipe,
    private dialog: MatDialog,
    private router: Router) { }
  ngOnInit() {
    if (!this.authService.checkAuthenticated()) {
      this.router.navigateByUrl('/auth/login');
    }
  this.isHRManager = this.authService.isHRManager;
  this.isAdmin = this.authService.isAdmin;
    this.setLoggedInUserInformation();
    this.getEmployeeEnrollment();
    this.getAllConfirmationRules();
    this.getAllGrades();
    //this.getAllSalaryRules();
    this.getAllEmployeeStatuses();
    this.getAllLeaveGroups();
    this.buildForm();
  }
  getAllLeaveGroups(): void {
    this.loading = true;
    this.leaveGroupService.getAllLeaveGroups(this.companyId).subscribe(
      (res) => {
        this.loading = false;
        this.leaveGroups = res;
      },
      () => {
        this.loading = false;
        this.leaveGroups = [];
      }
    );
  }
  setLoggedInUserInformation() {
    var loggedInUserInfo = this.authService.getLoggedInUserInfo();
    this.employeeFullName = loggedInUserInfo.displayName;
    this.companyId = loggedInUserInfo.companyId;
   
  }
  get f() { return this.employeeEnrollmentForm.controls; }
  getAllConfirmationRules() {
    this.confirmationRuleService.getActiveConfirmationRuleListByCompany(this.companyId).subscribe(result => {
      this.confirmationRules = result;
    });
  }
  getAllGrades() {
    this.gradeService.getGradeListByCompany(this.companyId).subscribe(result => {
      this.grades = result;
    });
  }
  getAllEmployeeStatuses() {
    this.employeeStatusService.getAllEmployeeStatusByCompanyId(this.companyId).subscribe(result => {
      this.employeeStatuses = result;
    });
  }
  getAllSalaryRules() {
    this.salaryRuleService.getActiveSalaryRuleListByCompany(this.companyId).subscribe(result => {
      this.salaryRules = result
    });
  }
  buildForm() {
    this.employeeEnrollmentForm = this.formBuilder.group({
      //employeeId: [this.employeeEnrollment.employeeId, [Validators.required]],
      joiningDate: [this.employeeEnrollment.joiningDate, [Validators.required]],
      quitDate: [this.employeeEnrollment.quitDate],
      //statusId: [this.employeeEnrollment.statusId, [Validators.required]],
      permanentDate: [this.employeeEnrollment.permanentDate],
      jobTypeId: [this.employeeEnrollment.jobTypeId],
      gradeId: [this.employeeEnrollment.gradeId],
      subGradeId: [this.employeeEnrollment.subGradeId],
      employeeTypeId: [this.employeeEnrollment.employeeTypeId],
      employeeCategoryId: [this.employeeEnrollment.employeeCategoryId],
      confirmationId: [this.employeeEnrollment.confirmationId],
      leaveTypeGroup:[this.employeeEnrollment.leaveTypeGroupName],
      leaveTypeGroupId:[this.employeeEnrollment.leaveTypeGroupId]
      //genderId: [this.employeeEnrollment.genderId, [Validators.required]],
    });
  }
  getEmployeeEnrollment() {
    if (this.employeeId) {
      this.blockUI.start();
      this.loading = true;
      this.employeeEnrollmentService.getEmployeeEnrollmentByEmployeeId(this.employeeId).subscribe(result => {
        if (result) {
          this.employeeEnrollment = result;
          this.employeeEnrollment.leaveTypeGroup = result.leaveTypeGroupName;
          this.employeeEnrollment.leaveTypeGroupId = result.leaveTypeGroupId;
         this.isEnrolled = true;
       //  console.log(this.employeeEnrollment);
          // Newly added
          if (this.employeeEnrollment.employeeImageUri) {
            this.profilePictureSrc = this.employeeEnrollment.employeeImageUri;
          }
          if(this.employeeEnrollment.signatureUri){
            this.signatureSrc = this.employeeEnrollment.signatureUri;
          }
          this.buildForm();
        }
        else{
          this.isEnrolled = false;
        }
        this.loading = false;
        this.blockUI.stop();
      }, error => {
        this.blockUI.stop();
        this.loading = false;

      });
    }
  }
  uploadImage(files) {
    this.fileValidationError = null;
   // console.log(files)
    let fileInfo = this.fileUploadService.imageFileUpload(files);
    if (fileInfo.validationError != null) {
      this.fileValidationError = fileInfo.validationError;
      return;
    }
    this.fileName = fileInfo.fileName;
    this.uploadedFiles = fileInfo.formData;
    this.fileToUpload = <File>files[0];
    var formData = new FormData();
    formData.append('file', this.fileToUpload, this.fileToUpload.name);
    formData.append('employeeId', this.employeeId);
    formData.append('id', this.employeeEnrollment.id);
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imagePreviewPath = event.target.result;
    }
    reader.readAsDataURL(files[0]);
    this.loading = true;
   
    this.employeeEnrollmentService.uploadEmployeeEnrollmentImage(formData).subscribe(result => {
      if (result && (result as any).status == true) {
        this.employeeEnrollment.id = (result as any).id;
        this.imagePreviewPath = (result as any).pictureUri;
        this.loading = false;
        this.alertService.success("Employee Image Saved Successfully");
        this.getEmployeeEnrollment();
      }
      else {
        this.alertService.error((result as any).message);
        this.loading = false;
      }
    }, () => {
      this.loading = false;
    });
  }
  uploadSignature(files:any):void{
    this.fileValidationError = null;
    //console.log(files)
    let fileInfo = this.fileUploadService.imageFileUpload(files);
    if (fileInfo.validationError != null) {
      this.fileValidationError = fileInfo.validationError;
      return;
    }
    this.fileName = fileInfo.fileName;
    this.uploadedFiles = fileInfo.formData;
    this.fileToUpload = <File>files[0];
    var formData = new FormData();
    formData.append('file', this.fileToUpload, this.fileToUpload.name);
    formData.append('employeeId', this.employeeId);
    formData.append('id', this.employeeEnrollment.id);
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imagePreviewPath = event.target.result;
    }
    reader.readAsDataURL(files[0]);
    this.loading = true;
   
    this.employeeEnrollmentService.uploadEmployeeSiganture(formData).subscribe(result => {
      if (result && (result as any).status == true) {
        this.employeeEnrollment.id = (result as any).id;
        this.imagePreviewPath = (result as any).pictureUri;
        this.loading = false;
        this.alertService.success("Employee Signature Saved Successfully");
        this.getEmployeeEnrollment();
      }
      else {
        this.alertService.error((result as any).message);
        this.loading = false;
      }
    }, () => {
      this.loading = false;
    });
  }
  saveEmployeeEnrollment() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeeEnrollmentForm.invalid) {
      return;
    }
    if (!this.employeeEnrollment.id) {
      this.createEmployeeEnrollment();
    }
    else {
      this.editEmployeeEnrollment();
    }
  }
  createEmployeeEnrollment() {
    this.employeeEnrollment = new EmployeeEnrollment(this.employeeEnrollmentForm.value);
    this.employeeEnrollment.employeeId = this.employeeId;
    this.employeeEnrollment.statusId = 1;
    this.employeeEnrollment.joiningDate = this.employeeEnrollment.joiningDate === null ? this.employeeEnrollment.joiningDate :
      this.employeeEnrollment.joiningDate === undefined ? this.employeeEnrollment.joiningDate :
        this.datePipe.transform(new Date(this.employeeEnrollment.joiningDate), 'yyyy-MM-dd');
    this.employeeEnrollment.quitDate = this.employeeEnrollment.quitDate === null ? this.employeeEnrollment.quitDate :
      this.employeeEnrollment.quitDate === undefined ? this.employeeEnrollment.quitDate :
        this.datePipe.transform(new Date(this.employeeEnrollment.quitDate), 'yyyy-MM-dd');
    this.employeeEnrollment.permanentDate = this.employeeEnrollment.permanentDate === null ? this.employeeEnrollment.permanentDate :
      this.employeeEnrollment.permanentDate === undefined ? this.employeeEnrollment.permanentDate :
        this.datePipe.transform(new Date(this.employeeEnrollment.permanentDate), 'yyyy-MM-dd');
    
        this.loading = true;
        this.employeeEnrollment.leaveTypeGroup = this.leaveTypeGroupName; 
        
    this.employeeEnrollmentService.createEmployeeEnrollment(this.employeeEnrollment).subscribe(result => {
      if (result && (result as any).status == true) {
        this.employeeEnrollment.id = (result as any).id;
        this.alertService.success("Employee Enrollment Saved Successfully");
        this.getEmployeeEnrollment();
      }
      else {
        this.alertService.error((result as any).message);
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => { this.loading = false; });
  }
  editEmployeeEnrollment() {
    let newData = new EmployeeEnrollment(this.employeeEnrollmentForm.value);
    this.employeeEnrollment.employeeId = this.employeeId;
    
    // this.employeeEnrollment.joiningDate = newData.joiningDate;
    //this.employeeEnrollment.quitDate = newData.quitDate;
    this.employeeEnrollment.joiningDate = this.employeeEnrollment.joiningDate === null ? this.employeeEnrollment.joiningDate :
      this.employeeEnrollment.joiningDate === undefined ? this.employeeEnrollment.joiningDate :
        this.datePipe.transform(new Date(newData.joiningDate), 'yyyy-MM-dd');
    this.employeeEnrollment.quitDate = this.employeeEnrollment.quitDate === null ? this.employeeEnrollment.quitDate :
      this.employeeEnrollment.quitDate === undefined ? this.employeeEnrollment.quitDate :
        this.datePipe.transform(new Date(newData.quitDate), 'yyyy-MM-dd');
    this.employeeEnrollment.permanentDate = this.datePipe.transform(new Date(newData.permanentDate), 'yyyy-MM-dd');
    // this.employeeEnrollment.permanentDate = this.datePipe.transform(new Date(newData.permanentDate), 'yyyy-MM-dd');
    //this.employeeEnrollment.statusId = newData.statusId;
    // this.employeeEnrollment.permanentDate = newData.permanentDate;
    this.employeeEnrollment.quitDate = newData.quitDate !=null? this.datePipe.transform(new Date(newData.quitDate), 'yyyy-MM-dd') : null;
    this.employeeEnrollment.jobTypeId = newData.jobTypeId;
    this.employeeEnrollment.gradeId = newData.gradeId;
    this.employeeEnrollment.subGradeId = newData.subGradeId;
    this.employeeEnrollment.employeeTypeId = newData.employeeTypeId;
    this.employeeEnrollment.employeeCategoryId = newData.employeeCategoryId;
    this.employeeEnrollment.confirmationId = newData.confirmationId;
    this.employeeEnrollment.genderId = newData.genderId;
    this.employeeEnrollment.leaveTypeGroup = this.leaveTypeGroupName === ""? this.leaveGroups.filter(item=> item.id ===  newData.leaveTypeGroupId)[0].leaveTypeGroupName:  this.leaveTypeGroupName;
    this.employeeEnrollment.leaveTypeGroupId = newData.leaveTypeGroupId;
    this.employeeEnrollment.leaveTypeGroupName = this.employeeEnrollment.leaveTypeGroup;
    this.loading = true;
    this.employeeEnrollmentService.editEmployeeEnrollment(this.employeeEnrollment).subscribe(result => {
      if (result && (result as any).status == true) {
        this.alertService.success("Employee Enrollment Saved Successfully");
        this.getEmployeeEnrollment();
      } else {
        this.alertService.error((result as any).message);
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => { this.loading = false; });
  }
  editEmployeeStatus() {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    var employeeStatus = new EmployeeStatusHistory();
    employeeStatus.statusId = this.employeeEnrollment.statusId;
    employeeStatus.employeeId = this.employeeId;
    editDialogConfig.data = employeeStatus
    editDialogConfig.panelClass = 'xHR-dialog';
    editDialogConfig.width = "50%";
    const outletEditDialog = this.dialog.open(EditEmployeeStatusComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onEmployeeStatusHistoryCreateEvent.subscribe((data) => {
      this.getEmployeeEnrollment();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  leaveGroupTypeChange(data:number):void{
    this.leaveTypeGroupName = this.leaveGroups.filter(item=> item.id === data)[0].leaveTypeGroupName;
    console.log(this.leaveGroups);
   
  }
}
