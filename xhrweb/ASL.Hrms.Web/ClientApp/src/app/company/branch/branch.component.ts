import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { Branch } from 'src/app/shared/models/company/branch';
import { CreateBranchComponent } from './create-branch/create-branch.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, BranchService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
@Component({
  selector: 'app-branch',
  templateUrl: './branch.component.html',
  styleUrls: ['./branch.component.css']
})
export class BranchComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('branch-list-section') blockUI: NgBlockUI
  branches: Branch[];
  branchId: any;
  branchFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private branchService: BranchService,
    private companyService: CompanyService,
    private injector: Injector) {
    super(injector);
  }
  ngOnInit() {
    this.buildForm();
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
  }
  buildForm() {
    this.branchFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]
    });
   
  }
  get f() { return this.branchFilterFormGroup.controls; }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllBranchByCompanyId();
    }
  }
  getBranch() {
    this.submitted = true;
    if (this.branchFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllBranchByCompanyId();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  createBranch() {
    const createBranchDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createBranchDialogConfig.disableClose = true;
    createBranchDialogConfig.autoFocus = true;
    createBranchDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createBranchDialogConfig.width = `${dialogWidth}%`;
    var branch = new Branch();
    branch.companyId = this.companyId;
    createBranchDialogConfig.data = branch;
    const createbranchDialog = this.dialog.open(CreateBranchComponent, createBranchDialogConfig);
    const successfullCreate = createbranchDialog.componentInstance.onBranchCreateEvent.subscribe((data) => {
      this.getAllBranchByCompanyId();
    });
    createbranchDialog.afterClosed().subscribe(() => {
    });
  }
  editBranch(branch: Branch) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = branch
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(CreateBranchComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onBranchEditEvent.subscribe((data) => {
      this.getAllBranchByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }
  onDeleteBranch(branch: Branch): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = "Are you sure to delete the branch " + branch.branchName;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteBranch(branch);
      }
    });
  }
  deleteBranch(branch: Branch) {
    this.branchService.deleteBranch(branch).subscribe((data) => {
      this.getAllBranchByCompanyId();
    },
      (error) => {
        console.log(error);
      });
  }
  getAllBranchById(brnchId) {
    this.branchService.getBranchById(brnchId).subscribe(result => {
      this.branches = result;
    })
  }
  getAllBranchByCompanyId() {
    this.blockUI.start();
  this.loading = true;
    this.branchService.getAllBranchByCompanyId(this.companyId).subscribe(result => {
      this.branches = result;
      this.loading = false;

      this.totalItems = this.branches.length;
      this.generateTotalItemsText();
       this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    })
  }
}
