import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeEducation } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeEducationService extends BaseService {
  private employeeEducationBaseUrl = this.Base_API_URL + 'EmployeeEducation';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeEducationBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeEducation';
  }

  getAllEmployeeEducations(): Observable<EmployeeEducation[]> {
    return this.http.get<EmployeeEducation[]>(this.employeeEducationBaseUrl);
  }


  getEmployeeEducationByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeEducationBaseUrl + "/employeeId/" + employeeId);
  }

  createEmployeeEducation(employeeEducation: EmployeeEducation) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeEducationBaseUrl, employeeEducation, requestHeader);
  }

  editEmployeeEducation(employeeEducation: EmployeeEducation) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeEducationBaseUrl, employeeEducation, requestHeader);
  }

  deleteEmployeeEducation(employeeEducation: any) {

    let httpParams = new HttpParams().set('employeeEducationId', employeeEducation.id);
    var input = { id: employeeEducation.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeeEducationBaseUrl, requestHeader);
  }
}




