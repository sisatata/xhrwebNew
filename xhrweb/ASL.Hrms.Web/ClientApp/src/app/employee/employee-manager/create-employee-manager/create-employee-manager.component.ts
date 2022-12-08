import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeManager } from 'src/app/shared/models';
import { EmployeeManagerService, EmployeeService, CommonLookUpTypeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { AuthService } from '../../../auth/services/auth.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-create-employee-manager',
  templateUrl: './create-employee-manager.component.html',
  styleUrls: ['./create-employee-manager.component.css']
})
export class CreateEmployeeManagerComponent implements OnInit {
  onEmployeeManagerCreateEvent: EventEmitter<number> = new EventEmitter();
  onEmployeeManagerEditEvent: EventEmitter<number> = new EventEmitter();
  employeeManagerCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = []
  thanas: any = [];
  districts: any = [];
  countries: any = [];
  managerTypes: any = [];
  possibleManagers: any = [];
  companyId: any;
  employeeManager: EmployeeManager;
  employeeManagerId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  constructor(
    private employeeService: EmployeeService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private dialogRef: MatDialogRef<CreateEmployeeManagerComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private authService: AuthService,
    private router: Router,
    private employeeManagerService: EmployeeManagerService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeManager = new EmployeeManager();
    if (isNaN(data)) {
      this.employeeManager = new EmployeeManager(data);
      this.employeeId = this.employeeManager.employeeId;
    }
    if (this.employeeManager.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();
  }
  ngOnInit() {
    if (!this.authService.checkAuthenticated()) {
      this.router.navigateByUrl('/auth/login');
    }
    this.setLoggedInUserInformation();
    this.getAllEmployees();
    this.getAllManagerTypes();
  }
  setLoggedInUserInformation() {
    var loggedInUserInfo = this.authService.getLoggedInUserInfo();
    //this.employeeFullName = loggedInUserInfo.displayName;
    this.companyId = loggedInUserInfo.companyId;
    //this.employeeId = employeeId;//loggedInUserInfo.employeeId;
    //this.employeeEnrollment.employeeId = employeeId;// loggedInUserInfo.employeeId;
    //this.imagePreviewPath
  }
  buildForm() {
    this.employeeManagerCreateForm = this.formBuilder.group({
      employeeId: [this.employeeManager.employeeId, [Validators.required]],
      managerId: [this.employeeManager.managerId, [Validators.required]],
      isPrimaryManager: [this.employeeManager.isPrimaryManager],
      assignedBy: [this.employeeManager.assignedBy],
      assignDate: [this.employeeManager.assignDate],
      unAssignedBy: [this.employeeManager.unAssignedBy],
      unAssignDate: [this.employeeManager.unAssignDate],
      companyId: this.companyId,//[this.employeeManager.companyId],
      managerTypeId: [this.employeeManager.managerTypeId, [Validators.required]],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeeManagerCreateForm.invalid) {
      return;
    }
    if (this.employeeManager.id === undefined) {
      this.createEmployeeManager();
    }
    else {
      this.editEmployeeManager();
    }
    this.dialogRef.close();
  }
  createEmployeeManager() {
    this.employeeManager = new EmployeeManager(this.employeeManagerCreateForm.value);
    this.employeeManager.employeeId = this.employeeId;
    //this.employeeManager.com = false;
    this.employeeManagerService.createEmployeeManager(this.employeeManager).subscribe((data: any) => {
      this.onEmployeeManagerCreateEvent.emit(this.employeeManager.employeeId);
      if(data.status === true){
        this.isFormValid = true;
        this.alertService.success("Employee Manager added successfully");
        this.dialogRef.close();
       }
       else{
        
        this.isFormValid = false;
        this.errorMessages = data.message;
        
       }
     
    }, (error: any) => {
      this.showErrorMessage(error);
    });
  }
  editEmployeeManager() {
    let newData = new EmployeeManager(this.employeeManagerCreateForm.value);
    if (this.employeeManager !== null) {
      this.employeeManager.employeeId = newData.employeeId;
      this.employeeManager.managerId = newData.managerId;
      this.employeeManager.isPrimaryManager = newData.isPrimaryManager;
      this.employeeManager.assignedBy = newData.assignedBy;
      this.employeeManager.assignDate = newData.assignDate;
      this.employeeManager.unAssignedBy = newData.unAssignedBy;
      this.employeeManager.unAssignDate = newData.unAssignDate;
      this.employeeManager.companyId = this.companyId;
      this.employeeManager.managerTypeId = newData.managerTypeId;
      this.employeeManagerService.editEmployeeManager(this.employeeManager).subscribe((data: any) => {
        this.onEmployeeManagerEditEvent.emit(this.employeeManager.id);
        if(data.status === true){
          this.isFormValid = true;
          this.alertService.success("Employee Manager updated successfully");
          this.dialogRef.close();
         }
         else{
          
          this.isFormValid = false;
          this.errorMessages = data.message;
          
         }
       
      }, (error: any) => {
        this.showErrorMessage(error);
      });
    }
  }
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
  }
  get f() { return this.employeeManagerCreateForm.controls; }
  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe(result => {
      this.possibleManagers = result;
    });
    //this.possibleManagers = this.employeeService.getAllEmployees();
  }
  getAllManagerTypes() {
    this.commonLookUpTypeService.getCommonLookUpTypeByCode('ManagerType').subscribe(result => {
      this.managerTypes = result;
    });
    //this.managerTypes = this.commonLookUpTypeService.getCommonLookUpTypeByCode('ManagerType');
  }
}
