import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
@Injectable({
  providedIn: 'root'
})
export class IncomeTaxSlabService extends BaseService {
  private incomeTaxSlabBaseUrl = this.Base_API_URL + 'IncomeTaxSlab';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.incomeTaxSlabBaseUrl = this.appSettingsService.apiBaseUrl + 'IncomeTaxSlab';
  }

  getAllIncomeTaxPayerType(): Observable<IncomeTaxPayerType[]> {
    return this.http.get<IncomeTaxPayerType[]>(this.incomeTaxSlabBaseUrl);
  }
  getAllIncomeTaxSlab(companyId: any): Observable<IncomeTaxSlab[]> {
    return this.http.get<IncomeTaxSlab[]>(this.incomeTaxSlabBaseUrl + '/company/' + companyId);
  }
  createIncomeTaxSlab(incomeTaxSlab: IncomeTaxSlab) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.incomeTaxSlabBaseUrl, incomeTaxSlab, requestHeader);
  }
  editIncomeTaxSlab(incomeTaxSlab: IncomeTaxSlab) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.incomeTaxSlabBaseUrl, incomeTaxSlab, requestHeader);
  }
  deleteIncomeTaxSlab(incomeTaxSlab: IncomeTaxSlab) {
    let httpParams = new HttpParams().set('id', incomeTaxSlab.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: incomeTaxSlab
    };
    return this.http.delete(this.incomeTaxSlabBaseUrl, requestHeader);
  }
}
