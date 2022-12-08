import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CompanyPhone } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class CompanyPhoneService extends BaseService {

  private companyphoneBaseUrl = this.Base_API_URL + 'CompanyPhone';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.companyphoneBaseUrl = this.appSettingsService.apiBaseUrl + 'CompanyPhone';
  }

  getAllCompanyPhoneByCompanyId(companyId): Observable<CompanyPhone[]> {
    return this.http.get<CompanyPhone[]>(this.companyphoneBaseUrl + '/company/' + companyId);
  }

  getCompanyPhoneById(companyphoneId: number): Observable<any[]> {
    return this.http.get<any[]>(this.companyphoneBaseUrl + companyphoneId);
  }

  createCompanyPhone(companyphone: CompanyPhone) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.companyphoneBaseUrl, companyphone, requestHeader);
  }

  editCompanyPhone(companyphone: CompanyPhone) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.companyphoneBaseUrl, companyphone, requestHeader);
  }

  deleteCompanyPhoneById(companyphoneId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.companyphoneBaseUrl + '/' + companyphoneId, requestHeader);
  }

  deleteCompanyPhone(companyphone: any) {
    let httpParams = new HttpParams().set('id', companyphone.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: companyphone
    };
    return this.http.delete(this.companyphoneBaseUrl, requestHeader);
  }

}
