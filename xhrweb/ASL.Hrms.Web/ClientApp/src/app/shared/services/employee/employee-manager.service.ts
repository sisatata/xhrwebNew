import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeManager } from '../../models/employee/employee-manager';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeManagerService extends BaseService {
  private employeeManagerBaseUrl = this.Base_API_URL + 'EmployeeManager';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeManagerBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeManager';
  }
  getAllEmployeeManagers(): Observable<EmployeeManager[]> {
    return this.http.get<EmployeeManager[]>(this.employeeManagerBaseUrl);
  }

  getEmployeeManagerListByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeManagerBaseUrl + "/employeeId/" + employeeId);
  }

  getEmployeeManagerByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeManagerBaseUrl + "/employeeId/" + employeeId);
  }
  createEmployeeManager(employeeManager: EmployeeManager) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeManagerBaseUrl, employeeManager, requestHeader);
  }
  editEmployeeManager(employeeManager: EmployeeManager) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeManagerBaseUrl, employeeManager, requestHeader);
  }

  deleteEmployeeManager(employeeManager: any) {

    var input = { id: employeeManager.id };
    console.log(input);
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeeManagerBaseUrl, requestHeader);
  }
}
