import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormGroupDirective, NgForm } from '@angular/forms';
import { CompanyService, BranchService, EmployeeService, DepartmentService, DesignationService, CommonLookUpTypeService } from 'src/app/shared/services';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Employee } from '../../shared/models';
import { AlertService } from 'src/app/shared/services/alert.service';
import { ErrorStateMatcher } from '@angular/material/core';



export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const invalidCtrl = !!(control && control.invalid && control.parent.dirty);
    const invalidParent = !!(control && control.parent && control.parent.invalid && control.parent.dirty);

    return (invalidCtrl || invalidParent);
    //return control.dirty && form.invalid;
  }
}


@Component({
  selector: 'app-create-employee',
  templateUrl: './create-employee.component.html',
  styleUrls: ['./create-employee.component.css']
})

export class CreateEmployeeComponent implements OnInit {

  onEmployeeCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeEditEvent: EventEmitter<any> = new EventEmitter();
  employeeCreateForm: FormGroup;
  public formSubmitValid: boolean;
  errorMessages: any[] = [];
  errorMessage: string;
  submitted = false;
  isEditMode = false;
  branchId: any;
  departmentId: any;
  positionId: any;
  companies: any;
  departments: any;
  branches: any;
  companyId: any;
  employee: Employee;
  employeeId: string;
  positions: any;
  genders: any;
  nationalities: any;
  hidePassword: boolean = true;
  hideConfirmPassword: boolean = true;
  matcher = new MyErrorStateMatcher();
  isFormValid: boolean;
loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private dialogRef: MatDialogRef<CreateEmployeeComponent>,
    private formBuilder: FormBuilder,
    private branchService: BranchService,
    private alertService: AlertService,
    private employeeService: EmployeeService,
    private designationService: DesignationService,
    private departmentService: DepartmentService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employee = new Employee();
    if (isNaN(data)) {
      this.employee = new Employee(data);
      this.companyId = this.employee.companyId;
      this.branchId = this.employee.branchId;
      this.departmentId = this.employee.departmentId;
      this.positionId = this.employee.positionId;

    } else {
      //this.companyId = data;
      //this.employee.companyId = this.companyId;
    }

    if (this.employee.id === undefined) {
      this.isEditMode = false;
    }
    else {
      this.isEditMode = true;
    }
    this.buildForm();

    this.addValidators();

    this.getAllBranchByCompanyId(this.companyId);
    this.getAllDepartmentByBranchId();
    this.getAllDesignationsByDepartmentId();
  }


  addValidators() {
    if (this.employee.id === undefined) {
      this.employeeCreateForm.get('password').setValidators([Validators.required, Validators.minLength(6)]);
      this.employeeCreateForm.get('userId').setValidators([Validators.required]);

    } else {
      this.employeeCreateForm.get('password').setValidators(null);
      this.employeeCreateForm.get('userId').setValidators(null);
    }
    this.employeeCreateForm.updateValueAndValidity();
  }

  ngOnInit() {
    this.getAllCompanies();

    if (this.companyId) {
      this.getAllBranchByCompanyId(this.companyId);
      this.getAllGenders();
      this.getAllNationalities();
    }
  }


  getAllGenders() {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "Gender").subscribe(result => {
      this.genders = result;
    });
  }

  getAllNationalities() {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "Nationality").subscribe(result => {
      this.nationalities = result;
    });
  }

  getAllDesignationsByDepartmentId() {
    this.designationService.getAllDesignationByDepartmentId(this.f.departmentId.value).subscribe(result => {
      this.positions = result;
    });
  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    if (this.companyId) {
      this.getAllBranchByCompanyId(this.companyId);
      this.getAllGenders();
      this.getAllNationalities();
    }
  }

  onChangeBranch() {
    this.branchId = this.f.branchId.value;
    if (this.branchId) {
      this.getAllDepartmentByBranchId();
    }
  }

  onChangeDepartment() {
    this.departmentId = this.f.departmentId.value;
    if (this.departmentId) {
      this.getAllDesignationsByDepartmentId();
    }
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  getAllBranchByCompanyId(companyId) {
    this.branchService.getAllBranchByCompanyId(companyId).subscribe((result: any) => {
      this.branches = result;
    });
  }

  getAllDepartmentByBranchId() {
    this.departmentService.getAllDepartmentByBranchId(this.branchId).subscribe(result => {
      this.departments = result;
    }, error => {
    })
  }


  buildForm() {
    this.employeeCreateForm = this.formBuilder.group({
      companyId: [this.employee.companyId, [Validators.required]],
      branchId: [this.employee.branchId, [Validators.required]],
      departmentId: [this.employee.departmentId, [Validators.required]],
      positionId: [this.employee.positionId, [Validators.required]],
      employeeId: [this.employee.employeeId, [Validators.required, Validators.maxLength(15)]],
      fullName: [this.employee.fullName, [Validators.required, Validators.maxLength(50)]],
      fullNameLC: [this.employee.fullNameLC, [Validators.maxLength(150), Validators.maxLength(50)]],
      dateOfBirth: [this.employee.dateOfBirth, [Validators.required]],
      referenceNumber: [this.employee.referenceNumber, [Validators.maxLength(15)]],
      nationalityId: [this.employee.nationalityId, [Validators.required]],
      genderId: [this.employee.genderId, [Validators.required]],
      userId: [this.employee.userId],
      password: [this.employee.password],
      confirmPassword: [''],
    }, { validator: this.checkPasswords });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeeCreateForm.invalid) {
      return;
    }
    if (this.employee.id === undefined) {
      this.createEmployee();
    }
    else {
      this.editEmployee();
    }
    if (this.formSubmitValid === true) {
      this.dialogRef.close();
    }
  }


  createEmployee() {
    this.employee = new Employee(this.employeeCreateForm.value);
    this.loading = true;
    this.employeeService.createEmployee(this.employee).subscribe((data: any) => {
      if (data.status === true) {
        this.formSubmitValid = true;
        this.onEmployeeCreateEvent.emit(this.employee.id);
        this.alertService.success("Employee added successfully");
        this.dialogRef.close();
      }
      else {
        this.formSubmitValid = false;
        this.errorMessage = data.message;
      }
      this.loading = false;
    }, (error: any) => {
      this.alertService.success(error);
      this.loading = false;
      this.showErrorMessage(error);
    });
  }

  editEmployee() {
    let newData = new Employee(this.employeeCreateForm.value);
    if (this.employee !== null) {
      this.employee.companyId = newData.companyId;
      this.employee.branchId = newData.branchId;
      this.employee.departmentId = newData.departmentId;
      this.employee.positionId = newData.positionId;
      this.employee.employeeId = newData.employeeId;
      this.employee.fullName = newData.fullName;
      this.employee.fullNameLC = newData.fullNameLC;
      this.employee.dateOfBirth = newData.dateOfBirth;
      this.employee.referenceNumber = newData.referenceNumber;
      this.employee.nationalityId = newData.nationalityId;
      this.employee.genderId = newData.genderId;
      this.loading = true;
      this.employeeService.editEmployee(this.employee).subscribe((data: any) => {
        this.onEmployeeEditEvent.emit(this.employee.id);
       
        if((data as any).status === true){
          this.isFormValid = true;
          this.alertService.success("Employee edited successfully");
          this.dialogRef.close();
        }
        else{
          this.isFormValid = false;
          this.errorMessages = (data as  any).message;
        }
       this.loading = false;
      }, (error: any) => {
        this.alertService.error(error);
        this.loading = false;

        this.showErrorMessage(error);
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
  get f() { return this.employeeCreateForm.controls; }

  checkPasswords(group: FormGroup) { // here we have the 'passwords' group
    let pass = group.controls.password.value;
    let confirmPass = group.controls.confirmPassword.value;

    return pass === confirmPass ? null : { notSamePassword: true }
  }
}

