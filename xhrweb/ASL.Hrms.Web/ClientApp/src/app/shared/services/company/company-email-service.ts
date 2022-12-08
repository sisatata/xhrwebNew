import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { CompanyEmail } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class CompanyEmailService extends BaseService {
  private companyEmailBaseUrl = this.Base_API_URL + 'CompanyEmail';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.companyEmailBaseUrl = this.appSettingsService.apiBaseUrl + 'CompanyEmail';
  }

  getAllCompanyEmails(): Observable<CompanyEmail[]> {
    return this.http.get<CompanyEmail[]>(this.companyEmailBaseUrl);
  }


  getCompanyEmailByCompanyId(companyId: any): Observable<any> {
    return this.http.get<any>(this.companyEmailBaseUrl + "/company/" + companyId);
  }

  createCompanyEmail(companyEmail: CompanyEmail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.companyEmailBaseUrl, companyEmail, requestHeader);
  }

  editCompanyEmail(companyEmail: CompanyEmail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.companyEmailBaseUrl, companyEmail, requestHeader);
  }

  deleteCompanyEmail(companyEmail: any) {
    let httpParams = new HttpParams().set('companyEmailId', companyEmail.id);
    var input = { id: companyEmail.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.companyEmailBaseUrl, requestHeader);
  }
}


