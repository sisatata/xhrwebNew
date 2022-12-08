import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeBankAccount } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeBankAccountService extends BaseService {

  private employeebankaccountBaseUrl = this.Base_API_URL + 'EmployeeBankAccount';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeebankaccountBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeBankAccount';
  }

  getAllEmployeeBankAccountByCompanyId(companyId): Observable<EmployeeBankAccount[]> {
    return this.http.get<EmployeeBankAccount[]>(this.employeebankaccountBaseUrl + '/company/' + companyId);
  }
getEmployeeBankAccountByEmployeeId(companyId:any, employeeId:any): Observable<EmployeeBankAccount[]> {
  return this.http.get<EmployeeBankAccount[]>(this.employeebankaccountBaseUrl + '/company/' + companyId +'/employee/' + employeeId);
}
  getEmployeeBankAccountById(employeebankaccountId: number): Observable<any[]> {
    return this.http.get<any[]>(this.employeebankaccountBaseUrl + employeebankaccountId);
  }

  createEmployeeBankAccount(employeebankaccount: EmployeeBankAccount) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeebankaccountBaseUrl, employeebankaccount, requestHeader);
  }

  editEmployeeBankAccount(employeebankaccount: EmployeeBankAccount) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeebankaccountBaseUrl, employeebankaccount, requestHeader);
  }

  deleteEmployeeBankAccountById(employeebankaccountId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.employeebankaccountBaseUrl + '/' + employeebankaccountId, requestHeader);
  }

  deleteEmployeeBankAccount(employeebankaccount: any) {
    let httpParams = new HttpParams().set('id', employeebankaccount.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: employeebankaccount
    };
    return this.http.delete(this.employeebankaccountBaseUrl, requestHeader);
  }

}
