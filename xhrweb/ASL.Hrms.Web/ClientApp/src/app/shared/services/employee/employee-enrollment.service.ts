import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeEnrollment } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeEnrollmentService extends BaseService {
  private employeeEnrollmentBaseUrl = this.Base_API_URL + 'EmployeeEnrollment';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeEnrollmentBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeEnrollment';
  }

  getAllEmployeeEnrollments(): Observable<EmployeeEnrollment[]> {
    return this.http.get<EmployeeEnrollment[]>(this.employeeEnrollmentBaseUrl);
  }


  getEmployeeEnrollmentByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeEnrollmentBaseUrl + "/employeeId/" + employeeId);
  }

  createEmployeeEnrollment(employeeEnrollment: EmployeeEnrollment) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeEnrollmentBaseUrl, employeeEnrollment, requestHeader);
  }

  editEmployeeEnrollment(employeeEnrollment: EmployeeEnrollment) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeEnrollmentBaseUrl, employeeEnrollment, requestHeader);
  }

  uploadEmployeeEnrollmentImage(formData: any) {
    var requestHeader = { headers: new HttpHeaders({ 'No-Auth': 'False' }) };
    return this.http.post(this.employeeEnrollmentBaseUrl+"/upload-image", formData, requestHeader);
  }
  uploadEmployeeSiganture(formData: any) {
    var requestHeader = { headers: new HttpHeaders({ 'No-Auth': 'False' }) };
    return this.http.post(this.employeeEnrollmentBaseUrl+"/upload-signature", formData, requestHeader);
  }

  //deleteEmployeeEnrollmentById(id: any) {
  //  var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
  //  return this.http.delete(this.employeeEnrollmentBaseUrl + '/' + id, requestHeader);
  //}

  deleteEmployeeEnrollment(employeeEnrollment: any) {

    let httpParams = new HttpParams().set('id', employeeEnrollment.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: employeeEnrollment
    };

    return this.http.delete(this.employeeEnrollmentBaseUrl, requestHeader);
  }


}


