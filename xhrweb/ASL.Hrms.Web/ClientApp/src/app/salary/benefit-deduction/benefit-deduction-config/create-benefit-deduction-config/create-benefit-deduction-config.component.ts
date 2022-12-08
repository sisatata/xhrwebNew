import { DatePipe } from '@angular/common';
import { Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { subscribeOn } from 'rxjs/operators';
import { BenefitDeductionConfig, BenefitDeductionInterval } from 'src/app/shared/models';
import { BenefitDeductionConfigService, BenefitDeductionIntervalService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-create-benefit-deduction-config',
  templateUrl: './create-benefit-deduction-config.component.html',
  styleUrls: ['./create-benefit-deduction-config.component.css']
})
export class CreateBenefitDeductionConfigComponent implements OnInit {
  onBenefitDeductionConfigCreateEvent: EventEmitter<any> = new EventEmitter();
  onBenefitDeductionConfigEditEvent: EventEmitter<any> = new EventEmitter();
  benefitDeductionConfig: BenefitDeductionConfig;
  benefitDeductionIntervals: BenefitDeductionInterval[];
  benefitDeductionConfigCreateForm: FormGroup;
  isEditMode: Boolean;
  submitted: boolean = false;
  benefitDeductionCode: any;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  constructor(
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateBenefitDeductionConfigComponent>,
    private formBuilder: FormBuilder,
    private benefitDeductionIntervalService: BenefitDeductionIntervalService,
    private datePipe: DatePipe,
    private benefitDeductionConfigService: BenefitDeductionConfigService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.benefitDeductionConfig = new BenefitDeductionConfig();
    if (isNaN(data)) {
      this.benefitDeductionConfig = new BenefitDeductionConfig(data);
      this.benefitDeductionConfig.benefitDeductionCode = data.benefitDeductionCode;
      this.benefitDeductionCode = data.benefitDeductionCode;
    } else {
    }
    if (this.benefitDeductionConfig.id) {
      this.isEditMode = true;
    }
  }
  ngOnInit(): void {
    this.getAllBenefitDeductionInterval();
    this.buildForm();
  }
  get f() { return this.benefitDeductionConfigCreateForm.controls; }
  getAllBenefitDeductionInterval(): void {
    this.loading = true;
    this.benefitDeductionIntervalService.getAllBenefitDeductionInterval().subscribe(result => {
      this.benefitDeductionIntervals = result;
      this.loading = false;
    }, () => { this.loading = false; })
  }
  buildForm(): void {
    this.benefitDeductionConfigCreateForm = this.formBuilder.group({
      id: [this.benefitDeductionConfig.id],
      benefitDeductionCode: [{ value: this.benefitDeductionConfig.benefitDeductionCode, disabled: true }],
      name: [this.benefitDeductionConfig.name, [Validators.required, Validators.maxLength(50)]],
      description: [this.benefitDeductionConfig.description, [Validators.required, Validators.maxLength(150)]],
      type: [this.benefitDeductionConfig.type, [Validators.required]],
      basicOrGross: [this.benefitDeductionConfig.basicOrGross],
      calculationType: [this.benefitDeductionConfig.calculationType],
      percentOfBasicOrGross: [this.benefitDeductionConfig.percentOfBasicOrGross],
      fixedAmount: [this.benefitDeductionConfig.fixedAmount],
      startDate: [this.benefitDeductionConfig.startDate , [Validators.required]],
      endDate: [this.benefitDeductionConfig.endDate , [Validators.required]],
      intervalId: [this.benefitDeductionConfig.intervalId],
      companyId: [this.benefitDeductionConfig.companyId],
      isCalculateSalary: [this.benefitDeductionConfig.isCalculateSalary],
      isActive: [this.benefitDeductionConfig.isActive],
    });
  }
  close() {
    this.dialogRef.close();
  }
  onSubmit(): void {
    this.submitted = true;
    if (this.benefitDeductionConfigCreateForm.invalid)
      return
    else if (this.benefitDeductionConfig.id === null || this.benefitDeductionConfig.id === undefined) {
      this.createBenefitDeductionConfiguration();
    }
    else {
      this.editBenefitDeductionConfiguration();
    }
  }
  createBenefitDeductionConfiguration(): void {
    this.loading = true;
    this.benefitDeductionConfig = new BenefitDeductionConfig(this.benefitDeductionConfigCreateForm.value);
    this.benefitDeductionConfig.benefitDeductionCode = this.benefitDeductionCode;
    this.benefitDeductionConfig.startDate =  this.datePipe.transform(new Date(this.benefitDeductionConfig.startDate), 'yyyy-MM-dd');
    this.benefitDeductionConfig.endDate = this.datePipe.transform(new Date(this.benefitDeductionConfig.endDate), 'yyyy-MM-dd');
    this.benefitDeductionConfigService.createBenefitDeductionConfig(this.benefitDeductionConfig).subscribe(result => {
      this.onBenefitDeductionConfigCreateEvent.emit((result as any).id);
      if ((result as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Benefit deduction configuration created successfully');
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
  editBenefitDeductionConfiguration(): void {
    let newData = new BenefitDeductionConfig(this.benefitDeductionConfigCreateForm.value);
    if (this.benefitDeductionConfig !== null) {
      this.benefitDeductionConfig.calculationType = newData.calculationType;
      this.benefitDeductionConfig.description = newData.description;
      this.benefitDeductionConfig.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.benefitDeductionConfig.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.benefitDeductionConfig.fixedAmount = newData.fixedAmount;
      this.benefitDeductionConfig.intervalId = newData.intervalId;
      this.benefitDeductionConfig.basicOrGross = newData.basicOrGross;
      this.benefitDeductionConfig.percentOfBasicOrGross = newData.percentOfBasicOrGross;
      this.benefitDeductionConfig.isActive = newData.isActive;
      this.benefitDeductionConfig.isCalculateSalary = newData.isCalculateSalary;
      this.benefitDeductionConfig.name = newData.name;
      this.benefitDeductionConfig.type = newData.type;
      this.loading = true;
      this.benefitDeductionConfigService.editBenefitDeductionConfig(this.benefitDeductionConfig).subscribe(result => {
        this.onBenefitDeductionConfigEditEvent.emit((result as any).id);
        if ((result as any).status === true) {
          this.isFormValid = true;
          this.alertService.success('Benefit deduction configuration updated successfully');
          this.dialogRef.close();
        }
        else {
          this.isFormValid = false;
          this.errorMessages = (result as any).message;
        }
        this.loading = false;
      }, () => { this.loading = false; })
    }
  }
}
