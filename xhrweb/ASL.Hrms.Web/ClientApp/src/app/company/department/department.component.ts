import { Component, OnInit, Injector } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { CreateDepartmentComponent } from './create-department/create-department.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Department } from '../../shared/models';
import { DepartmentService, BranchService, CompanyService } from '../../shared/services';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})

export class DepartmentComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('department-list-section') blockUI: NgBlockUI;
  departments: Department[] = [];
  departmentId: any;
  branchId: any;
  companies: any;
  branches: any;
  departmentFilterFormGroup: FormGroup;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;

  constructor(private departmentService: DepartmentService,
    private branchService: BranchService, private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private dialog: MatDialog, private injector: Injector) {
    super(injector);
  }

  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }


  buildForm() {
    this.departmentFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      branchId: [this.branchId, Validators.required],
    });
  }

  get f() { return this.departmentFilterFormGroup.controls; }


  onChangeCompany(companyId: any) {
    this.companyId = companyId;

    if (companyId) {
      this.getAllBranchByCompanyId();
    }
  }

  onChangeBranch(branchId: any) {
    this.branchId = branchId;
    if (branchId) {
      this.getAllDepartmentByBranchId();
    }
  }

  getDepartments() {
    this.submitted = true;
    if (this.departmentFilterFormGroup.invalid) {
      return;
    }
    this.branchId = this.f.branchId.value;

    this.getAllDepartmentByBranchId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }

  getAllBranchByCompanyId() {
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe((result: any) => {
      this.branches = result;
      this.branchId = result[0].id;
      this.buildForm();
      this.getAllDepartmentByBranchId();
    });
  }

  getAllDepartmentByBranchId() {
    this.blockUI.start();
    this.loading = true;
    this.departmentService.getAllDepartmentByBranchId(this.branchId).subscribe(result => {
      this.departments = result;
      this.totalItems = this.departments.length;
      this.loading = false;
      this.generateTotalItemsText();
      
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    })
  }

  createDepartment() {
    const createdepartmentDialogConfig = new MatDialogConfig;
    createdepartmentDialogConfig.disableClose = true;
    createdepartmentDialogConfig.autoFocus = true;
    createdepartmentDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createdepartmentDialogConfig.width = `${dialogWidth}%`;
    var now = window.screen.width;
    var department = new Department();
    department.companyId = this.companyId;
    department.branchId = this.branchId;
    createdepartmentDialogConfig.data = department;
    const createDepartmentDialog = this.dialog.open(CreateDepartmentComponent, createdepartmentDialogConfig)
    const successfullCreate = createDepartmentDialog.componentInstance.onDepartmentCreateEvent.subscribe((data) => {
      this.getAllDepartmentByBranchId();
    });
    createDepartmentDialog.afterClosed().subscribe(() => {
    });

  }

  editDepartment(department: Department) {
    const editDialogConfig = new MatDialogConfig();
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = department
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateDepartmentComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onDepartmentEditEvent.subscribe((data) => {
      this.getAllDepartmentByBranchId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteDepartment(department: Department): void {
    const confirmationDialogConfig = new MatDialogConfig();
    confirmationDialogConfig.data = "Are you sure to delete the department " + department.departmentName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteDepartment(department);
      }
    });
  }

  deleteDepartment(department: Department) {
    this.departmentService.deleteDepartment(department).subscribe((data) => {
      this.getAllDepartmentByBranchId();
    },
      (error) => {
        console.log(error);
      });
  }
}
