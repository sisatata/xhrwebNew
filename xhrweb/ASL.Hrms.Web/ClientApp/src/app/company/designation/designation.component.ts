import { Component, OnInit, Injector } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Designation } from 'src/app/shared/models/company/designation';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { DesignationService } from 'src/app/shared/services/company/designation.service';
import { DepartmentService, BranchService, CompanyService } from 'src/app/shared/services';
import { CreateDesignationComponent } from './create-designation/create-designation.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';

@Component({
  selector: 'app-designation',
  templateUrl: './designation.component.html',
  styleUrls: ['./designation.component.css']
})
export class DesignationComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('designation-list-section') blockUI: NgBlockUI
  designations: Designation[]=[];
  designatId: any;
  desinationFilterFormGroup: FormGroup
  departments: any;
  departmentId: any;
  branches: any;
  branchId: any;
  companies: any;
  submitted: boolean;
  linkedEntityId: any;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;

  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private designationService: DesignationService,
    private departmentServce: DepartmentService,
    private branchService: BranchService,
    private companyService: CompanyService,
    private injector: Injector
  ) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }

  buildForm() {
    this.desinationFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      branchId: [this.branchId, Validators.required],
      departmentId: [this.departmentId, Validators.required]
    })
  }

  get f() { return this.desinationFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllBranchByCompanyId();
    }
  }

  onChangeBranch(branchId: any) {
    this.branchId = branchId;
    if (branchId) {
      this.getALLDepartmentByBranchId();
    }
  }

  onChangeDepartment(departmentId: any) {
    this.departmentId = departmentId;
    this.linkedEntityId = departmentId;
    if (departmentId) {
      this.getAllDesignationByDepartmentId();
    }
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }

  getAllBranchByCompanyId() {
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
      this.branches = result;
     // this.setBranch(result);
    });
  }
  setBranch(branches:any):void{
   this.branchId = branches[0].id;

   this.buildForm();
  }
  getALLDepartmentByBranchId() {
    this.departmentServce.getAllDepartmentByBranchId(this.branchId).subscribe((result: any) => {
      this.departments = result;
     // this.setDepartment(result);  
    });
  }
  setDepartment(departments:any):void{
    this.departmentId = departments[0].id;
    this.buildForm();
   
  }
  //need to check
  getDesinations() {
    this.submitted = false;
    if (this.desinationFilterFormGroup.invalid) {
      return;
    }
    this.departmentId = this.f.departmentId.value;
    this.linkedEntityId = this.f.departmentId.value;
    this.getAllDesignationByDepartmentId();
  }

  createDesination() {

    const createDesignationDialogConfig = new MatDialogConfig();
    createDesignationDialogConfig.disableClose = true;
    createDesignationDialogConfig.autoFocus = true;
    createDesignationDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createDesignationDialogConfig.width = `${dialogWidth}%`;
    var designation = new Designation();
    designation.branchId = this.branchId;
    designation.departmentId = this.departmentId;
    createDesignationDialogConfig.data = designation;
    const createDesignationDialog = this.dialog.open(CreateDesignationComponent, createDesignationDialogConfig);
    const successfullCreate = createDesignationDialog.componentInstance.onDesignationCreateEvent.subscribe((data) => {
      this.getAllDesignationByDepartmentId();
    })
    createDesignationDialog.afterClosed().subscribe(() => {
    });
  }
  editDesignation(designation: Designation) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = designation;
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateDesignationComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onDesignationEditEvent.subscribe((data) => {
      this.getAllDesignationByDepartmentId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteDesignation(designation: Designation): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the designation " + designation.designationName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteDesignation(designation);
      }
    });
  }
  deleteDesignation(designation: Designation) {
    this.designationService.deleteDesignation(designation).subscribe((data) => {
      this.getAllDesignationByDepartmentId();
    }, (error) => {
      //this.alertService.error('Internal server error happen');
      console.log(error);
    });
  }


  getAllDesignationByDepartmentId() {
    this.blockUI.start();
    this.loading = true;
    this.designationService.getAllDesignationByDepartmentId(this.linkedEntityId).subscribe(result => {
      this.designations = result;      
     this.loading = false;
      this.totalItems = this.designations.length;
      this.generateTotalItemsText();

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    })
  }


}
