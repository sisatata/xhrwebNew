import { Component, OnInit, Injector } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { BankBranch } from "src/app/shared/models/payroll/bank-branch";
import { CreateBankBranchComponent } from "./create-bankbranch/create-bank-branch.component";
import { ConfirmationDialogComponent } from "src/app/shared/components/confirmation-dialog/confirmation-dialog.component";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { CompanyService, BankBranchService } from "src/app/shared/services";
import { BlockUI, NgBlockUI } from "ng-block-ui";
import { BaseAuthorizedComponent } from "src/app/shared/components/base-authorized/base-authorized.component";
@Component({
  selector: "app-bank-branch",
  templateUrl: "./bank-branch.component.html",
  styleUrls: ["./bank-branch.component.css"],
})
export class BankBranchComponent
  extends BaseAuthorizedComponent
  implements OnInit
{
  @BlockUI("bankbranch-list-section") blockUI: NgBlockUI;
  bankbranchs: BankBranch[];
  bankbranchId: any;
  bankbranchFilterFormGroup: FormGroup;
  companies: any;
  submitted: boolean;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private bankbranchService: BankBranchService,
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
    this.bankbranchFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
    });
  }
  get f() {
    return this.bankbranchFilterFormGroup.controls;
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      //this.getAllBankBranchByCompanyId();
    }
  }
  getBankBranch() {
    this.submitted = true;
    if (this.bankbranchFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    //this.getAllBankBranchByCompanyId();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  createBankBranch() {
    const createBankBranchDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createBankBranchDialogConfig.disableClose = true;
    createBankBranchDialogConfig.autoFocus = true;
    createBankBranchDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createBankBranchDialogConfig.width = `${dialogWidth}%`;
    var bankbranch = new BankBranch();
    bankbranch.companyId = this.companyId;
    createBankBranchDialogConfig.data = bankbranch;
    const createBankBranchDialog = this.dialog.open(
      CreateBankBranchComponent,
      createBankBranchDialogConfig
    );
    const successfullCreate =
      createBankBranchDialog.componentInstance.onBankBranchCreateEvent.subscribe(
        (data) => {
          //this.getAllBankBranchByCompanyId();
        }
      );
    createBankBranchDialog.afterClosed().subscribe(() => {});
  }
  editBankBranch(bankbranch: BankBranch) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = bankbranch;
    editDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(
      CreateBankBranchComponent,
      editDialogConfig
    );
    const successFullEdit =
      outletEditDialog.componentInstance.onBankBranchEditEvent.subscribe(
        (data) => {
          //this.getAllBankBranchByCompanyId();
        }
      );
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  onDeleteBankBranch(bankbranch: BankBranch): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data =
      "Are you sure to delete the BankBranch " + bankbranch.branchName;
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteBankBranch(bankbranch);
      }
    });
  }
  deleteBankBranch(bankbranch: BankBranch) {
    this.bankbranchService.deleteBankBranch(bankbranch).subscribe(
      (data) => {
        //this.getAllBankBranchByCompanyId();
      },
      (error) => {
        console.log(error);
      }
    );
  }
  getAllBankBranchById(brnchId) {
    this.bankbranchService.getBankBranchById(brnchId).subscribe((result) => {
      this.bankbranchs = result;
    });
  }
}
