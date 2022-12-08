import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeaveType } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class LeaveTypeService extends BaseService {
  private leaveTypeBaseUrl = this.Base_API_URL + 'LeaveType';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.leaveTypeBaseUrl = this.appSettingsService.apiBaseUrl + 'LeaveType';
  }

  getAllLeaveTypeByCompanyId(companyId): Observable<LeaveType[]> {
    return this.http.get<LeaveType[]>(this.leaveTypeBaseUrl + '/company/' + companyId);
  }
  getAllLeaveTypeByCompanyIdForCombo(companyId): Observable<LeaveType[]> {
    return this.http.get<LeaveType[]>(this.leaveTypeBaseUrl + '/company-combo-box/' + companyId);
  }

  getAllLeaveTypeByEmployeeId(employeeId:any): Observable<LeaveType[]> {
    return this.http.get<LeaveType[]>(this.leaveTypeBaseUrl + '/employee/' + employeeId);
  }
  getLeaveTypeById(leaveTypeId: any): Observable<any[]> {
    return this.http.get<any[]>(this.leaveTypeBaseUrl + leaveTypeId);
  }

  createLeaveType(leaveType: LeaveType) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.leaveTypeBaseUrl, leaveType, requestHeader);
  }

  editLeaveType(leaveType: LeaveType) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.leaveTypeBaseUrl, leaveType, requestHeader);
  }

  deleteLeaveTypeById(leaveTypeId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.leaveTypeBaseUrl + '/' + leaveTypeId, requestHeader);
  }

  deleteLeaveType(leaveType: any) {
    let httpParams = new HttpParams().set('id', leaveType.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: leaveType
    };
    return this.http.delete(this.leaveTypeBaseUrl, requestHeader);
  }



}
