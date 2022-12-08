import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeEducation, Guid } from 'src/app/shared/models';
import { EmployeeEducationService, CommonLookUpTypeService, CompanyService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-employee-education',
  templateUrl: './create-employee-education.component.html',
  styleUrls: ['./create-employee-education.component.css']
})
export class CreateEmployeeEducationComponent implements OnInit {
  onEmployeeEducationCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeEducationEditEvent: EventEmitter<any> = new EventEmitter();
  employeeEducationCreateForm: FormGroup
  submitted = false;
  employeeId: any;
  educationalInstitutes: any = [];
  educationalDegrees: any = [];
  employeeEducation: EmployeeEducation;
  isEditMode = false;
  companyId: any = localStorage.getItem('companyId');//'2d86dab8-5fc1-436b-856a-60096ded9e16'; // It will come after user login. Now set as default;
  companies: any[] = [];
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  employeeEducations: any;
  isDuplicateFound: boolean;
  constructor(
    private dialogRef: MatDialogRef<CreateEmployeeEducationComponent>,
    private formBuilder: FormBuilder,
    private employeeEducationservice: EmployeeEducationService,
    private companyService: CompanyService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private alertService: AlertService,
    private employeeEducationService : EmployeeEducationService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeEducation = new EmployeeEducation();
    if (isNaN(data)) {
      this.employeeEducation = new EmployeeEducation(data);
      this.employeeId = this.employeeEducation.employeeId;
    }
    if (this.employeeEducation.id) {
      this.isEditMode = true;
    }
    else {
      this.isEditMode = false;
    }
    this.buildForm();
  }
  ngOnInit() {
    this.getAllCompanies();
    this.getAllEducationDegrees();
    this.getAllEducationalInstitue();
    this.getAllEducations();
  }
  getAllEducations() {
   this.loading = true;
    this.employeeEducationService.getEmployeeEducationByEmployeeId(this.employeeId).subscribe(result => {
      this.employeeEducations = result;
      this.loading = false;
    }, error => {
      this.loading = false;

    });
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  buildForm() {
    this.employeeEducationCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      educationalDegreeId: [this.employeeEducation.educationalDegreeId, [Validators.required]], //"3fa85f64-5717-4562-b3fc-2c963f66afa6"
      educationalInstituteId: [this.employeeEducation.educationalInstituteId, [Validators.required]],
      session: [this.employeeEducation.session, [ Validators.maxLength(20)]],
      passingYear: [this.employeeEducation.passingYear, [Validators.required, Validators.maxLength(20)]],
      boardorUniversity: [this.employeeEducation.boardorUniversity, [Validators.required, Validators.required, Validators.maxLength(150)]],
      result: [this.employeeEducation.result, [Validators.maxLength(30)]],
      resultType: [this.employeeEducation.resultType, [Validators.maxLength(10)]],
      outOf: [this.employeeEducation.outOf]
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if( this.checkaDuplicateDegree(this.employeeEducationCreateForm)){
      this.isDuplicateFound = true;
      return; 
    }
    
  
    if (this.employeeEducationCreateForm.invalid) {
      return;
    }
    if (this.employeeEducation.id === undefined) {
      this.createEmployeeEducation();
    }
    else {
      this.editEmployeeEducation();
    }
    this.dialogRef.close();
  }
  getAllEducationDegrees() {
    if (this.companyId) {
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "EducationalDegree").subscribe(result => {
        this.educationalDegrees = result;
      });
    }
  }
  getAllEducationalInstitue() {
    if (this.companyId) {
      this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "EducationalInstitue").subscribe(result => {
        this.educationalInstitutes = result;
      });
    }
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    this.getAllEducationalInstitue();
    this.getAllEducationDegrees();
  }
  createEmployeeEducation() {
    this.employeeEducation = new EmployeeEducation(this.employeeEducationCreateForm.value);
    this.employeeEducation.employeeId = this.employeeId;
    this.loading = true;
    this.employeeEducationservice.createEmployeeEducation(this.employeeEducation).subscribe((data: any) => {
      this.onEmployeeEducationCreateEvent.emit(this.employeeEducation.employeeId);
      if (data.status === true) {
        this.isFormValid = true;
        this.alertService.success("Employee Education added successfully");
        this.dialogRef.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = data.message;
      }
      this.loading = false;
    }, (error: any) => {
      this.showErrorMessage(error);
      this.loading = false;
    });
  }
  editEmployeeEducation() {
    let newData = new EmployeeEducation(this.employeeEducationCreateForm.value);
    if (this.employeeEducation !== null) {
      this.employeeEducation.educationalDegreeId = newData.educationalDegreeId;
      this.employeeEducation.educationalInstituteId = newData.educationalInstituteId;
      this.employeeEducation.session = newData.session;
      this.employeeEducation.boardorUniversity = newData.boardorUniversity;
      this.employeeEducation.result = newData.result;
      this.employeeEducation.resultType = newData.resultType;
      this.employeeEducation.outOf = newData.outOf;
      this.employeeEducation.passingYear = newData.passingYear;
      this.loading = true;
      this.employeeEducationservice.editEmployeeEducation(this.employeeEducation).subscribe((data: any) => {
        this.onEmployeeEducationEditEvent.emit(this.employeeEducation.id);
        if (data.status === true) {
          this.isFormValid = true;
          this.alertService.success("Employe Address edited successfully");
          this.dialogRef.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = data.message;
        }
        this.loading = false;
      }, (error: any) => {
        this.showErrorMessage(error);
        this.alertService.error(error);
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
  get f() { return this.employeeEducationCreateForm.controls; }
  checkaDuplicateDegree(data:any):boolean{
    let degree =this.employeeEducation.educationalDegreeId;
    let duplicate = this.employeeEducations.filter(item=> 
      (item.educationalDegreeName === 'S.S.C.' && item.educationalDegreeId === degree ) || 
      
      (item.educationalDegreeName === 'H.S.C.' && item.educationalDegreeId === degree));

   return duplicate.length > 0;
      

  }
}
