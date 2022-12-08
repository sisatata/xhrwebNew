import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeDetail } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeDetailService extends BaseService {
  private employeeDetailBaseUrl = this.Base_API_URL + 'EmployeeDetail';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeDetailBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeDetail';
  }

  getAllEmployeeDetails(): Observable<EmployeeDetail[]> {
    return this.http.get<EmployeeDetail[]>(this.employeeDetailBaseUrl);
  }


  getEmployeeDetailByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeDetailBaseUrl+"/employeeId/" + employeeId);
  }

  createEmployeeDetail(employeeDetail: EmployeeDetail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeDetailBaseUrl, employeeDetail, requestHeader);
  }

  editEmployeeDetail(employeeDetail: EmployeeDetail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeDetailBaseUrl, employeeDetail, requestHeader);
  }

  //deleteEmployeeDetailById(id: any) {
  //  var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
  //  return this.http.delete(this.employeeDetailBaseUrl + '/' + id, requestHeader);
  //}

  deleteEmployeeDetail(employeeDetail: any) {

    let httpParams = new HttpParams().set('id', employeeDetail.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: employeeDetail
    };

    return this.http.delete(this.employeeDetailBaseUrl, requestHeader);
  }


}


