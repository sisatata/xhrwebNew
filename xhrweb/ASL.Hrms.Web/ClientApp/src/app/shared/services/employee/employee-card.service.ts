import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeCard, EmployeeEducation } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeCardService extends BaseService {
  private employeeCardBaseUrl = this.Base_API_URL + 'EmployeeCard';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeCardBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeCard';
  }

  getAllEmployeeEducations(): Observable<EmployeeEducation[]> {
    return this.http.get<EmployeeEducation[]>(this.employeeCardBaseUrl);
  }


  getEmployeeCardByEmployee(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeCardBaseUrl + "/Employee/" + employeeId);
  }

  createEmployeeCard(employeeCard: EmployeeCard) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeCardBaseUrl, employeeCard, requestHeader);
  }

  editEmployeeCard(employeeCard: EmployeeCard) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeCardBaseUrl, employeeCard, requestHeader);
  }

  deleteEmployeeCard(employeeCard: any) {

    let httpParams = new HttpParams().set('employeeEducationId', employeeCard.id);
    var input = { id: employeeCard.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeeCardBaseUrl, requestHeader);
  }
}




