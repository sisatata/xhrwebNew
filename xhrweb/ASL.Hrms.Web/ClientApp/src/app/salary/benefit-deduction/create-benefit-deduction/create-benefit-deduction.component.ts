import { Component, OnInit, EventEmitter, Inject } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BenefitDeductionCode, EmployeeSalary } from 'src/app/shared/models';
import { CompanyService, EmployeeSalaryService, PaymentOptionService, EmployeeService, SalaryStructureService, DesignationService, BenefitDeductionService } from 'src/app/shared/services';
import { AlertService } from '../../../shared/services/alert.service';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
@Component({
  selector: 'app-create-benefit-deduction',
  templateUrl: './create-benefit-deduction.component.html',
  styleUrls: ['./create-benefit-deduction.component.css']
})
export class CreateBenefitDeductionComponent implements OnInit {
  onBenefitDeductionCodeCreateEvent: EventEmitter<any> = new EventEmitter();
  onBenefitDeductionEditEvent: EventEmitter<any> = new EventEmitter();
  benefitDeductionCodeCreateForm: FormGroup
  companies: any;
  companyId: any = localStorage.getItem('companyId');
  benefitDeduction: BenefitDeductionCode = new BenefitDeductionCode();
  submitted: boolean;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  isEditMode: boolean;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateBenefitDeductionComponent>,
    private formBuilder: FormBuilder,
    private benefitDeductionService: BenefitDeductionService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.benefitDeduction = new BenefitDeductionCode();
    if (isNaN(data)) {
      this.benefitDeduction = new BenefitDeductionCode(data);
    }
   
    if (this.benefitDeduction.id) {
      this.isEditMode = true;
    }
    this.buildForm();
    this.getAllCompanies();
  }
  ngOnInit(): void {
    this.getAllCompanies();
  }
  getAllCompanies():void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    })
  }
  buildForm() {
    this.benefitDeductionCodeCreateForm = this.formBuilder.group({
      companyId: [this.benefitDeduction.companyId],
      benifitDeductionCode: [this.benefitDeduction.benifitDeductionCode, [Validators.required, Validators.maxLength(50)]],
      benifitDeductionCodeName: [this.benefitDeduction.benifitDeductionCodeName, [Validators.required, Validators.maxLength(50)]],
    });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.benefitDeductionCodeCreateForm.invalid) {
      return;
    }
    if (this.benefitDeduction.id === undefined) {
      this.createBenefitDeductionCode();
    }
    else {
      this.editBenefitDeductionCode();
    }
    // this.dialogRef.close();
  }
  createBenefitDeductionCode():void {
    this.benefitDeduction = new BenefitDeductionCode(this.benefitDeductionCodeCreateForm.value);
    this.loading = true;
    this.benefitDeductionService.createBenefitDeductionCode(this.benefitDeduction).subscribe(result => {
      this.onBenefitDeductionCodeCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Benefit deduction code created successfully');
        this.dialogRef.close();
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
  close() {
    this.dialogRef.close();
  }
  get f() { return this.benefitDeductionCodeCreateForm.controls; }
  editBenefitDeductionCode():void {
    let newData = new BenefitDeductionCode(this.benefitDeductionCodeCreateForm.value);
    if (this.benefitDeduction !== null) {
      this.benefitDeduction.benifitDeductionCode = newData.benifitDeductionCode;
      this.benefitDeduction.benifitDeductionCodeName = newData.benifitDeductionCodeName;
    }
    this.loading = true;
    this.benefitDeductionService.editBenefitDeductionCode(this.benefitDeduction).subscribe(result => {
      this.onBenefitDeductionCodeCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Benefit deduction code updated successfully');
        this.dialogRef.close();
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
