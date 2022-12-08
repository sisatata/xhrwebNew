import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeSalaryComp } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeSalaryCompService extends BaseService {

  private employeeSalaryCompBaseUrl = this.Base_API_URL + 'EmployeeSalaryComponent';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeSalaryCompBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeSalaryComponent';
  }

  getAllEmployeeSalaryCompByCompanyId(companyId): Observable<EmployeeSalaryComp[]> {
    return this.http.get<EmployeeSalaryComp[]>(this.employeeSalaryCompBaseUrl + '/company/' + companyId);
  }

  getEmployeeSalaryCompById(employeeSalaryCompId: number): Observable<any[]> {
    return this.http.get<any[]>(this.employeeSalaryCompBaseUrl + employeeSalaryCompId);
  }

  createEmployeeSalaryComp(employeeSalaryComp: EmployeeSalaryComp) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeSalaryCompBaseUrl, employeeSalaryComp, requestHeader);
  }

  editEmployeeSalaryComp(employeeSalaryComp: EmployeeSalaryComp) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeSalaryCompBaseUrl, employeeSalaryComp, requestHeader);
  }

  deleteEmployeeSalaryCompById(employeeSalaryCompId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.employeeSalaryCompBaseUrl + '/' + employeeSalaryCompId, requestHeader);
  }

  deleteEmployeeSalaryComp(employeeSalaryComp: any) {
    let httpParams = new HttpParams().set('id', employeeSalaryComp.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: employeeSalaryComp
    };
    return this.http.delete(this.employeeSalaryCompBaseUrl, requestHeader);
  }


}
