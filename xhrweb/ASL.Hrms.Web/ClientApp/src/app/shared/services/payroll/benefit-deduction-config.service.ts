import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bank } from '../../models/payroll/bank';
import { BenefitDeductionCode } from '../../models/payroll/benefit-deduction';
import { BaseService } from '../base.service';

import { BenefitDeductionConfig } from '../../models/payroll/benefit-deduction-config';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class BenefitDeductionConfigService extends BaseService {

  private benefitDeductionConfigBaseUrl = this.Base_API_URL + 'BenefitDeductionConfig';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.benefitDeductionConfigBaseUrl = this.appSettingsService.apiBaseUrl + 'BenefitDeductionConfig';
  }


  getAllBenefitDeductionConfigByCompanyId(companyId: any): Observable<BenefitDeductionConfig[]> {
    return this.http.get<BenefitDeductionConfig[]>(this.benefitDeductionConfigBaseUrl + '/companyId/' + companyId);
  }

  deleteBenefitDeductionConfig(benefitDeductionConfig: any) {
    let httpParams = new HttpParams().set('id', benefitDeductionConfig.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: benefitDeductionConfig
    };
    return this.http.delete(this.benefitDeductionConfigBaseUrl, requestHeader);
  }

  createBenefitDeductionConfig(benefitDeductionConfig: BenefitDeductionConfig) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.benefitDeductionConfigBaseUrl, benefitDeductionConfig, requestHeader);
  }
  editBenefitDeductionConfig(benefitDeductionConfig: BenefitDeductionConfig) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.benefitDeductionConfigBaseUrl, benefitDeductionConfig, requestHeader);
  }


}
