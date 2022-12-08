import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BankBranch } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class BankBranchService extends BaseService {

  private bankbranchBaseUrl = this.Base_API_URL + 'BankBranch';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.bankbranchBaseUrl = this.appSettingsService.apiBaseUrl + 'BankBranch';
  }

  getAllBankBranchByBankId(bankId): Observable<BankBranch[]> {
    return this.http.get<BankBranch[]>(this.bankbranchBaseUrl + '/getbybank/' + bankId);
  }

  getBankBranchById(bankbranchId: number): Observable<any[]> {
    return this.http.get<any[]>(this.bankbranchBaseUrl + bankbranchId);
  }

  createBankBranch(bankbranch: BankBranch) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.bankbranchBaseUrl, bankbranch, requestHeader);
  }

  editBankBranch(bankbranch: BankBranch) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.bankbranchBaseUrl, bankbranch, requestHeader);
  }

  deleteBankBranchById(bankbranchId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.bankbranchBaseUrl + '/' + bankbranchId, requestHeader);
  }

  deleteBankBranch(bankbranch: any) {
    let httpParams = new HttpParams().set('id', bankbranch.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: bankbranch
    };
    return this.http.delete(this.bankbranchBaseUrl, requestHeader);
  }

}
