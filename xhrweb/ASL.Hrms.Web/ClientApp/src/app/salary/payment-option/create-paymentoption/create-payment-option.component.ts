import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PaymentOption } from 'src/app/shared/models';
import { CompanyService, PaymentOptionService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';

@Component({
  selector: 'app-create-payment-option',
  templateUrl: './create-payment-option.component.html',
  styleUrls: ['./create-payment-option.component.css']
})
export class CreatePaymentOptionComponent implements OnInit {
  onPaymentOptionCreateEvent: EventEmitter<any> = new EventEmitter();
  onPaymentOptionEditEvent: EventEmitter<any> = new EventEmitter();

  paymentoptionCreateForm: FormGroup
  submitted = false;
  companyId: any;
  companies: any;
  paymentOption: PaymentOption = new PaymentOption();
  paymentoptionId: number;
  isEditMode = false;

  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreatePaymentOptionComponent>,
    private formBuilder: FormBuilder,
    private paymentoptionservice: PaymentOptionService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.paymentOption = new PaymentOption();
    if (isNaN(data)) {
      this.paymentOption = new PaymentOption(data);
      //this.companyId = this.paymentOption.companyId;

    } else {

    }
    this.buildForm();
    this.getAllCompanies();
  }

  ngOnInit() {
    this.getAllCompanies();
  }

  getAllCompanies() {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }

  onChangeCompany() {
    this.companyId = this.f.companyId.value;
  }

  buildForm() {
    this.paymentoptionCreateForm = this.formBuilder.group({
      id: [this.paymentOption.id],
      paymentOptionId: [this.paymentOption.paymentOptionId],
      optionName: [this.paymentOption.optionName, [Validators.maxLength(50)]],
      optionCode: [this.paymentOption.optionCode, [Validators.maxLength(10)]],
      sortOrder: [this.paymentOption.sortOrder],
      isDeleted: [this.paymentOption.isDeleted],


    });
  }

  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.paymentoptionCreateForm.invalid) {
      return;
    }
    if (this.paymentOption.id === undefined) {

      this.createPaymentOption();
    }
    else {
      this.editPaymentOption();
    }
    this.dialogRef.close();
  }

  createPaymentOption() {
    this.paymentOption = new PaymentOption(this.paymentoptionCreateForm.value);
    this.paymentoptionservice.createPaymentOption(this.paymentOption).subscribe((data: any) => {
      this.onPaymentOptionCreateEvent.emit(this.paymentOption.id);
      this.alertService.success('PaymentOption added successfully');
    }, (error: any) => {
      this.alertService.error(error);
    });
  }


  editPaymentOption() {
    let newData = new PaymentOption(this.paymentoptionCreateForm.value);
    if (this.paymentOption !== null) {
      this.paymentOption.id = newData.id;
      this.paymentOption.paymentOptionId = newData.paymentOptionId;
      this.paymentOption.optionName = newData.optionName;
      this.paymentOption.optionCode = newData.optionCode;
      this.paymentOption.sortOrder = newData.sortOrder;
      this.paymentOption.isDeleted = newData.isDeleted;


      this.paymentoptionservice.editPaymentOption(this.paymentOption).subscribe((data: any) => {
        this.onPaymentOptionEditEvent.emit(this.paymentOption.id)
        this.alertService.success('PaymentOption updated successfully');
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
  get f() { return this.paymentoptionCreateForm.controls; }
}


