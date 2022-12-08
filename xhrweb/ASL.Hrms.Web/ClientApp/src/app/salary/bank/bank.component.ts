import { Component, OnInit, Injector } from "@angular/core";
import { MatDialog, MatDialogConfig } from "@angular/material/dialog";
import { Bank } from "src/app/shared/models/payroll/bank";
import { BankBranch } from "src/app/shared/models/payroll/bank-branch";
import { CreateBankComponent } from "./create-bank/create-bank.component";
import { CreateBankBranchComponent } from "../bank-branch/create-bankbranch/create-bank-branch.component";
import { ConfirmationDialogComponent } from "src/app/shared/components/confirmation-dialog/confirmation-dialog.component";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { CompanyService } from "src/app/shared/services";
import { BlockUI, NgBlockUI } from "ng-block-ui";
import { BaseAuthorizedComponent } from "src/app/shared/components/base-authorized/base-authorized.component";
import { BankService } from "../../shared/services/index";
import { BankBranchService } from "../../shared/services/index";
import { Branch } from "../../shared/models/index";
import { AlertService } from "src/app/shared/services/alert.service";
@Component({
  selector: "app-bank",
  templateUrl: "./bank.component.html",
  styleUrls: ["./bank.component.css"],
})
export class BankComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI("bank-list-section") blockUI: NgBlockUI;
  banks: Bank[];
  bankId: any;
  bankFilterFormGroup: FormGroup;
  companies: any;
  submitted: boolean;
  Index: any;
  childList: any = [];
  hideme = [];
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private bankService: BankService,
    private bankBranchService: BankBranchService,
    private alertService: AlertService,
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
    this.bankFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
    });
  }
  get f() {
    return this.bankFilterFormGroup.controls;
  }
  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllBankByCompanyId();
    }
  }
  getBank() {
    this.submitted = true;
    if (this.bankFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllBankByCompanyId();
  }
  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    });
  }
  public showChildList(index, bankId) {
    this.getAllBrancgesByBankId(index, bankId);
    this.hideme[index] = !this.hideme[index];
    this.Index = index;
  }
  getAllBrancgesByBankId(index: any, bankId: any) {
    if (this.companyId) {
      //this.blockUI.start();
      this.bankBranchService.getAllBankBranchByBankId(bankId).subscribe(
        (result) => {
          this.childList[index] = result;
          //this.blockUI.stop();
        },
        (error) => {
          this.blockUI.stop();
        }
      );
    }
  }
  createBranchFromBankList(bankId: any, index: any) {
    const createBankBranchDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createBankBranchDialogConfig.disableClose = true;
    createBankBranchDialogConfig.autoFocus = true;
    createBankBranchDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createBankBranchDialogConfig.width = `${dialogWidth}%`;
    var bankbranch = new BankBranch();
    bankbranch.companyId = this.companyId;
    bankbranch.bankId = bankId;
    createBankBranchDialogConfig.data = bankbranch;
    const createBankBranchDialog = this.dialog.open(
      CreateBankBranchComponent,
      createBankBranchDialogConfig
    );
    const successfullCreate =
      createBankBranchDialog.componentInstance.onBankBranchCreateEvent.subscribe(
        (data) => {
          this.getAllBrancgesByBankId(index, bankId);
        }
      );
    createBankBranchDialog.afterClosed().subscribe(() => {});
  }
  editBankBranch(bankbranch: BankBranch, index: any) {
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
          this.getAllBrancgesByBankId(index, bankbranch.bankId);
        }
      );
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  createBank() {
    const createBankDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createBankDialogConfig.disableClose = true;
    createBankDialogConfig.autoFocus = true;
    createBankDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    createBankDialogConfig.width = `${dialogWidth}%`;
    var bank = new Bank();
    bank.companyId = this.companyId;
    createBankDialogConfig.data = bank;
    const createBankDialog = this.dialog.open(
      CreateBankComponent,
      createBankDialogConfig
    );
    const successfullCreate =
      createBankDialog.componentInstance.onBankCreateEvent.subscribe((data) => {
        this.getAllBankByCompanyId();
        this.hideAllChilds();
      });
    createBankDialog.afterClosed().subscribe(() => {});
  }
  editBank(bank: Bank) {
    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = bank;
    editDialogConfig.panelClass = "xHR-dialog";
    const dialogWidth = window.screen.width <= 576 ? 90 : 50;
    editDialogConfig.width = `${dialogWidth}%`;
    const outletEditDialog = this.dialog.open(
      CreateBankComponent,
      editDialogConfig
    );
    const successFullEdit =
      outletEditDialog.componentInstance.onBankEditEvent.subscribe((data) => {
        this.getAllBankByCompanyId();
        this.hideAllChilds();
      });
    outletEditDialog.afterClosed().subscribe(() => {});
  }
  onDeleteBank(bank: Bank): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data =
      "Are you sure to delete the Bank " + bank.bankName;
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteBank(bank);
      }
    });
  }
  deleteBank(bank: Bank) {
    this.bankService.deleteBank(bank).subscribe(
      (data) => {
        this.getAllBankByCompanyId();
      },
      (error) => {
        console.log(error);
      }
    );
  }
  getAllBankById(brnchId) {
    this.bankService.getBankById(brnchId).subscribe((result) => {
      this.banks = result;
    });
  }
  getAllBankByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.bankService.getAllBankByCompanyId(this.companyId).subscribe(
      (result) => {
        this.banks = result;
        this.totalItems = this.banks.length;
        this.generateTotalItemsText();
        this.loading = false;
        this.blockUI.stop();
      },
      (error) => {
        this.blockUI.stop();
        this.loading = false;
      }
    );
  }
  onDeleteBankBranch(bank: any, index: any): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data =
      "Are you sure to delete the Branch " + bank.branchName;
    confirmationDialogConfig.panelClass = "xHR-dialog";
    const confirmationDialog = this.dialog.open(
      ConfirmationDialogComponent,
      confirmationDialogConfig
    );
    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteBankBranch(bank);
      }
    });
  }
  deleteBankBranch(bank: any): void {
    this.bankBranchService.deleteBankBranch(bank).subscribe((result) => {
      if ((result as any).status === true) {
        this.alertService.success("Bank Branch deleted successfully");
        this.hideAllChilds();
      }
    });
  }
  hideAllChilds(): void {
    for (let i = 0; i < this.banks.length; i++) {
      this.hideme[i] = false;
    }
  }
}
