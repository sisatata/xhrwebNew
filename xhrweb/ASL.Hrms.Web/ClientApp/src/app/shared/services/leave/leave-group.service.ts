import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { LeaveGroup, ShiftGroup } from '../../models';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class LeaveGroupService extends BaseService {
  private leaveGroupBaseUrl = `${this.Base_API_URL}LeaveTypeGroup`;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.leaveGroupBaseUrl = `${this.appSettingsService.apiBaseUrl}LeaveTypeGroup`;
  }

  getAllLeaveGroups(companyId:any): Observable<LeaveGroup[]> {
    return this.http.get<LeaveGroup[]>(`${this.leaveGroupBaseUrl}/company/${companyId}`);
  }


  getShiftGroupByCompanyId(companyId: any): Observable<any> {
    return this.http.get<any>(this.leaveGroupBaseUrl + "/company/" + companyId);
  }

  createLeaveGroup(leaveGroup: LeaveGroup) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.leaveGroupBaseUrl, leaveGroup, requestHeader);
  }

  editLeaveGroup(leaveGroup: LeaveGroup) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.leaveGroupBaseUrl, leaveGroup, requestHeader);
  }

  deleteLeaveGroup(leaveGroup: LeaveGroup) {

    let httpParams = new HttpParams().set('shiftGroupId', leaveGroup.id);
    var input = { id: leaveGroup.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.leaveGroupBaseUrl, requestHeader);
  }
}



