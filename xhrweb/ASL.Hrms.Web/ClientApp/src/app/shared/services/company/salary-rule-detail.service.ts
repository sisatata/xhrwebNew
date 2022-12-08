import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SalaryRuleDetail } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class SalaryRuleDetailService extends BaseService {
  private salaryRuleDetailBaseUrl = this.Base_API_URL + 'SalaryRuleDetail';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.salaryRuleDetailBaseUrl = this.appSettingsService.apiBaseUrl + 'SalaryRuleDetail';
  }

  getSalaryRuleDetailListByCompany(companyId: any): Observable<SalaryRuleDetail[]> {
    return this.http.get<SalaryRuleDetail[]>(this.salaryRuleDetailBaseUrl + '/companyId/' + companyId);
  }

  getSalaryRuleDetailById(id: any): Observable<SalaryRuleDetail> {
    return this.http.get<SalaryRuleDetail>(this.salaryRuleDetailBaseUrl + id);
  }

  createSalaryRuleDetail(salaryRuleDetail: SalaryRuleDetail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.salaryRuleDetailBaseUrl, salaryRuleDetail, requestHeader);
  }

  updateSalaryRuleDetail(salaryRuleDetail: SalaryRuleDetail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.salaryRuleDetailBaseUrl, salaryRuleDetail, requestHeader);
  }

  deleteSalaryRuleDetail(salaryRuleDetail: SalaryRuleDetail) {
    let httpParams = new HttpParams().set('salaryRuleDetailId', salaryRuleDetail.id);
    var input = { id: salaryRuleDetail.id };
    var requestHeader = {
      headers: new HttpHeaders({
        'Content-Type': 'apllication/json', 'No-Auth': 'False'
      }),
      body: input
    };
    return this.http.delete(this.salaryRuleDetailBaseUrl, requestHeader);
  }

}
