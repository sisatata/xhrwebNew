import { DatePipe } from '@angular/common';
import { AfterViewInit, Component, EventEmitter, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FestivalBonus } from 'src/app/shared/models';
import { BonsuConfigurationService, CommonLookUpTypeService, CompanyService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
import { CreateEmployeeSalaryComponent } from '../../employee-salary/create-employee-salary/create-employee-salary.component';
@Component({
  selector: 'app-create-festival-bonus-config',
  templateUrl: './create-festival-bonus-config.component.html',
  styleUrls: ['./create-festival-bonus-config.component.css']
})
export class CreateFestivalBonusConfigComponent implements OnInit {
  onFestivalBonusConfigCreateEvent: EventEmitter<any> = new EventEmitter();
  onFestivalBonusConfigEditEvent: EventEmitter<any> = new EventEmitter();
  submitted = false;
  festivalBonusConfigCreateForm: FormGroup;
  festivalBonusConfig: FestivalBonus;
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  companyId: any;
  isEditMode: boolean; relegions: any = [];
  companies: any;
  partials = [{ value: 'Day Wise', id: 10 }, { value: 'Month Wise', id: 20 }, { value: 'Year Wise', id: 30 }];
  basciOrGrosses = [ { value: 'Basic', id: 10 }, { value: 'Gross', id: 20 }];
  religions: any;
  showPartial: boolean = true;;
  defaultValue: any;
  basicOrGross: any;
  partialOn:any;
  defaultPartialOn =10;
  defaultBasicOrGross: number = 10;
  showFixedAmount: boolean = true;
  constructor(
    private companyService: CompanyService,
    private alertService: AlertService,
    private dialogRef: MatDialogRef<CreateEmployeeSalaryComponent>,
    private formBuilder: FormBuilder,
    private datePipe: DatePipe,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private festivalBonusConfigService: BonsuConfigurationService,
    @Inject(MAT_DIALOG_DATA) data) {
    this.festivalBonusConfig = new FestivalBonus();
    if (isNaN(data)) {
      this.companyId = data.companyId;
      this.festivalBonusConfig = new FestivalBonus(data);
    }
    if (this.festivalBonusConfig.id) {
      this.isEditMode = true;
      this.showPartial = this.festivalBonusConfig.partialOnId !== 0 ? true : false;
    }
  }
  ngOnInit(): void {
    this.getAllCompanies();
   // this.getAllReligions();
    this.buildForm();
    this.basicOrGross = this.isEditMode ? this.festivalBonusConfig.basicOrGrossId : this.defaultBasicOrGross;
    this.partialOn = this.isEditMode ? this.festivalBonusConfig.partialOnId : this.defaultPartialOn;
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
    })
  }
  getAllReligions() {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, "Religion").subscribe(result => {
      this.religions = result;
    }, () => {
    });
  }
  buildForm(): void {
    this.festivalBonusConfigCreateForm = this.formBuilder.group({
      companyId: [this.companyId, [Validators.required]],
      startDate: [this.festivalBonusConfig.startDate, [Validators.required]],
      endDate: [this.festivalBonusConfig.endDate, [Validators.required]],
      religionId: [this.festivalBonusConfig.religionId],
      rangeFromInMonth: [this.festivalBonusConfig.rangeFromInMonth, [Validators.required]],
      rangeToInMonth: [this.festivalBonusConfig.rangeToInMonth, [Validators.required]],
      basicOrGrossId: [this.festivalBonusConfig.basicOrGrossId,],
      percentOfBasicOrGross: [this.festivalBonusConfig.percentOfBasicOrGross],
      fixedAmount: [this.festivalBonusConfig.fixedAmount],
      isPaidFull: [this.festivalBonusConfig.isPaidFull],
      partialOnId: [this.festivalBonusConfig.partialOnId],
      remarks: [this.festivalBonusConfig.remarks, [Validators.maxLength(250)]],
    });
  }
  onSubmit(): void {
   this.submitted = true;
    if (this.festivalBonusConfigCreateForm.invalid) {
      return;
    }
    if (this.festivalBonusConfig.id === null || this.festivalBonusConfig.id === undefined) {
      
      this.createFestivalBonusConfig();
    }
    else {
     
      this.editFestivalBonusConfig();
    }
  }
  createFestivalBonusConfig(): void {
    this.festivalBonusConfig = new FestivalBonus(this.festivalBonusConfigCreateForm.value);
    this.festivalBonusConfig.startDate = this.datePipe.transform(new Date(this.festivalBonusConfig.startDate), 'yyyy-MM-dd');
    this.festivalBonusConfig.endDate = this.datePipe.transform(new Date(this.festivalBonusConfig.endDate), 'yyyy-MM-dd');
    this.festivalBonusConfig.basicOrGrossId = +this.festivalBonusConfig.basicOrGrossId;
    this.festivalBonusConfig.partialOnId = +this.festivalBonusConfig.partialOnId;
    this.festivalBonusConfig.partialOnId = this.showPartial === false ? 0 : this.festivalBonusConfig.partialOnId;
    this.loading = true;
    this.festivalBonusConfigService.createFestivalBonusConfig(this.festivalBonusConfig).subscribe(res => {
      this.onFestivalBonusConfigCreateEvent.emit((res as any).id);
      if ((res as any).status === true) {
        this.alertService.success('Bonus config successfully created');
        this.isFormValid = true;
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    });
  }
  editFestivalBonusConfig(): void {
    let newData = new FestivalBonus(this.festivalBonusConfigCreateForm.value);
    let id = this.festivalBonusConfig.id;
    if (this.festivalBonusConfig !== null) {
      this.festivalBonusConfig = Object.assign({}, newData);
      this.festivalBonusConfig.startDate = this.datePipe.transform(new Date(newData.startDate), 'yyyy-MM-dd');
      this.festivalBonusConfig.endDate = this.datePipe.transform(new Date(newData.endDate), 'yyyy-MM-dd');
      this.festivalBonusConfig.basicOrGrossId = +this.festivalBonusConfig.basicOrGrossId;
      this.festivalBonusConfig.partialOnId = +this.festivalBonusConfig.partialOnId;
      this.festivalBonusConfig.partialOnId = this.showPartial === false ? 0 : this.festivalBonusConfig.partialOnId;
      this.festivalBonusConfig.id = id;
    }
    ;
    this.loading = true;
    this.festivalBonusConfigService.editFestivalBonusConfig(this.festivalBonusConfig).subscribe(res => {
      this.onFestivalBonusConfigEditEvent.emit((res as any).id);
      if ((res as any).status === true) {
        this.alertService.success('Bonus config successfully updated');
        this.isFormValid = true;
        this.close();
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    });
  }
  get f() { return this.festivalBonusConfigCreateForm.controls; }
  close(): void {
    this.dialogRef.close();
  }
  showOptions(data: any): void {
    this.showPartial = data.checked ? false : true;
   
  }
 
}
