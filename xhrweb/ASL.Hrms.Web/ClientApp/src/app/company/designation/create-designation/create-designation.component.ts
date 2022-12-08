import { Component, OnInit, EventEmitter, inject, Inject } from '@angular/core';
import { CompanyService, BranchService, DepartmentService } from 'src/app/shared/services';
import { DesignationService } from 'src/app/shared/services/company/designation.service';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Designation } from 'src/app/shared/models/company/designation';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AlertService } from '../../../shared/services/alert.service';
@Component({
  selector: 'app-create-designation',
  templateUrl: './create-designation.component.html',
  styleUrls: ['./create-designation.component.css']
})
export class CreateDesignationComponent implements OnInit {
  onDesignationCreateEvent: EventEmitter<any> = new EventEmitter();
  onDesignationEditEvent: EventEmitter<any> = new EventEmitter();
  designationCreateForm: FormGroup;
  submitted = false;
  isEditMode = false;
  designation: Designation;
  companies: any;
  branches: any;
  branchId: any;
  departments: any;
  departmentId: any;
  designationId: number;
  linkedEntityId: any;
  linkedEntityType: string = "Department";
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  companyId: any;
  constructor(
    private dialogRef: MatDialogRef<CreateDesignationComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private alertService: AlertService,
    private branchService: BranchService,
    private departmentService: DepartmentService,
    private designationService: DesignationService
    , @Inject(MAT_DIALOG_DATA) data) {
    this.designation = new Designation();
    if (isNaN(data)) {
      this.designation = new Designation(data);
      this.companyId = this.designation.companyId;
      this.branchId = this.designation.branchId;
      this.linkedEntityId = this.designation.departmentId;
    } else {
    }
    this.designation.companyId = localStorage.getItem('companyId');
    this.buildForm();
    this.getAllBranchByCompanyId(this.companyId);
    this.getALLDepartmentByBranchId(this.branchId);
  }
  ngOnInit() {
    this.getAllCompanies();
    this.getAllBranchByCompanyId(this.designation.companyId);
  }
  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    this.designation.companyId = this.companyId;
    if (this.companyId) {
      this.getAllBranchByCompanyId(this.companyId);
    }
  }
  onChangeBranch() {
    this.branchId = this.f.branchId.value;
    if (this.branchId) {
      this.getALLDepartmentByBranchId(this.branchId);
    }
  }
  onChangeDepartment() {
    this.departmentId = this.f.departmentId.value;
    this.linkedEntityId = this.f.departmentId.value;
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  getAllBranchByCompanyId(companyId) {
    this.branchService.getAllBranchByCompanyId(companyId).subscribe((result: any) => {
      this.branches = result;
    });
  }
  getALLDepartmentByBranchId(branchId) {
    this.departmentService.getAllDepartmentByBranchId(branchId).subscribe((result: any) => {
      this.departments = result;
    });
  }
  buildForm() {
    this.designationCreateForm = this.formBuilder.group({
      companyId: [this.designation.companyId],
      branchId: [this.designation.branchId],
      departmentId: [this.designation.departmentId],
      linkedEntityId: [this.designation.departmentId],
      designationName: [this.designation.designationName, [Validators.required, Validators.maxLength(250)]],
      designationLocalizedName: [this.designation.designationLocalizedName, [Validators.maxLength(150)]],
      shortName: [this.designation.shortName, [Validators.maxLength(20)]],
      sortOrder: [this.designation.sortOrder],
    })
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.designationCreateForm.invalid) {
      return;
    }
    if (this.designation.id === undefined) {
      this.createDesignation();
    }
    else {
      this.editDesignation();
    }
    //this.dialogRef.close();
  }
  createDesignation() {
    this.designation = new Designation(this.designationCreateForm.value);
    this.designation.linkedEntityId = this.linkedEntityId;
    this.designation.linkedEntityType = this.linkedEntityType;
    this.loading = true;
    this.designationService.createDesignation(this.designation).subscribe((data: any) => {
      this.onDesignationCreateEvent.emit(this.designation.id);
      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success("Branch updated successfully");
        this.dialogRef.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
      this.alertService.success("Designation added successfully");
    }, (error: any) => {
      this.alertService.error(error);
      this.loading = true;
    });
  }
  editDesignation() {
    let changeData = new Designation(this.designationCreateForm.value);
    if (this.designation !== null) {
      this.designation.designationName = changeData.designationName;
      this.designation.designationLocalizedName = changeData.designationLocalizedName;
      this.designation.shortName = changeData.shortName;
      this.designation.sortOrder = changeData.sortOrder;
      this.designation.companyId = changeData.companyId;
      this.designation.branchId = changeData.branchId;
      this.designation.departmentId = changeData.departmentId;
      this.designation.linkedEntityId = changeData.departmentId;
      this.designation.linkedEntityType = this.linkedEntityType;
      this.loading = true;
      this.designationService.editDesignation(this.designation).subscribe((data: any) => {
        this.onDesignationEditEvent.emit(this.designation.id);
        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success("Branch updated successfully");
          this.dialogRef.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (data as any).message;
        }
        this.loading = false;
        this.alertService.success("Designation updated successfully");
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
  }
  get f() { return this.designationCreateForm.controls; }
}
