import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CompanyAddress } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class CompanyAddressService extends BaseService {

  private companyaddressBaseUrl = this.Base_API_URL + 'CompanyAddress';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.companyaddressBaseUrl = this.appSettingsService.apiBaseUrl + 'CompanyAddress';
  }

  getAllCompanyAddressByCompanyId(companyId): Observable<CompanyAddress[]> {
    return this.http.get<CompanyAddress[]>(this.companyaddressBaseUrl + '/company/' + companyId);
  }

  getCompanyAddressById(companyaddressId: number): Observable<any[]> {
    return this.http.get<any[]>(this.companyaddressBaseUrl + companyaddressId);
  }

  createCompanyAddress(companyAddress: CompanyAddress) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.companyaddressBaseUrl, companyAddress, requestHeader);
  }


  editCompanyAddress(companyaddress: CompanyAddress) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.companyaddressBaseUrl, companyaddress, requestHeader);
  }

  deleteCompanyAddressById(companyaddressId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.companyaddressBaseUrl + '/' + companyaddressId, requestHeader);
  }

  deleteCompanyAddress(companyaddress: any) {
    let httpParams = new HttpParams().set('id', companyaddress.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: companyaddress
    };
    return this.http.delete(this.companyaddressBaseUrl, requestHeader);
  }

}
