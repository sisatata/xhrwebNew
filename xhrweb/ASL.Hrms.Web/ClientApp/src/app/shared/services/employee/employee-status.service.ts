import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeStatus } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeStatusService extends BaseService {
  private employeeStatusBaseUrl = this.Base_API_URL + 'EmployeeStatus';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeStatusBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeStatus';
  }

  getAllEmployeeStatusByCompanyId(companyId): Observable<EmployeeStatus[]> {
    return this.http.get<EmployeeStatus[]>(this.employeeStatusBaseUrl + '/companyId/' + companyId);
  }

  getBranchById(employeeStatusId: any): Observable<any[]> {
    return this.http.get<any[]>(this.employeeStatusBaseUrl + employeeStatusId);
  }

  createEmployeeStatus(employeeStatus: EmployeeStatus) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeStatusBaseUrl, employeeStatus, requestHeader);
  }

  editEmployeeStatus(employeeStatus: EmployeeStatus) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeStatusBaseUrl, employeeStatus, requestHeader);
  }

  deleteEmployeeStatusById(employeeStatusId: any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.employeeStatusBaseUrl + '/' + employeeStatusId, requestHeader);
  }

  deleteEmployeeStatus(employeeStatus: any) {

    let httpParams = new HttpParams().set('id', employeeStatus.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: employeeStatus
    };

    return this.http.delete(this.employeeStatusBaseUrl, requestHeader);
  }


}

