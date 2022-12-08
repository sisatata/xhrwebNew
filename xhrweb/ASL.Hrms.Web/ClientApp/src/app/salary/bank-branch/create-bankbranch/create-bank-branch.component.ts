import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BankBranch } from 'src/app/shared/models';
import { CompanyService, BankBranchService, BankService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Component({
  selector: 'app-create-bank-branch',
  templateUrl: './create-bank-branch.component.html',
  styleUrls: ['./create-bank-branch.component.css']
})
export class CreateBankBranchComponent implements OnInit {
  @BlockUI('bank-list-section') blockUI: NgBlockUI
  onBankBranchCreateEvent: EventEmitter<any> = new EventEmitter();
  onBankBranchEditEvent: EventEmitter<any> = new EventEmitter();

  bankbranchCreateForm: FormGroup
  submitted = false;
  companyId: any;
  bankId: any;
  banks: any;
  companies: any;
  bankBranch: BankBranch = new BankBranch();
  bankbranchId: number;
  isEditMode = false;

  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateBankBranchComponent>,
    private formBuilder: FormBuilder,
    private bankbranchservice: BankBranchService,
    private bankService: BankService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.bankBranch = new BankBranch();
    if (isNaN(data)) {
      this.bankBranch = new BankBranch(data);
      this.companyId = this.bankBranch.companyId;
      this.bankId = this.bankBranch.bankId;
    } else {

    }
    this.buildForm();
    this.getAllCompanies();
    this.getAllBankByCompanyId();
  }

  ngOnInit() {
    this.getAllCompanies();
    this.getAllBankByCompanyId();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  getAllBankByCompanyId() {
    this.blockUI.start();
    this.bankService.getAllBankByCompanyId(this.companyId).subscribe(result => {
      this.banks = result;
      //this.totalItems = this.banks.length;
      //this.generateTotalItemsText();

      this.blockUI.stop();
    }, error => {
      this.blockUI.stop();
    })

  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }

  buildForm() {
    this.bankbranchCreateForm = this.formBuilder.group({
      id: [this.bankBranch.id],
      branchName: [this.bankBranch.branchName, [Validators.required, Validators.maxLength(50)]],
      //branchNameLC: [this.bankBranch.branchNameLC, [Validators.maxLength(50)]],
      branchAddress: [this.bankBranch.branchAddress, [Validators.maxLength(250)]],
      contactPerson: [this.bankBranch.contactPerson, [Validators.maxLength(50)]],
      contactNumber: [this.bankBranch.contactNumber, [Validators.maxLength(20)]],
      contactEmailId: [this.bankBranch.contactEmailId, [Validators.maxLength(50)]],
      sortOrder: [this.bankBranch.sortOrder],
      companyId: [this.bankBranch.companyId],
      bankId: [this.bankBranch.bankId],
      isDeleted: [this.bankBranch.isDeleted],
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.bankbranchCreateForm.invalid) {
      return;
    }
    if (this.bankBranch.id === undefined) {

      this.createBankBranch();
    }
    else {
      this.editBankBranch();
    }
    this.dialogRef.close();
  }

  createBankBranch() {
    this.bankBranch = new BankBranch(this.bankbranchCreateForm.value);
    this.bankbranchservice.createBankBranch(this.bankBranch).subscribe((data: any) => {
      this.onBankBranchCreateEvent.emit(this.bankBranch.id);
      this.alertService.success('BankBranch added successfully');
    }, (error: any) => {
      this.alertService.error(error);
    });
  }


  editBankBranch() {
    let newData = new BankBranch(this.bankbranchCreateForm.value);
    if (this.bankBranch !== null) {
      this.bankBranch.id = newData.id;
      this.bankBranch.branchName = newData.branchName;
      this.bankBranch.branchNameLC = newData.branchNameLC;
      this.bankBranch.branchAddress = newData.branchAddress;
      this.bankBranch.contactPerson = newData.contactPerson;
      this.bankBranch.contactNumber = newData.contactNumber;
      this.bankBranch.contactEmailId = newData.contactEmailId;
      this.bankBranch.sortOrder = newData.sortOrder;
      this.bankBranch.companyId = newData.companyId;
      this.bankBranch.isDeleted = newData.isDeleted;


      this.bankbranchservice.editBankBranch(this.bankBranch).subscribe((data: any) => {
        this.onBankBranchEditEvent.emit(this.bankBranch.id)
        this.alertService.success('BankBranch updated successfully');
      }, (error: any) => {
        this.alertService.error(error);
      });
    }
  }

  close() {
    this.dialogRef.close();
  }
  showErrorMessage(error: any) {
    console.log(error);

  }
  get f() { return this.bankbranchCreateForm.controls; }
}


