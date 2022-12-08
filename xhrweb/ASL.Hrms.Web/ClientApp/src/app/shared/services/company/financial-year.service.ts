import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FinancialYear } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class FinancialYearService extends BaseService {
  private finantialYearBaseUrl = this.Base_API_URL + 'FinancialYear';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.finantialYearBaseUrl = this.appSettingsService.apiBaseUrl + 'FinancialYear';
  }


  getAllFinancialYearByCompanyId(companyId): Observable<FinancialYear[]> {
    return this.http.get<FinancialYear[]>(this.finantialYearBaseUrl + '/company/' + companyId);
  }

  getCurrentFinancialYearByCompanyId(companyId): Observable<FinancialYear> {
    return this.http.get<FinancialYear>(this.finantialYearBaseUrl + '/get-current-year/' + companyId);
  }

  getFinancialYearById(financialYearId: any): Observable<any[]> {
    return this.http.get<any[]>(this.finantialYearBaseUrl + financialYearId);
  }


  createFinancialYear(financialYear: FinancialYear) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.finantialYearBaseUrl, financialYear, requestHeader);
  }

  editFinancialYear(financialYear: FinancialYear) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.finantialYearBaseUrl, financialYear, requestHeader);
  }

  deleteFinancialYearById(financialYearId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.finantialYearBaseUrl + '/' + financialYearId, requestHeader);
  }

  deleteFinancialYear(financialYear: any) {
    let httpParams = new HttpParams().set('id', financialYear.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: financialYear
    };
    return this.http.delete(this.finantialYearBaseUrl, requestHeader);
  }
  getCurrentFinancialYearIdByCompanyAndYearName(companyId:any, yearName:any){
    return this.http.get<any[]>(`${this.finantialYearBaseUrl}/get-current-financialyearid/company/${companyId}/year/${yearName}`);
  }

}
