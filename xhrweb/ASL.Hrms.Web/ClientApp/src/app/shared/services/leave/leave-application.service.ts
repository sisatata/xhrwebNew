import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LeaveApplication } from '../../models/leave/leave-application';
import { AppSettingsService } from '../../../app-settings.service';
import { ApproveOrRejectLeaveRequest, LeaveRequest } from '../../models';
import { ApproveOrRejectEmployeePromotionTransferComponent } from 'src/app/employee/employee-promotion-transfer-list/approve-or-reject-employee-promotion-transfer/approve-or-reject-employee-promotion-transfer.component';

@Injectable({
  providedIn: 'root'
})
export class LeaveApplicationService extends BaseService {
  private leaveApplicationBaseUrl = this.Base_API_URL + 'LeaveApplication';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.leaveApplicationBaseUrl = this.appSettingsService.apiBaseUrl + 'LeaveApplication';
  }


 
  getAllLeaveApplicationByCompanyId(companyId, startDate, endDate): Observable<LeaveApplication[]> {
    return this.http.get<LeaveApplication[]>(this.leaveApplicationBaseUrl + '/company/' + companyId + '/' + startDate + '/' + endDate);
  }
  getAllLeaveApplicationByParameter(companyId, startDate, endDate, employeeName, leaveName, status): Observable<LeaveApplication[]> {
    let params = new HttpParams();
    params = params.append('companyId', companyId);
    params = params.append('startDate', startDate);
    params = params.append('endDate', endDate);
    params = params.append('employeeName', employeeName);
    params = params.append('leaveTypeName', leaveName);
    params = params.append('status', status);
    return this.http.get<LeaveApplication[]>(this.leaveApplicationBaseUrl, { params: params });
  }
  getAllLeaveApplicationByEmployeeId(employeeId, startDate, endDate): Observable<any> {
    return this.http.get<any>(this.leaveApplicationBaseUrl + '/employee/' + employeeId + '/' + startDate + '/' + endDate);
  }
  getLeaveRequestsByManagerId(managerId:any, startDate:any, endDate:any):Observable<LeaveRequest[]>{
    return this.http.get<LeaveRequest[]>(`${this.leaveApplicationBaseUrl}/pending-leave-application/${managerId}/${startDate}/${endDate}`);
  }
  getAllLeaveApplication(leaveApplication: LeaveApplication): Observable<LeaveApplication[]> {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post<LeaveApplication[]>(`${this.leaveApplicationBaseUrl}/leave-application`, leaveApplication, requestHeader);
  }
  getLeaveApplicationById(leaveApplicationId: any): Observable<any[]> {
    return this.http.get<any[]>(this.leaveApplicationBaseUrl + leaveApplicationId);
  }
  applyLeave(leaveApplication: LeaveApplication) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.leaveApplicationBaseUrl + '/apply-leave', leaveApplication, requestHeader);
  }

  updateLeave(leaveApplication: LeaveApplication) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.leaveApplicationBaseUrl + '/update-leave', leaveApplication, requestHeader);
  }
  approveLeave(leaveApplication: LeaveApplication) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.leaveApplicationBaseUrl, leaveApplication, requestHeader);
  }
  declineleaveApplicationById(leaveApplicationId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.leaveApplicationBaseUrl + '/' + leaveApplicationId, requestHeader);
  }
  approveLeaveRequest(leaveRequest: ApproveOrRejectLeaveRequest){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.leaveApplicationBaseUrl}/approve-leave`,leaveRequest, requestHeader);
  }
  rejectLeaveRequest(leaveRequest: ApproveOrRejectLeaveRequest){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.leaveApplicationBaseUrl}/decline-leave`,leaveRequest, requestHeader);
  }
  declineleaveApplication(leaveApplication: any) {
    let httpParams = new HttpParams().set('id', leaveApplication.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: leaveApplication
    };
    return this.http.delete(this.leaveApplicationBaseUrl, requestHeader);
  }
}
