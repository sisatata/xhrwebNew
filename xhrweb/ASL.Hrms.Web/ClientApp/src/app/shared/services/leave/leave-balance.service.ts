import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LeaveProcess } from '../../models/leave/leave-process';
import { LeaveBalance } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class LeaveBalanceService extends BaseService{
  private leaveProcessUrl = this.Base_API_URL + 'LeaveBalance'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.leaveProcessUrl = this.appSettingsService.apiBaseUrl + 'LeaveBalance';
  }

  processLeave(processLeave:LeaveProcess) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.leaveProcessUrl +'/process-balance',processLeave, requestHeader);
  }
  getEmployeeLeaveBalanceByCalendar(employeeId :any, leaveCalendar: any):any{
    return this.http.get<LeaveBalance[]>(this.leaveProcessUrl + '/employee/' + employeeId + '/calendar/'+ leaveCalendar);
  }
  getLeaveBalanceSummary(leaveBalance: LeaveBalance){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.leaveProcessUrl}/leave-balance`,leaveBalance, requestHeader);
  }

}
