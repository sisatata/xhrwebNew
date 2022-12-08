import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeePhone } from '../../models';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeePhoneService extends BaseService {
  private employeePhoneBaseUrl = this.Base_API_URL + 'EmployeePhone';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeePhoneBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeePhone';
  }

  getAllEmployeePhones(): Observable<EmployeePhone[]> {
    return this.http.get<EmployeePhone[]>(this.employeePhoneBaseUrl);
  }


  getEmployeePhoneByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeePhoneBaseUrl + "/employeeId/" + employeeId);
  }

  createEmployeePhone(employeePhone: EmployeePhone) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeePhoneBaseUrl, employeePhone, requestHeader);
  }

  editEmployeePhone(employeePhone: EmployeePhone) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeePhoneBaseUrl, employeePhone, requestHeader);
  }

  deleteEmployeePhone(employeePhone: any) {

    let httpParams = new HttpParams().set('employeePhoneId', employeePhone.id);
    var input = { id: employeePhone.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeePhoneBaseUrl, requestHeader);
  }
}


