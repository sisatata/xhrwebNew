import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { EmployeeBankAccount } from 'src/app/shared/models';
import { CompanyService, EmployeeBankAccountService, BankBranchService, BankService, PaymentOptionService, EmployeeService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-create-employee-bank-account',
  templateUrl: './create-employee-bank-account.component.html',
  styleUrls: ['./create-employee-bank-account.component.css']
})
export class CreateEmployeeBankAccountComponent implements OnInit {
  onEmployeeBankAccountCreateEvent: EventEmitter<any> = new EventEmitter();
  onEmployeeBankAccountEditEvent: EventEmitter<any> = new EventEmitter();
  @BlockUI('bank-list-section') blockUI: NgBlockUI
  employeebankaccountCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  bankId: any;
  banks: any;
  bankBranchId: any;
  bankBranches: any;
  paymentOptions: any;
  employees: any;
  employeeId: any;
  employeeBankAccount: EmployeeBankAccount = new EmployeeBankAccount();
  employeebankaccountId: number;
  isEditMode = false;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private bankBranchService: BankBranchService,
    private bankService: BankService,
    private paymentOptionService: PaymentOptionService,
    private employeeService: EmployeeService,
    private dialogRef: MatDialogRef<CreateEmployeeBankAccountComponent>,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private employeebankaccountservice: EmployeeBankAccountService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.employeeBankAccount = new EmployeeBankAccount();
    if (isNaN(data)) {
      this.employeeBankAccount = new EmployeeBankAccount(data);
      this.companyId = this.employeeBankAccount.companyId;
      this.employeeId = this.employeeBankAccount.employeeId;
      this.bankId = this.employeeBankAccount.bankId;

    } else {

    }
    if (this.employeeBankAccount.id) {
      this.isEditMode = true;
    }
    this.buildForm();
    this.getAllCompanies();
    this.getAllEmployees();
    this.getAllBankByCompanyId();
    this.getAllBankBranchByBankId();
    this.getAllPaymentOptionByCompanyId();
  }

  ngOnInit() {
    this.getAllCompanies();
    this.getAllEmployees();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  //onChangeCompany() {
  //  this.companyId = this.f.companyId.value;
  //}

  getAllEmployees() {
    this.employeeService.getAllEmployees().subscribe((result: any) => {
      this.employees = result;
    });
  }
  buildForm() {

    this.employeebankaccountCreateForm = this.formBuilder.group({
      id: [this.employeeBankAccount.id],
      bankId: [this.employeeBankAccount.bankId],
      bankBranchId: [this.employeeBankAccount.bankBranchId],
      accountNo: [this.employeeBankAccount.accountNo, [Validators.maxLength(20)]],
      accountTitle: [this.employeeBankAccount.accountTitle, [Validators.maxLength(100)]],
      isPrimary: [this.employeeBankAccount.isPrimary],
      companyId: [this.employeeBankAccount.companyId],
      employeeId: [this.employeeBankAccount.employeeId],
      startDate: [this.employeeBankAccount.startDate],
      endDate: [this.employeeBankAccount.endDate],
      remarks: [this.employeeBankAccount.remarks, [Validators.maxLength(250)]],


    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.employeebankaccountCreateForm.invalid) {
      return;
    }
    if (this.employeeBankAccount.id === undefined) {

      this.createEmployeeBankAccount();
    }
    else {
      this.editEmployeeBankAccount();
    }
    //this.dialogRef.close();
  }

  createEmployeeBankAccount() {
    this.employeeBankAccount = new EmployeeBankAccount(this.employeebankaccountCreateForm.value);
    this.loading = true;
    this.employeeBankAccount.startDate = this.datePipe.transform(new Date(this.employeeBankAccount.startDate), 'yyyy-MM-dd');
    this.employeeBankAccount.endDate = this.datePipe.transform(new Date(this.employeeBankAccount.endDate), 'yyyy-MM-dd');
  
    this.employeebankaccountservice.createEmployeeBankAccount(this.employeeBankAccount).subscribe((data: any) => {
      this.onEmployeeBankAccountCreateEvent.emit(this.employeeBankAccount.id);

      if ((data as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('EmployeeBankAccount added successfully');
        this.dialogRef.close();

      }
      else {
        this.isFormValid = false;
        this.errorMessages = (data as any).message;
      }
      this.loading = false;
    }, (error: any) => {
      this.alertService.error(error);
      this.loading = false;
    });
  }


  editEmployeeBankAccount() {
    let newData = new EmployeeBankAccount(this.employeebankaccountCreateForm.value);
    if (this.employeeBankAccount !== null) {
      this.employeeBankAccount.id = newData.id;
      this.employeeBankAccount.bankId = newData.bankId;
      this.employeeBankAccount.bankBranchId = newData.bankBranchId;
      this.employeeBankAccount.accountNo = newData.accountNo;
      this.employeeBankAccount.accountTitle = newData.accountTitle;
      this.employeeBankAccount.isPrimary = newData.isPrimary;
      this.employeeBankAccount.companyId = newData.companyId;
      this.employeeBankAccount.employeeId = newData.employeeId;
      this.employeeBankAccount.startDate = newData.startDate;
      this.employeeBankAccount.endDate = newData.endDate;
      this.employeeBankAccount.remarks = newData.remarks;
      this.employeeBankAccount.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.employeeBankAccount.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.loading = true;
      this.employeebankaccountservice.editEmployeeBankAccount(this.employeeBankAccount).subscribe((data: any) => {
        this.onEmployeeBankAccountEditEvent.emit(this.employeeBankAccount.id)

        if ((data as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('EmployeeBankAccount updated successfully');
          this.dialogRef.close();

        }
        else {
          this.isFormValid = false;
          this.errorMessages = (data as any).message;
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

  }
  get f() { return this.employeebankaccountCreateForm.controls; }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    if (this.companyId) {
      this.getAllBankByCompanyId();
      this.getAllPaymentOptionByCompanyId();
    }
  }

  getAllBankByCompanyId() {
    this.blockUI.start();
    this.bankService.getAllBankByCompanyId(this.companyId).subscribe(result => {
      this.banks = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })
  }

  getAllPaymentOptionByCompanyId() {
    this.blockUI.start();
    this.paymentOptionService.getAllPaymentOptionByCompanyId(this.companyId).subscribe(result => {
      this.paymentOptions = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })
  }

  onChangeBank() {
    this.bankId = this.f.bankId.value;
    if (this.bankId) {
      this.getAllBankBranchByBankId();
    }
  }

  getAllBankBranchByBankId() {
    this.blockUI.start();
    this.bankBranchService.getAllBankBranchByBankId(this.bankId).subscribe(result => {
      this.bankBranches = result;
      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })

  }

}


