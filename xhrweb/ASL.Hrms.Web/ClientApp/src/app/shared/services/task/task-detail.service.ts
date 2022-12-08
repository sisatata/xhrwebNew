
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { LeaveBalance, TaskCategory, TaskDetail, TaskDetailExport } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class TaskDetailService extends BaseService{
  private taskDetailBaseUrl = this.Base_API_URL + 'TaskDetail'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.taskDetailBaseUrl = this.appSettingsService.apiBaseUrl + 'TaskDetail';
  }

  createTaskCategory(taskDetail: TaskDetail) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.taskDetailBaseUrl, taskDetail, requestHeader);
  }
  getAllParentTasksDetailByCompany(employeeId:any, startDate:any, endDate:any, userId:any):Observable<TaskDetail[]>{
    return this.http.get<TaskDetail[]>(`${this.taskDetailBaseUrl}/userId/${userId}/employeeId/${employeeId}/startDate/${startDate}/endDate/${endDate} `);
  }

  getTaskListExcel(taskDetailExport: TaskDetailExport):Observable<string>{
    const httpOption: Object = {
      observe: 'response',
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'blob'
    };
    return this.http.post<any>(`${this.taskDetailBaseUrl}/taskDetailList-excel`,taskDetailExport, httpOption);
  }
  getAllTasksDetailByCompanyId(companyId:any):Observable<TaskDetail[]>{
    return this.http.get<TaskDetail[]>(this.taskDetailBaseUrl + '/companyId/' + companyId);
  }

  getAllTasksDetailByCompanyAndParent( parentTaskId:any):Observable<TaskDetail[]>{
    return this.http.get<TaskDetail[]>(`${this.taskDetailBaseUrl}/parentTaskId/${parentTaskId}` );
  }
  editTaskDetail(taskDetail: TaskDetail) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.put(this.taskDetailBaseUrl, taskDetail, requestHeader);
  }


  deleteTaskDetail(taskDetail: TaskDetail) {
    let httpParams=new HttpParams().set('id',taskDetail.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:taskDetail
    };
    return this.http.delete(this.taskDetailBaseUrl , requestHeader);
  
  
  }
}
