import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeEmail } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeEmailService extends BaseService {
  private employeeEmailBaseUrl = this.Base_API_URL + 'EmployeeEmail';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeEmailBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeEmail';
  }

  getAllEmployeeEmails(): Observable<EmployeeEmail[]> {
    return this.http.get<EmployeeEmail[]>(this.employeeEmailBaseUrl);
  }


  getEmployeeEmailByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeEmailBaseUrl + "/employeeId/" + employeeId);
  }

  createEmployeeEmail(employeeEmail: EmployeeEmail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeEmailBaseUrl, employeeEmail, requestHeader);
  }

  editEmployeeEmail(employeeEmail: EmployeeEmail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeEmailBaseUrl, employeeEmail, requestHeader);
  }

  deleteEmployeeEmail(employeeEmail: any) {

    let httpParams = new HttpParams().set('employeeEmailId', employeeEmail.id);
    var input = { id: employeeEmail.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeeEmailBaseUrl, requestHeader);
  }
}


