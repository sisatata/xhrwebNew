import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ConfirmationRule } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class ConfirmationRuleService extends BaseService {
  private confirmationRuleBaseUrl = this.Base_API_URL + 'ConfirmationRule';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.confirmationRuleBaseUrl = this.appSettingsService.apiBaseUrl + 'ConfirmationRule';
  }

  getConfirmationRuleListByCompany(companyId: any): Observable<ConfirmationRule[]> {
    return this.http.get<ConfirmationRule[]>(this.confirmationRuleBaseUrl + '/companyId/' + companyId);
  }
  getActiveConfirmationRuleListByCompany(companyId: any): Observable<ConfirmationRule[]> {
    return this.http.get<ConfirmationRule[]>(this.confirmationRuleBaseUrl + '/activeListByCompany/' + companyId);
  }
  getConfirmationRuleById(id: any): Observable<ConfirmationRule> {
    return this.http.get<ConfirmationRule>(this.confirmationRuleBaseUrl + id);
  }

  createConfirmationRule(confirmationRule: ConfirmationRule) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.confirmationRuleBaseUrl, confirmationRule, requestHeader);
  }

  updateConfirmationRule(confirmationRule: ConfirmationRule) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.confirmationRuleBaseUrl, confirmationRule, requestHeader);
  }

  deleteConfirmationRule(confirmationRule: ConfirmationRule) {
    let httpParams = new HttpParams().set('confirmationRuleId', confirmationRule.id);
    var input = { id: confirmationRule.id };
    var requestHeader = {
      headers: new HttpHeaders({
        'Content-Type': 'apllication/json', 'No-Auth': 'False'
      }),
      body: input
    };
    return this.http.delete(this.confirmationRuleBaseUrl, requestHeader);
  }

}
