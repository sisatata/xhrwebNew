import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bank } from '../../models/payroll/bank';
import { BenefitDeductionCode } from '../../models/payroll/benefit-deduction';
import { BaseService } from '../base.service';

import { BenefitDeductionConfig } from '../../models/payroll/benefit-deduction-config';

import { BenefitDeductionInterval } from '../../models/payroll/benefit-deduction-interval';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class BenefitDeductionIntervalService extends BaseService {

  private benefitDeductionIntervalBaseUrl = this.Base_API_URL + 'BenefitDeductionInterval/';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.benefitDeductionIntervalBaseUrl = this.appSettingsService.apiBaseUrl + 'BenefitDeductionInterval/';
  }


  getAllBenefitDeductionInterval(): Observable<BenefitDeductionInterval[]> {
    return this.http.get<BenefitDeductionInterval[]>(this.benefitDeductionIntervalBaseUrl);
  }



}
