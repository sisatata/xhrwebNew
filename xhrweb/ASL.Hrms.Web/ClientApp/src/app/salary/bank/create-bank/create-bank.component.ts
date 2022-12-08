import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CompanyService, BankService,BankBranchService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { Bank } from '../../../shared/models/index';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Component({
  selector: 'app-create-bank',
  templateUrl: './create-bank.component.html',
  styleUrls: ['./create-bank.component.css']
})
export class CreateBankComponent implements OnInit {
  onBankCreateEvent: EventEmitter<any> = new EventEmitter();
  onBankEditEvent: EventEmitter<any> = new EventEmitter();
  @BlockUI('common-lookup-type-section') blockUI: NgBlockUI
  bankCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  bank: Bank = new Bank();
  bankId: number;
  isEditMode = false;
  //hideme = [];
  //Index: any;
  //childList: any = [];
  isFormValid: boolean;
  errorMessages: any ;
loading: boolean = false;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private alertServiceSecondary: AlertService,
    private dialogRef: MatDialogRef<CreateBankComponent>,
    private formBuilder: FormBuilder,
    private bankservice: BankService,
    private bankBranchService: BankBranchService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.bank = new Bank();
    if (isNaN(data)) {
      this.bank = new Bank(data);
      this.companyId = this.bank.companyId;
    } else {

    }
    this.buildForm();
    this.getAllCompanies();
  }

  ngOnInit() {
    //this.onChangeCompany(this.companyId);
    this.getAllCompanies();
    
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result; 
    })
  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
    //if (companyId) {
    //  this.getAllBankByCompanyId();
    //}
  }

  buildForm() {
    this.bankCreateForm = this.formBuilder.group({
      companyId: [this.bank.companyId],
      bankName: [this.bank.bankName, [Validators.required, Validators.maxLength(250)]],
      //bankLocalizedName: [this.bank.bankLocalizedName, [Validators.maxLength(150)]],
      //shortName: [this.bank.shortName, [Validators.required, Validators.maxLength(50)]],
      sortOrder: [this.bank.sortOrder],
    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.bankCreateForm.invalid) {
      return;
    }
    if (this.bank.id === undefined) {

      this.createBank();
    }
    else {
      this.editBank();
    }
  //  this.dialogRef.close();
  }

  //public showChildList(index, code) {
  //  this.getAllBranchesByBankId(index, code);
  //  this.hideme[index] = !this.hideme[index];
  //  this.Index = index;
  //}

  //getAllBranchesByBankId(index: any, code: string) {
  //  if (this.companyId) {
  //    this.blockUI.start();
  //    this.bankBranchService.getAllBankBranchByBankId(this.companyId, code).subscribe(result => {
  //      this.childList[index] = result;
  //      this.blockUI.stop();
  //    }, error => {
  //      this.blockUI.stop();
  //    })
  //  }
  //}
  createBank() {
    this.bank = new Bank(this.bankCreateForm.value);
    this.loading = true;
    this.bankservice.createBank(this.bank).subscribe((data: any) => {
      this.onBankCreateEvent.emit(this.bank.id);
     
      if((data as any).status === true){
        this.isFormValid = true;
        this.alertService.success('Bank added successfully');
        setTimeout(()=>{ this.alertService.info('You can create bank branch by clicking "+" sign');}, 2000);
        //setTimeout(function(){  this.alertService.info('You can create bank branch by clicking "+" sign');}, 3000);
        
        this.dialogRef.close();

      }
      else{
        this.isFormValid = false;
        this.errorMessages = (data as  any).message;
      }
     this.loading = false;
    }, (error: any) => {
      this.alertService.error(error);
      this.loading =false;
    });
  }


  editBank() {
    let newData = new Bank(this.bankCreateForm.value);
    if (this.bank !== null) {
      this.bank.bankName = newData.bankName;
      //this.bank.bankLocalizedName = newData.bankLocalizedName;
      //this.bank.shortName = newData.shortName;
      this.bank.companyId = newData.companyId;
      this.bank.sortOrder = newData.sortOrder;
      this.loading = true;
      this.bankservice.editBank(this.bank).subscribe((data: any) => {
        this.onBankEditEvent.emit(this.bank.id)
       
        if((data as any).status === true){
          this.isFormValid = true;
          this.alertService.success('Bank updated successfully');
          this.dialogRef.close();

        }
        else{
          this.isFormValid = false;
          this.errorMessages = (data as  any).message;
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
  get f() { return this.bankCreateForm.controls; }

}


