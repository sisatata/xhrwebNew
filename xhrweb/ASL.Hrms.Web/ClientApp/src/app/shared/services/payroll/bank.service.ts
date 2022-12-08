import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bank } from '../../models/payroll/bank';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class BankService extends BaseService{
  
  private bankBaseUrl = this.Base_API_URL +'Bank';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.bankBaseUrl = this.appSettingsService.apiBaseUrl + 'Bank';
  }

  getAllBankByCompanyId(companyId):Observable<Bank[]>{
    return this.http.get<Bank[]>(this.bankBaseUrl+'/company/'+companyId);
   }
 
   getBankById(bankId: number): Observable<any[]> {
     return this.http.get<any[]>(this.bankBaseUrl  + bankId);
   }

  createBank(bank: Bank) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.bankBaseUrl, bank, requestHeader);
  }

  editBank(bank: Bank) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.bankBaseUrl, bank, requestHeader);
  }

  deleteBankById(bankId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.bankBaseUrl + '/' + bankId, requestHeader);
  }

  deleteBank(bank: any) {
    let httpParams=new HttpParams().set('id',bank.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:bank
    };
    return this.http.delete(this.bankBaseUrl , requestHeader);
  }
  
}
