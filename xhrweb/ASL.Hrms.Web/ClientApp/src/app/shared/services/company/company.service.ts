import { Injectable, Injector } from '@angular/core';
import { HttpHeaders, HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Company } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class CompanyService extends BaseService {

  private companyBaseUrl = this.Base_API_URL + 'Company';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.companyBaseUrl = this.appSettingsService.apiBaseUrl + 'Company';
  }

  getAllCompanies(): Observable<Company[]> {
    return this.http.get<Company[]>(this.companyBaseUrl);
  }

  getAllCompaniesByApprovalStatus(approvalStatus: string): Observable<Company[]> {
    return this.http.get<Company[]>(this.companyBaseUrl + "/approval-status/" + approvalStatus);
  }


  getCompanyById(companyId: any): Observable<any[]> {
    return this.http.get<any[]>(`${this.companyBaseUrl}/company-id/${companyId}`);
  }


  createCompany(company: Company) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.companyBaseUrl, company, requestHeader);
  }

  editCompany(company: Company) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.companyBaseUrl, company, requestHeader);
  }

  approveCompanyRequest(company: Company) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.companyBaseUrl + "/approve-company", company, requestHeader);
  }

  declineCompanyRequest(company: Company) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.companyBaseUrl + "/decline-company", company, requestHeader);
  }


  deleteCompanyById(companyId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.companyBaseUrl + '/' + companyId, requestHeader);
  }
deactivateCompany(company:Company){
   var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.companyBaseUrl + '/deactivate-company' , company, requestHeader);
}
  deleteCompany(company: any) {
    let httpParams = new HttpParams().set('id', company.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: company
    };

    return this.http.delete(this.companyBaseUrl, requestHeader);
  }
  uploadCompanymage(formData: any) {
    var requestHeader = { headers: new HttpHeaders({ 'No-Auth': 'False' }) };
    console.log(formData);
    return this.http.post(this.companyBaseUrl+"/upload-image", formData, requestHeader);
  }
}
