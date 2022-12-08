import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AttendanceProcess } from '../../models/attendance/attendance-process';
import { AppSettingsService } from '../../../app-settings.service';
import { Observable } from 'rxjs';
import { ApproveOrRejectAttendanceRequest, AttendanceRequest } from '../../models';
import { ApproveOrRejectAttendanceRequestComponent } from 'src/app/attendance/attendance-request/approve-or-reject-attendance-request/approve-or-reject-attendance-request.component';

@Injectable({
  providedIn: 'root'
})
export class AttendanceRequestService extends BaseService{
  
  private attendanceRequestBaseUrl = `${this.Base_API_URL}AttendanceRequest`;
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super()
    this.attendanceRequestBaseUrl = `${this.appSettingsService.apiBaseUrl}AttendanceRequest`;
  }

  
  getAttendanceRequestsByEmployeeId(employeeId:any,requestTypeId:number, startDate:any, endDate:any):Observable<AttendanceRequest[]>{
    return this.http.get<AttendanceRequest[]>(`${this.attendanceRequestBaseUrl}/GetBySearch/${employeeId}/${requestTypeId}/${startDate}/${endDate}`);
  }
  getAttendanceRequestsByManagerId(managerId:any, startDate:any, endDate:any):Observable<AttendanceRequest[]>{
    return this.http.get<AttendanceRequest[]>(`${this.attendanceRequestBaseUrl}/pending-attendance-request/${managerId}/${startDate}/${endDate}`);
  }
  createAttendanceRequest(attendanceRequeest: AttendanceRequest){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.attendanceRequestBaseUrl, attendanceRequeest, requestHeader);

  }
  editAttendanceRequest(attendanceRequeest: AttendanceRequest){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.attendanceRequestBaseUrl, attendanceRequeest, requestHeader);

  }
  approveAttendanceRequest(approveOrRejetcAttendanceRequest: ApproveOrRejectAttendanceRequest){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.attendanceRequestBaseUrl}/approve`, approveOrRejetcAttendanceRequest, requestHeader);
  }
  rejectAttendanceRequest(approveOrRejetcAttendanceRequest: ApproveOrRejectAttendanceRequest){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.attendanceRequestBaseUrl}/decline-attendance`, approveOrRejetcAttendanceRequest, requestHeader);
  }
}
