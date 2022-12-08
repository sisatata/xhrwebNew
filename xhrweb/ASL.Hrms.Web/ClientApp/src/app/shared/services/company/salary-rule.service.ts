import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SalaryRule } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class SalaryRuleService extends BaseService {
  private salaryRuleBaseUrl = this.Base_API_URL + 'SalaryRule';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.salaryRuleBaseUrl = this.appSettingsService.apiBaseUrl + 'SalaryRule';
  }

  getSalaryRuleListByCompany(companyId: any): Observable<SalaryRule[]> {
    return this.http.get<SalaryRule[]>(this.salaryRuleBaseUrl + '/companyId/' + companyId);
  }

  getActiveSalaryRuleListByCompany(companyId: any): Observable<SalaryRule[]> {
    return this.http.get<SalaryRule[]>(this.salaryRuleBaseUrl + '/activeListByCompany/' + companyId);
  }

  getSalaryRuleById(id: any): Observable<SalaryRule> {
    return this.http.get<SalaryRule>(this.salaryRuleBaseUrl + id);
  }

  createSalaryRule(salaryRule: SalaryRule) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.salaryRuleBaseUrl, salaryRule, requestHeader);
  }

  updateSalaryRule(salaryRule: SalaryRule) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.salaryRuleBaseUrl, salaryRule, requestHeader);
  }

  deleteSalaryRule(salaryRule: SalaryRule) {
    let httpParams = new HttpParams().set('salaryRuleId', salaryRule.id);
    var input = { id: salaryRule.id };
    var requestHeader = {
      headers: new HttpHeaders({
        'Content-Type': 'apllication/json', 'No-Auth': 'False'
      }),
      body: input
    };
    return this.http.delete(this.salaryRuleBaseUrl, requestHeader);
  }

}
