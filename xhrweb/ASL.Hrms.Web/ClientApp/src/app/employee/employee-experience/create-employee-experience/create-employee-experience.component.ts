import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeExperience } from 'src/app/shared/models';
import { EmployeeExperienceService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-employee-experience',
  templateUrl: './create-employee-experience.component.html',
  styleUrls: ['./create-employee-experience.component.css']
})
export class CreateEmployeeExperienceComponent implements OnInit {
  onEmployeeExperienceCreateEvent: EventEmitter<number> = new EventEmitter();
  onEmployeeExperienceEditEvent: EventEmitter<number> = new EventEmitter();
  employeeExperienceCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  companies: any = [];
  thanas: any = [];
  districts: any = [];
  countries: any = [];
  addressTypes: any = [];
  employeeExperience: EmployeeExperience;
  employeeExperienceId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  constructor(
    private dialogRef: MatDialogRef<CreateEmployeeExperienceComponent>,
    private formBuilder: FormBuilder,
    private employeeExperienceservice: EmployeeExperienceService,
    private alertService: AlertService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeExperience = new EmployeeExperience();
    if (isNaN(data)) {
      this.employeeExperience = new EmployeeExperience(data);
      this.employeeId = this.employeeExperience.employeeId;
    }
    if (this.employeeExperience.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();
  }
  ngOnInit() {
  }
  buildForm() {
    this.employeeExperienceCreateForm = this.formBuilder.group({
      companyName: [this.employeeExperience.companyName, [Validators.required, Validators.maxLength(150)]],
      designation: [this.employeeExperience.designation, [Validators.required, Validators.maxLength(150)]],
      functionalDesignation: [this.employeeExperience.functionalDesignation, [Validators.maxLength(150)]],
      department: [this.employeeExperience.department, [ Validators.maxLength(150)]],
      responsibilities: [this.employeeExperience.responsibilities, [Validators.required, Validators.maxLength(500)]],
      companyAddress: [this.employeeExperience.companyAddress, [Validators.required, Validators.maxLength(150)]],
      companyMobile: [this.employeeExperience.companyMobile, [Validators.maxLength(20)]],
      companyContactNo: [this.employeeExperience.companyContactNo, [Validators.maxLength(20)]],
      companyEmail: [this.employeeExperience.companyEmail, [Validators.email, Validators.maxLength(150)]],
      companyWeb: [this.employeeExperience.companyWeb, [Validators.maxLength(50)]],
      fromDate: [this.employeeExperience.fromDate, [Validators.required]],
      toDate: [this.employeeExperience.toDate],
      tillDate: [this.employeeExperience.tillDate],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
  
    if (this.employeeExperienceCreateForm.invalid) {
      return;
    }
    if (this.employeeExperience.id === undefined) {
      this.createEmployeeExperience();
    }
    else {
      this.editEmployeeExperience();
    }
    this.dialogRef.close();
  }
  createEmployeeExperience() {
    this.employeeExperience = new EmployeeExperience(this.employeeExperienceCreateForm.value);
    this.employeeExperience.employeeId = this.employeeId;
    this.employeeExperienceservice.createEmployeeExperience(this.employeeExperience).subscribe((data: any) => {
      this.onEmployeeExperienceCreateEvent.emit(this.employeeExperience.employeeId);
      if(data.status === true){
        this.isFormValid = true;
        this.alertService.success("Employee Experience added successfully");
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
  editEmployeeExperience() {
    let newData = new EmployeeExperience(this.employeeExperienceCreateForm.value);
    if (this.employeeExperience !== null) {
      this.employeeExperience.companyName = newData.companyName;
      this.employeeExperience.designation = newData.designation;
      this.employeeExperience.functionalDesignation = newData.functionalDesignation;
      this.employeeExperience.responsibilities = newData.responsibilities;
      this.employeeExperience.companyAddress = newData.companyAddress;
      this.employeeExperience.companyContactNo = newData.companyContactNo;
      this.employeeExperience.companyMobile = newData.companyMobile;
      this.employeeExperience.companyEmail = newData.companyEmail;
      this.employeeExperience.companyWeb = newData.companyWeb;
      this.employeeExperience.fromDate = newData.fromDate;
      this.employeeExperience.toDate = newData.toDate;
      this.employeeExperience.tillDate = newData.tillDate;
      this.employeeExperienceservice.editEmployeeExperience(this.employeeExperience).subscribe((data: any) => {
        this.onEmployeeExperienceEditEvent.emit(this.employeeExperience.id);
        if(data.status === true){
          this.isFormValid = true;
          this.alertService.success("Employee Experience edited successfully");
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
  get f() { return this.employeeExperienceCreateForm.controls; }
}
