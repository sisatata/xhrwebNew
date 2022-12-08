import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MonthCycle } from '../../models/company/month-cycle';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class MonthCycleService extends BaseService {
  private monthCycleBaseUrl = this.Base_API_URL + 'MonthCycle';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.monthCycleBaseUrl = this.appSettingsService.apiBaseUrl + 'MonthCycle';
  }

  getMonthCycleByCompanyAndFinancialYear(companyId, financialYearId): Observable<MonthCycle[]> {
    return this.http.get<MonthCycle[]>(this.monthCycleBaseUrl + '/company/' + companyId + '/financialYear/' + financialYearId);
  }

  getMonthCycleById(monthCycleId: any): Observable<any[]> {
    return this.http.get<any[]>(this.monthCycleBaseUrl + monthCycleId);
  }
  createMonthCycle(monthCycle: MonthCycle) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.monthCycleBaseUrl, monthCycle, requestHeader);
  }

  editMonthCycle(monthCycle: MonthCycle) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.monthCycleBaseUrl, monthCycle, requestHeader);
  }

  deleteMonthCycleById(monthCycleId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.monthCycleBaseUrl + '/' + monthCycleId, requestHeader);
  }

  deleteMonthCycle(monthCycle: any) {
    let httpParams = new HttpParams().set('id', monthCycle.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: monthCycle
    };
    return this.http.delete(this.monthCycleBaseUrl, requestHeader);
  }

}
