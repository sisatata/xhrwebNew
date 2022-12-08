import { Inject } from '@angular/core';
import { Component, EventEmitter, Injector, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseAuthorizedComponent } from 'src/app/shared/components/base-authorized/base-authorized.component';
import { IncomeTaxPayerType } from 'src/app/shared/models';
import { CompanyService, IncomeTaxPayerTypeService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-income-tax-payer-type',
  templateUrl: './create-income-tax-payer-type.component.html',
  styleUrls: ['./create-income-tax-payer-type.component.css']
})
export class CreateIncomeTaxPayerTypeComponent implements OnInit {
  onIncomeTaxPayerTypeCreateEvent: EventEmitter<any> = new EventEmitter();
  onIncomeTaxPayerTypeEditEvent: EventEmitter<any> = new EventEmitter();
  incomeTaxPayerTypeCreateForm: FormGroup;
  submitted: boolean;
  incomeTaxPayerType: IncomeTaxPayerType;
  isEditMode: boolean;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean;
  companies: any;
  duplicateMessage: string = 'Income tax payer type code already exist. Please try another';
  isDuplicate: boolean = false;
  incomeTaxPayerTypes: any;
  payerTypeCodeInEditMode: string;
  constructor(
    private injector: Injector,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateIncomeTaxPayerTypeComponent>,
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private incomeTaxPayerTypeService: IncomeTaxPayerTypeService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.incomeTaxPayerType = new IncomeTaxPayerType();
    if (isNaN(data)) {
      this.incomeTaxPayerType = new IncomeTaxPayerType(data);
    }
    if (this.incomeTaxPayerType.id) {
      this.isEditMode = true;
      this.checkDuplicatesInEditMode();
    }

  }
  ngOnInit(): void {
    this.buildForm();
    this.getAllCompanies();
    this.getAllIncomeTaxPayerTypes();
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
      this.loading = false;
    })
  }
  buildForm() {
    this.incomeTaxPayerTypeCreateForm = this.formBuilder.group({
      companyId: [this.incomeTaxPayerType.companyId],
      payerTypeCode: [this.incomeTaxPayerType.payerTypeCode, [Validators.required, Validators.maxLength(50)]],
      payerTypeName: [this.incomeTaxPayerType.payerTypeName, [Validators.required, Validators.maxLength(50)]],
      isActive: [this.incomeTaxPayerType.isActive],
      remarks: [this.incomeTaxPayerType.remarks, [Validators.maxLength(50)]],
    });
  }
  get f() { return this.incomeTaxPayerTypeCreateForm.controls; }
  close(): void {
    this.dialogRef.close();
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.checkDuplicates()) {
      return;
    }
    if (this.incomeTaxPayerTypeCreateForm.invalid) {
      return;
    }
    else if (this.incomeTaxPayerType.id === undefined || this.incomeTaxPayerType.id === null) {
      this.createIncomeTaxPayerType();
    }
    else {
      this.editIncomeTaxPayerTypeDialogConfig();
    }
  }
  createIncomeTaxPayerType(): void {
    this.incomeTaxPayerType = new IncomeTaxPayerType(this.incomeTaxPayerTypeCreateForm.value);
    this.loading = true;
    this.incomeTaxPayerTypeService.createIncomeTaxPayerType(this.incomeTaxPayerType).subscribe(result => {
      this.onIncomeTaxPayerTypeCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Income tax payer type created successfully');
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (result as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })
  }
  editIncomeTaxPayerTypeDialogConfig(): void {
    let newData = new IncomeTaxPayerType(this.incomeTaxPayerTypeCreateForm.value);
    if (this.incomeTaxPayerType !== null) {
      this.incomeTaxPayerType.isActive = newData.isActive;
      this.incomeTaxPayerType.payerTypeCode = newData.payerTypeCode;
      this.incomeTaxPayerType.payerTypeName = newData.payerTypeName;
      this.incomeTaxPayerType.remarks = newData.remarks;
      this.loading = true;
      this.incomeTaxPayerTypeService.editIncomeTaxPayerType(this.incomeTaxPayerType).subscribe(result => {
        this.onIncomeTaxPayerTypeEditEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('Income tax payer type updated successfully');
          this.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (result as any).message;
        }
        this.loading = false;
      }, () => {
        this.loading = false;
      })
    }
  }
  checkDuplicates(): boolean {

    let checkDuplicate = this.incomeTaxPayerTypes.findIndex(item => item.payerTypeCode === this.incomeTaxPayerTypeCreateForm.value.payerTypeCode);

    if (checkDuplicate !== -1 && (this.payerTypeCodeInEditMode !== this.incomeTaxPayerTypeCreateForm.value.payerTypeCode)) {
      this.isDuplicate = true;
    }
    else {
      this.isDuplicate = false;
    }
    return this.isDuplicate;

  }
  getAllIncomeTaxPayerTypes(): void {

    this.incomeTaxPayerTypeService.getAllIncomeTaxPayerType(this.incomeTaxPayerType.companyId).subscribe(result => {
      this.incomeTaxPayerTypes = result;



    }, () => {

    })
  }
  onFocus(): void {
    this.isDuplicate = false;
  }
  checkDuplicatesInEditMode(): void {
    this.payerTypeCodeInEditMode = this.incomeTaxPayerType.payerTypeCode;
  }
}
