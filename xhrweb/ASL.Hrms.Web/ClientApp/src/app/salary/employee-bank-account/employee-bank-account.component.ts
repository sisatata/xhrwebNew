import { Component, OnInit, Injector } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { EmployeeBankAccount } from 'src/app/shared/models/payroll/employee-bank-account';
import { CreateEmployeeBankAccountComponent } from './create-employeebankaccount/create-employee-bank-account.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CompanyService, EmployeeBankAccountService } from 'src/app/shared/services';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';


@Component({
  selector: 'app-employee-bank-account',
  templateUrl: './employee-bank-account.component.html',
  styleUrls: ['./employee-bank-account.component.css']
})
export class EmployeeBankAccountComponent extends BaseAuthorizedComponent implements OnInit {
  @BlockUI('employeebankaccount-list-section') blockUI: NgBlockUI
  employeebankaccounts: EmployeeBankAccount[];
  employeebankaccountId: any;
  employeebankaccountFilterFormGroup: FormGroup
  companies: any;
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
isEmployee: boolean;
employeeId:any;
isAdmin:boolean;
isPayrollManager:boolean;
  constructor(private dialog: MatDialog,
    private formBuilder: FormBuilder,
    private employeebankaccountService: EmployeeBankAccountService,
    private companyService: CompanyService,
    private injector: Injector) {
    super(injector);
    this.isEmployee = this.authService.isEmployee;
    this.employeeId = this.authService.getLoggedInUserInfo().employeeId;
    this.isAdmin = this.authService.isAdmin;
    this.isPayrollManager = this.authService.isPayrollManger;

  }

  ngOnInit() {
    this.buildForm();
    if(!this.isEmployee)
    this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    if(this.isEmployee){
     
      this.getEmployeeBanAccountByEmployee();
    }
  }

  buildForm() {
    this.employeebankaccountFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required]

    });
  }

  get f() { return this.employeebankaccountFilterFormGroup.controls; }

  onChangeCompany(companyId: any) {
    this.companyId = companyId;
    if (companyId) {
      this.getAllEmployeeBankAccountByCompanyId();
    }
  }
  getEmployeeBanAccountByEmployee():void{
    this.loading = true;
    this.employeebankaccountService.getEmployeeBankAccountByEmployeeId(this.companyId, this.employeeId).subscribe(result=>{
      this.loading = false;
      this.employeebankaccounts = result;
      this.totalItems = this.employeebankaccounts.length;
      this.generateTotalItemsText();

    },()=>{
      this.loading = false;;
      
    })
  }
  getEmployeeBankAccount() {
    this.submitted = true;
    if (this.employeebankaccountFilterFormGroup.invalid) {
      return;
    }
    this.companyId = this.f.companyId.value;
    this.getAllEmployeeBankAccountByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;

    })
  }

  createEmployeeBankAccount() {
    const createEmployeeBankAccountDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    createEmployeeBankAccountDialogConfig.disableClose = true;
    createEmployeeBankAccountDialogConfig.autoFocus = true;
    createEmployeeBankAccountDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    createEmployeeBankAccountDialogConfig.width = `${dialogWidth}%`;
    var employeebankaccount = new EmployeeBankAccount();
    employeebankaccount.companyId = this.companyId;
    createEmployeeBankAccountDialogConfig.data = employeebankaccount;
    const createEmployeeBankAccountDialog = this.dialog.open(CreateEmployeeBankAccountComponent, createEmployeeBankAccountDialogConfig);
    const successfullCreate = createEmployeeBankAccountDialog.componentInstance.onEmployeeBankAccountCreateEvent.subscribe((data) => {
      this.getAllEmployeeBankAccountByCompanyId();
    });
    createEmployeeBankAccountDialog.afterClosed().subscribe(() => {
    });
  }
  editEmployeeBankAccount(employeeBankAccount: EmployeeBankAccount) {

    const editDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    editDialogConfig.disableClose = true;
    editDialogConfig.autoFocus = true;
    editDialogConfig.data = employeeBankAccount
    editDialogConfig.panelClass = 'xHR-dialog';
    const dialogWidth = window.screen.width <= 576 ? 90: 50;
    editDialogConfig.width =  `${dialogWidth}%`;

    const outletEditDialog = this.dialog.open(CreateEmployeeBankAccountComponent, editDialogConfig);
    const successFullEdit = outletEditDialog.componentInstance.onEmployeeBankAccountEditEvent.subscribe((data) => {
      this.getAllEmployeeBankAccountByCompanyId();
    });
    outletEditDialog.afterClosed().subscribe(() => {
    });
  }

  onDeleteEmployeeBankAccount(employeebankaccount: EmployeeBankAccount): void {
    const confirmationDialogConfig = new MatDialogConfig();
    // Setting different dialog configurations
    confirmationDialogConfig.data = 'Are you sure to delete the EmployeeBankAccount ' + employeebankaccount.accountNo;
    confirmationDialogConfig.panelClass = 'xHR-dialog';
    const confirmationDialog = this.dialog.open(ConfirmationDialogComponent, confirmationDialogConfig);

    confirmationDialog.afterClosed().subscribe((result) => {
      if (result != undefined) {
        this.deleteEmployeeBankAccount(employeebankaccount);
      }
    });
  }
  deleteEmployeeBankAccount(employeebankaccount: EmployeeBankAccount) {
    this.employeebankaccountService.deleteEmployeeBankAccount(employeebankaccount).subscribe((data) => {
      this.getAllEmployeeBankAccountByCompanyId();
    },
      (error) => {
        console.log(error);
      });
  }

  getAllEmployeeBankAccountById(brnchId) {
    this.employeebankaccountService.getEmployeeBankAccountById(brnchId).subscribe(result => {
      this.employeebankaccounts = result;
    })
  }

  getAllEmployeeBankAccountByCompanyId() {
    this.blockUI.start();
    this.loading = true;
    this.employeebankaccountService.getAllEmployeeBankAccountByCompanyId(this.companyId).subscribe(result => {
      this.employeebankaccounts = result;
      this.loading = false;
      this.totalItems = this.employeebankaccounts.length;
      this.generateTotalItemsText();

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
      this.loading = false;

    })

  }
}

