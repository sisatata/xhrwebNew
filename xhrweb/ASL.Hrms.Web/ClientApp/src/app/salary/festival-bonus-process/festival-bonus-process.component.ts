import { DatePipe } from '@angular/common';
import { Injector } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatOption } from '@angular/material/core';
import { MatSelectChange } from '@angular/material/select';
import { FestivalBonusProcess, Guid } from 'src/app/shared/models';
import { BonsuConfigurationService, CommonLookUpTypeService, CompanyService, FinancialYearService } from 'src/app/shared/services';
import { AlertService } from 'src/app/shared/services/alert.service';
@Component({
  selector: 'app-festival-bonus-process',
  templateUrl: './festival-bonus-process.component.html',
  styleUrls: ['./festival-bonus-process.component.css']
})
export class FestivalBonusProcessComponent implements OnInit {
  fesivalBonusProccess: FestivalBonusProcess = new FestivalBonusProcess();
  festivalBonusFilterFormGroup: FormGroup;
  financialYearId: any;
  financialYears: any;
  financialYearName: any;
  companies: any;
  submitted: boolean;
  companyId: any = localStorage.getItem('companyId');
  isFormValid: boolean;
  errorMessages: any;
  loading: boolean = false;
  bonusTypeId: any;
  bonusDate: any;
  bonustypes: any[];
  constructor(
    private formBuilder: FormBuilder,
    private companyService: CompanyService,
    private alertService: AlertService,
    private financialYearService: FinancialYearService,
    private commonLookUpTypeService: CommonLookUpTypeService,
    private festivalBonusConfigService: BonsuConfigurationService,
    private datePipe: DatePipe,

  ) {

  }
  ngOnInit(): void {
    this.getAllCompanies();
    this.getBonusTypes();
    this.buildForm();
    this.getAllFinancialYearByCompanyId();
  }
  getBonusTypes(): void {
    this.commonLookUpTypeService.getCommonLookUpTypeByParentCode(this.companyId, 'FestivalBonusTypes').subscribe(res => {
      this.bonustypes = res;
    }, () => { })
  }
  getAllCompanies(): void {
    this.companyService.getAllCompanies().subscribe((result: any) => {
      this.companies = result;
    }, () => {
    })
  }
  onChangeCompany(companyId: Guid): void {
    this.companyId = companyId;

    if (companyId) {
      this.getAllFinancialYearByCompanyId();
    }
  }
  buildForm(): void {
    this.festivalBonusFilterFormGroup = this.formBuilder.group({
      companyId: [this.companyId, Validators.required],
      financialYearId: [this.fesivalBonusProccess.financialYearId, [Validators.required]],
      bonusTypeId: [this.fesivalBonusProccess.bonusTypeId, [Validators.required]],
      bonusDate: [this.fesivalBonusProccess.bonusDate, [Validators.required]],
    });
  }
  getAllFinancialYearByCompanyId(): void {
    this.financialYearService.getAllFinancialYearByCompanyId(this.companyId).subscribe(result => {
      this.financialYears = result;
      const runningYear = result.find(x => x.isRunningYear === true);
      this.financialYearId = runningYear.id;
      this.fesivalBonusProccess.financialYearId = this.financialYearId;

    })
  }
  onChangeFinancialYears(event: MatSelectChange): void {
    var financialYearName = (event.source.selected as MatOption).viewValue;
    var financialYearId = event.source.value
    this.financialYearId = financialYearId;
    this.financialYearName = financialYearName;
  }

  submitForm(): void {
    this.submitted = true;
    if (this.festivalBonusFilterFormGroup.invalid) {
      return;
    }
    else {
      this.processFestivalBonus();
    }
  }
  processFestivalBonus(): void {
    this.loading = true;
    this.fesivalBonusProccess = new FestivalBonusProcess(this.festivalBonusFilterFormGroup.value);
    this.fesivalBonusProccess.bonusDate = this.datePipe.transform(new Date(this.fesivalBonusProccess.bonusDate), 'yyyy-MM-dd');
    this.festivalBonusConfigService.bonusProcess(this.fesivalBonusProccess).subscribe(res => {
      if ((res as any).status === true) {
        this.isFormValid = true;
        this.alertService.success('Bonus successfully processed');
      }
      else {
        this.isFormValid = false;
        this.errorMessages = (res as any).message;
      }
      this.loading = false;
    }, () => {
      this.loading = false;
    })

  }
  get f() { return this.festivalBonusFilterFormGroup.controls; }

}
