import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxParameter, IncomeTaxPayerType, IncomeTaxSlab, PaymentOption } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
@Injectable({
  providedIn: 'root'
})
export class IncomeTaxParameterService extends BaseService {
  private incomeTaxParameterBaseUrl = this.Base_API_URL + 'IncomeTaxParameter';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.incomeTaxParameterBaseUrl = this.appSettingsService.apiBaseUrl + 'IncomeTaxParameter';
  }

  getAllIncomeTaxParameter(companyId: any): Observable<IncomeTaxParameter[]> {
    return this.http.get<IncomeTaxParameter[]>(this.incomeTaxParameterBaseUrl + '/company/' + companyId);
  }
  createIncomeTaxParameter(incomeTaxParameter: IncomeTaxParameter) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.incomeTaxParameterBaseUrl, incomeTaxParameter, requestHeader);
  }
  editIncomeTaxParameter(incomeTaxParameter: IncomeTaxParameter) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.incomeTaxParameterBaseUrl, incomeTaxParameter, requestHeader);
  }
  deleteIncomeTaxParameter(incomeTaxParameter: IncomeTaxParameter) {
    let httpParams = new HttpParams().set('id', incomeTaxParameter.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: incomeTaxParameter
    };
    return this.http.delete(this.incomeTaxParameterBaseUrl, requestHeader);
  }
}
