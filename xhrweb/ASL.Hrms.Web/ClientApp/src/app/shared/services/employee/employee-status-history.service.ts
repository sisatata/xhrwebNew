import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../../models/company/department';
import { EmployeeStatusHistory } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeStatusHistoryService extends BaseService {
  private employeeStatusHistoryBaseUrl = this.Base_API_URL + 'EmployeeStatusHistory';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeStatusHistoryBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeStatusHistory';
  }

  getAllEmployeeStatusHistorys(): Observable<EmployeeStatusHistory[]> {
    return this.http.get<EmployeeStatusHistory[]>(this.employeeStatusHistoryBaseUrl);
  }

  getAllEmployeeStatusHistoryByBranchId(branchId): Observable<EmployeeStatusHistory[]> {
    return this.http.get<EmployeeStatusHistory[]>(this.employeeStatusHistoryBaseUrl + '/branch/' + branchId);
  }

  getBranchById(employeeStatusHistoryId: any): Observable<any[]> {
    return this.http.get<any[]>(this.employeeStatusHistoryBaseUrl + employeeStatusHistoryId);
  }

  createEmployeeStatusHistory(employeeStatusHistory: EmployeeStatusHistory) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeStatusHistoryBaseUrl, employeeStatusHistory, requestHeader);
  }

  editEmployeeStatusHistory(employeeStatusHistory: EmployeeStatusHistory) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeStatusHistoryBaseUrl, employeeStatusHistory, requestHeader);
  }

  deleteEmployeeStatusHistoryById(employeeStatusHistoryId: any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.employeeStatusHistoryBaseUrl + '/' + employeeStatusHistoryId, requestHeader);
  }

  deleteEmployeeStatusHistory(employeeStatusHistory: any) {

    let httpParams = new HttpParams().set('id', employeeStatusHistory.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: employeeStatusHistory
    };

    return this.http.delete(this.employeeStatusHistoryBaseUrl, requestHeader);
  }


}

