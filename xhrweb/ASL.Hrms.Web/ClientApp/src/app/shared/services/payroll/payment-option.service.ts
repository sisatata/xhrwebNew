import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaymentOption } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class PaymentOptionService extends BaseService{
  
  private paymentoptionBaseUrl = this.Base_API_URL +'PaymentOption';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.paymentoptionBaseUrl = this.appSettingsService.apiBaseUrl + 'PaymentOption';
  }

  getAllPaymentOptionByCompanyId(companyId):Observable<PaymentOption[]>{
    return this.http.get<PaymentOption[]>(this.paymentoptionBaseUrl+'/company/'+companyId);
   }
 
   getPaymentOptionById(paymentoptionId: number): Observable<any[]> {
     return this.http.get<any[]>(this.paymentoptionBaseUrl  + paymentoptionId);
   }

  createPaymentOption(paymentoption: PaymentOption) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.paymentoptionBaseUrl, paymentoption, requestHeader);
  }

  editPaymentOption(paymentoption: PaymentOption) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.paymentoptionBaseUrl, paymentoption, requestHeader);
  }

  deletePaymentOptionById(paymentoptionId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.paymentoptionBaseUrl + '/' + paymentoptionId, requestHeader);
  }

  deletePaymentOption(paymentoption: any) {
    let httpParams=new HttpParams().set('id',paymentoption.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:paymentoption
    };
    return this.http.delete(this.paymentoptionBaseUrl , requestHeader);
  }
  
}
