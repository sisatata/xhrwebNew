import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeSalary } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeSalaryService extends BaseService {

  private employeesalaryBaseUrl = this.Base_API_URL + 'EmployeeSalary';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeesalaryBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeSalary';
  }

  getAllEmployeeSalaryByCompanyId(companyId): Observable<EmployeeSalary[]> {
    return this.http.get<EmployeeSalary[]>(this.employeesalaryBaseUrl + '/company/' + companyId);
  }
getEmployeeSalaryByEmployeeId(employeeId:any):Observable<EmployeeSalary[]>{

  return this.http.get<EmployeeSalary[]>(this.employeesalaryBaseUrl+'/employee/'+ employeeId);
}
  getEmployeeSalaryById(employeesalaryId: number): Observable<any[]> {
    return this.http.get<any[]>(this.employeesalaryBaseUrl + employeesalaryId);
  }

  createEmployeeSalary(employeesalary: EmployeeSalary) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeesalaryBaseUrl, employeesalary, requestHeader);
  }
getCurrentSalary(employeeSalary: EmployeeSalary){
  var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
  return this.http.post(`${this.employeesalaryBaseUrl}/currentSalary`, employeeSalary, requestHeader);
}
  editEmployeeSalary(employeesalary: EmployeeSalary) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeesalaryBaseUrl, employeesalary, requestHeader);
  }

  deleteEmployeeSalaryById(employeesalaryId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.employeesalaryBaseUrl + '/' + employeesalaryId, requestHeader);
  }

  deleteEmployeeSalary(employeesalary: any) {
    let httpParams = new HttpParams().set('id', employeesalary.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: employeesalary
    };
    return this.http.delete(this.employeesalaryBaseUrl, requestHeader);
  }

}
