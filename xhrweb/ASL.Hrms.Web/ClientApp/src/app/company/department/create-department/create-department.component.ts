import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, BranchService, DepartmentService } from 'src/app/shared/services';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Department } from '../../../shared/models';
import { AlertService } from '../../../shared/services/alert.service';


@Component({
  selector: 'app-create-department',
  templateUrl: './create-department.component.html',
  styleUrls: ['./create-department.component.css']
})
export class CreateDepartmentComponent implements OnInit {

  onDepartmentCreateEvent: EventEmitter<any> = new EventEmitter();
  onDepartmentEditEvent: EventEmitter<any> = new EventEmitter();
  departmentCreateForm: FormGroup
  submitted = false;
  isEditMode = false;
  branchId: any;
  companies: any;
  branches: any;
  companyId: any;
  department: Department;
  departmentId: number;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private alertService:AlertService,
    private dialogRef: MatDialogRef<CreateDepartmentComponent>,
    private formBuilder: FormBuilder,
    private branchService: BranchService,
    private departmentService: DepartmentService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.department = new Department();
    if (isNaN(data)) {
      this.department = new Department(data);
      this.companyId = this.department.companyId;

    } else {
      
    }
    if (this.department.id === undefined) {
      this.isEditMode = false;
    }
    else {
      this.isEditMode = true;
    }
    this.buildForm();
    this.getAllBranchByCompanyId(this.companyId);
  }

  ngOnInit() {
    this.getAllCompanies();
  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    if (this.companyId) {
      this.getAllBranchByCompanyId(this.companyId);
    }
  }

  onChangeBranch() {
    this.branchId = this.f.branchId.value;
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


  buildForm() {
    this.departmentCreateForm = this.formBuilder.group({
      companyId: [this.department.companyId, [Validators.required]],
      branchId: [this.department.branchId, [Validators.required]],
      departmentName: [this.department.departmentName, [Validators.required, Validators.maxLength(250)]],
      departmentLocalizedName: [this.department.departmentLocalizedName, [Validators.maxLength(150)]],
      shortName: [this.department.shortName, [Validators.required, Validators.maxLength(50)]],
      sortOrder: [this.department.sortOrder, [Validators.required, Validators.min(1)]],

    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.departmentCreateForm.invalid) {
      return;
    }
    if (this.department.id === undefined) {
      this.createDepartment();
    }
    else  {
      this.editDepartment();
    }
   
  }


  createDepartment() {
    this.department = new Department(this.departmentCreateForm.value);
    this.loading = true;
    this.departmentService.createDepartment(this.department).subscribe((data: any) => {
      this.onDepartmentCreateEvent.emit(this.department.id);
      if(data.status === true){
        this.isFormValid =true;
        this.alertService.success("Department added successfully");
        this.dialogRef.close();
      }else{
        this.isFormValid = false;
        this.errorMessages = data.message;
      }
      this.loading = false;

    }, (error: any) => {
     this.alertService.error(error);
     this.loading = false;

    });
  }

  editDepartment() {
    
    let newData = new Department(this.departmentCreateForm.value);
    if (this.department !== null) {
      this.department.departmentName = newData.departmentName;
      this.department.departmentLocalizedName = newData.departmentLocalizedName;
      this.department.shortName = newData.shortName;
      this.department.companyId = newData.companyId;
      this.department.branchId = newData.branchId;
      this.department.sortOrder = newData.sortOrder;
      this.loading = true;
      this.departmentService.editDepartment(this.department).subscribe((data: any) => {
        this.onDepartmentEditEvent.emit(this.department.id)
       
        if(data.status === true){
          this.isFormValid =true;
          this.alertService.success("Department updated successfully");
          this.dialogRef.close();
        }else{
          this.isFormValid = false;
          this.errorMessages = data.message;
        }
        this.loading = false;
      }, (error: any) => {
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
    this.alertService.error("Internal server error happen");
  }
  get f() { return this.departmentCreateForm.controls; }
}
