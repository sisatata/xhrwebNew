import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeFamilyMember } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeFamilyMemberService extends BaseService {
  private employeeFamilyMemberBaseUrl = this.Base_API_URL + 'EmployeeFamilyMember';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeFamilyMemberBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeFamilyMember';
  }

  getAllEmployeeFamilyMembers(): Observable<EmployeeFamilyMember[]> {
    return this.http.get<EmployeeFamilyMember[]>(this.employeeFamilyMemberBaseUrl);
  }


  getEmployeeFamilyMemberByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeFamilyMemberBaseUrl + "/employeeId/" + employeeId);
  }

  createEmployeeFamilyMember(employeeFamilyMember: EmployeeFamilyMember) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeFamilyMemberBaseUrl, employeeFamilyMember, requestHeader);
  }

  editEmployeeFamilyMember(employeeFamilyMember: EmployeeFamilyMember) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeFamilyMemberBaseUrl, employeeFamilyMember, requestHeader);
  }

  deleteEmployeeFamilyMember(employeeFamilyMember: any) {

    let httpParams = new HttpParams().set('employeeFamilyMemberId', employeeFamilyMember.id);
    var input = { id: employeeFamilyMember.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeeFamilyMemberBaseUrl, requestHeader);
  }
}


