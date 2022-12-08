import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bank } from '../../models/payroll/bank';
import { BenefitDeductionCode } from '../../models/payroll/benefit-deduction';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';



@Injectable({
  providedIn: 'root'
})
export class BenefitDeductionService extends BaseService{
  
  private benefitDeductionBaseUrl = this.Base_API_URL +'BenefitDeductionCode';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.benefitDeductionBaseUrl = this.appSettingsService.apiBaseUrl + 'BenefitDeductionCode';
  }

  getAllBankByCompanyId(companyId):Observable<Bank[]>{
    return this.http.get<Bank[]>(this.benefitDeductionBaseUrl+'/company/'+companyId);
   }
 
   getBankById(bankId: number): Observable<any[]> {
     return this.http.get<any[]>(this.benefitDeductionBaseUrl  + bankId);
   }

  createBank(bank: Bank) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.benefitDeductionBaseUrl, bank, requestHeader);
  }

  editBank(bank: Bank) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.benefitDeductionBaseUrl, bank, requestHeader);
  }

  deleteBankById(bankId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.benefitDeductionBaseUrl + '/' + bankId, requestHeader);
  }

  deleteBank(bank: any) {
    let httpParams=new HttpParams().set('id',bank.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:bank
    };
    return this.http.delete(this.benefitDeductionBaseUrl , requestHeader);
  }
  getAllBenifitDeductionByCompanyId(companyId:any):Observable<BenefitDeductionCode[]>{
    return this.http.get<BenefitDeductionCode[]>(this.benefitDeductionBaseUrl  + '/companyId/' +companyId );
  }
  createBenefitDeductionCode(benefitDeductionCode : BenefitDeductionCode){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.benefitDeductionBaseUrl, benefitDeductionCode, requestHeader);
  }
  deleteBenefitDeductionCode(benefitDeduction : BenefitDeductionCode){
    let httpParams=new HttpParams().set('id',benefitDeduction.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:benefitDeduction
    };
    return this.http.delete(this.benefitDeductionBaseUrl , requestHeader);
  }

  editBenefitDeductionCode(benefitDeductionCode : BenefitDeductionCode){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.put(this.benefitDeductionBaseUrl, benefitDeductionCode, requestHeader);
  }
  
}
