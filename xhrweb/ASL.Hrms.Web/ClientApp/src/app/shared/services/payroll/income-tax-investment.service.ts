import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxInvestment, IncomeTaxParameter, IncomeTaxPayerType, IncomeTaxSlab, PaymentOption } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
@Injectable({
  providedIn: 'root'
})
export class IncomeTaxInvestmentService extends BaseService {
  private incomeTaxInvestmentBaseUrl = this.Base_API_URL + 'IncomeTaxInvestment';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.incomeTaxInvestmentBaseUrl = this.appSettingsService.apiBaseUrl + 'IncomeTaxInvestment';
  }
  getAllIncomeTaxInvestment(companyId: any): Observable<IncomeTaxInvestment[]> {
    return this.http.get<IncomeTaxInvestment[]>(this.incomeTaxInvestmentBaseUrl + '/company/' + companyId);
  }
  createIncomeTaxInvestment(incomeTaxInvestment: IncomeTaxInvestment) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.incomeTaxInvestmentBaseUrl, incomeTaxInvestment, requestHeader);
  }
  editIncomeTaxInvestment(incomeTaxInvestment: IncomeTaxInvestment) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.incomeTaxInvestmentBaseUrl, incomeTaxInvestment, requestHeader);
  }
  deleteIncomeTaxInvestment(incomeTaxInvestment: IncomeTaxInvestment) {
    let httpParams = new HttpParams().set('id', incomeTaxInvestment.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: incomeTaxInvestment
    };
    return this.http.delete(this.incomeTaxInvestmentBaseUrl, requestHeader);
  }
}
