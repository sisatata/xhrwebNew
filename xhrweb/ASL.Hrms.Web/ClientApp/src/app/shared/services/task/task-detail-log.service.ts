
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { LeaveBalance, TaskCategory, TaskDetail, TaskDetailLog } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';
@Injectable({
  providedIn: 'root'
})
export class TaskDetailLogService extends BaseService{
  private taskDetailLogBaseUrl = this.Base_API_URL + 'TaskDetailLog'
  constructor(private http:HttpClient, private appSettingsService: AppSettingsService) { super()
    this.taskDetailLogBaseUrl = this.appSettingsService.apiBaseUrl + 'TaskDetailLog';
  }

  createTaskDetailLogByTaskDetailId(taskDetailLog: TaskDetailLog) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.taskDetailLogBaseUrl, taskDetailLog, requestHeader);
  }
  getAllTaskDetailLogsByTaskDetailId(taskDetailId:any):Observable<TaskDetailLog[]>{
    return this.http.get<TaskDetailLog[]>(this.taskDetailLogBaseUrl + '/taskDetail/' + taskDetailId);
  }
  getAllTasksDetailByCompanyId(companyId:any):Observable<TaskDetail[]>{
    return this.http.get<TaskDetail[]>(this.taskDetailLogBaseUrl + '/companyId/' + companyId);  
  }

  getAllTasksDetailByCompanyAndParent(companyId:any, parentTaskId:any ):Observable<TaskDetail[]>{
    return this.http.get<TaskDetail[]>(this.taskDetailLogBaseUrl + '/company/' + companyId+'/parentTaskId/' +parentTaskId);
  }
  editTaskDetailLog(taskDetailLog: TaskDetailLog) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.put(this.taskDetailLogBaseUrl, taskDetailLog, requestHeader);
  }

  processLeave(processLeave:LeaveProcess) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.taskDetailLogBaseUrl +'/process-balance',processLeave, requestHeader);
  }
  getEmployeeLeaveBalanceByCalendar(employeeId :any, leaveCalendar: any):any{
    return this.http.get<LeaveBalance[]>(this.taskDetailLogBaseUrl + '/employee/' + employeeId + '/calendar/'+ leaveCalendar);
  }
  deleteTaskDetail(taskDetail: TaskDetail) {
    let httpParams=new HttpParams().set('id',taskDetail.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:taskDetail
    };
    return this.http.delete(this.taskDetailLogBaseUrl , requestHeader);
  
  
  }
}
