import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, BranchService, EmployeeFamilyMemberService, DepartmentService, DesignationService, CommonLookUpTypeService } from 'src/app/shared/services';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { EmployeeFamilyMember, Guid } from '../../../shared/models';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-employee-family-member',
  templateUrl: './create-employee-family-member.component.html',
  styleUrls: ['./create-employee-family-member.component.css']
})
export class CreateEmployeeFamilyMemberComponent implements OnInit {
  onEmployeeFamilyMemberCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeFamilyMemberEditEvent: EventEmitter<any> = new EventEmitter();
  employeeFamilyMemberCreateForm: FormGroup;
  submitted = false;
  isEditMode = false;
  companies: any;
  employeeId: any;
  employeeFamilyMember: EmployeeFamilyMember;
  relationTypes: any[] = [];
  companyId: any = localStorage.getItem('companyId');
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private dialogRef: MatDialogRef<CreateEmployeeFamilyMemberComponent>,
    private formBuilder: FormBuilder,
    private alertService: AlertService,
    private employeeFamilyMemberService: EmployeeFamilyMemberService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeFamilyMember = new EmployeeFamilyMember();
    if (isNaN(data)) {
      this.employeeFamilyMember = new EmployeeFamilyMember(data);
      this.employeeId = this.employeeFamilyMember.employeeId;
    } else {
      //this.companyId = data;
      //this.employeeFamilyMember.companyId = this.companyId;
    }
    this.buildForm();
  }
  ngOnInit() {
    this.getAllCompanies();
    this.getAllRelationTypes();
  }
  getAllRelationTypes() {
    if (this.companyId) {
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "RelationType").subscribe(result => {
        this.relationTypes = result;
      });
    }
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  buildForm() {
    this.employeeFamilyMemberCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      familiyMemberName: [this.employeeFamilyMember.familiyMemberName, [Validators.required, Validators.maxLength(100)]],
      dateOfBirth: [this.employeeFamilyMember.dateOfBirth, [Validators.required]],
      educationClass: [this.employeeFamilyMember.educationClass, [Validators.maxLength(150)]],
      educationalInstitute: [this.employeeFamilyMember.educationalInstitute, [Validators.maxLength(150)]],
      educationYear: [this.employeeFamilyMember.educationYear, [Validators.maxLength(20)]],
      relationTypeId: [this.employeeFamilyMember.relationTypeId, [Validators.required]],
      relationTypeName: [this.employeeFamilyMember.relationTypeName],
      isDependant: [this.employeeFamilyMember.isDependant],
      isEligibleForMedical: [this.employeeFamilyMember.isEligibleForMedical],
      isEligibleForEducation: [this.employeeFamilyMember.isEligibleForEducation],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
   
    if (this.employeeFamilyMemberCreateForm.invalid) {
      return;
    }
    if (this.employeeFamilyMember.id === undefined) {
      this.createEmployeeFamilyMember();
    }
    else {
      this.editEmployeeFamilyMember();
    }
    this.dialogRef.close();
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    this.getAllRelationTypes();
  }
  createEmployeeFamilyMember() {
    this.employeeFamilyMember = new EmployeeFamilyMember(this.employeeFamilyMemberCreateForm.value);
    this.employeeFamilyMember.employeeId = this.employeeId;
    this.loading = true;
    this.employeeFamilyMemberService.createEmployeeFamilyMember(this.employeeFamilyMember).subscribe((data: any) => {
      this.onEmployeeFamilyMemberCreateEvent.emit(this.employeeFamilyMember.id);
      if(data.status === true){
        this.isFormValid = true;
        this.alertService.success("Employee Family Member added successfully");
        this.dialogRef.close();
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
  editEmployeeFamilyMember() {
    let newData = new EmployeeFamilyMember(this.employeeFamilyMemberCreateForm.value);
    if (this.employeeFamilyMember !== null) {
      this.employeeFamilyMember.employeeId = this.employeeId;
      this.employeeFamilyMember.companyId = newData.companyId;
      this.employeeFamilyMember.familiyMemberName = newData.familiyMemberName;
      this.employeeFamilyMember.dateOfBirth = newData.dateOfBirth;
      this.employeeFamilyMember.educationClass = newData.educationClass;
      this.employeeFamilyMember.educationalInstitute = newData.educationalInstitute;
      this.employeeFamilyMember.educationYear = newData.educationYear;
      this.employeeFamilyMember.relationTypeId = newData.relationTypeId;
      this.employeeFamilyMember.relationTypeName = newData.relationTypeName;
      this.employeeFamilyMember.isDependant = newData.isDependant;
      this.employeeFamilyMember.isEligibleForMedical = newData.isEligibleForMedical;
      this.employeeFamilyMember.isEligibleForEducation = newData.isEligibleForEducation;
      this.loading = true;
      this.employeeFamilyMemberService.editEmployeeFamilyMember(this.employeeFamilyMember).subscribe((data: any) => {
        
        this.onEmployeeFamilyMemberEditEvent.emit(this.employeeFamilyMember.id);
        if(data.status === true){
          this.isFormValid = true;
          this.alertService.success("Employee Family Member edited successfully");
          this.dialogRef.close();
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
  }
  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);
    //this.alertService.error("Internal server error happen");
  }
  get f() { return this.employeeFamilyMemberCreateForm.controls; }
}
