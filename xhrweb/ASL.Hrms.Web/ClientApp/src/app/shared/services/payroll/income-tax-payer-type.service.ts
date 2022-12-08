import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, PaymentOption } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class IncomeTaxPayerTypeService extends BaseService {

  private incomeTaxPayerTypeBaseUrl = this.Base_API_URL + 'IncomeTaxPayerType';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.incomeTaxPayerTypeBaseUrl = this.appSettingsService.apiBaseUrl + 'IncomeTaxPayerType';
  }

  getAllPaymentOptionByCompanyId(companyId): Observable<PaymentOption[]> {
    return this.http.get<PaymentOption[]>(this.incomeTaxPayerTypeBaseUrl + '/company/' + companyId);
  }
  getAllIncomeTaxPayerType(companyId: any): Observable<IncomeTaxPayerType[]> {
    return this.http.get<IncomeTaxPayerType[]>(this.incomeTaxPayerTypeBaseUrl + '/company/' + companyId);
  }



  createIncomeTaxPayerType(incomeTaxPayerType: IncomeTaxPayerType) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.incomeTaxPayerTypeBaseUrl, incomeTaxPayerType, requestHeader);
  }

  editIncomeTaxPayerType(incomeTaxPayerType: IncomeTaxPayerType) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.incomeTaxPayerTypeBaseUrl, incomeTaxPayerType, requestHeader);
  }



  deleteIncomeTaxPayerType(incomeTaxPayerType: IncomeTaxPayerType) {
    let httpParams = new HttpParams().set('id', incomeTaxPayerType.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: incomeTaxPayerType
    };
    return this.http.delete(this.incomeTaxPayerTypeBaseUrl, requestHeader);
  }

}
