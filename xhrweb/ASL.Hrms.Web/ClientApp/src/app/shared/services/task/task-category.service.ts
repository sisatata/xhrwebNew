
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { LeaveBalance, TaskCategory } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class TaskCategoryService extends BaseService {
  private taskCategoryBaseUrl = this.Base_API_URL + 'TaskCategory'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.taskCategoryBaseUrl = this.appSettingsService.apiBaseUrl + 'TaskCategory';
  }

  createTaskCategory(taskCategory: TaskCategory) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.taskCategoryBaseUrl, taskCategory, requestHeader);
  }
  editTaskCategory(taskCategory: TaskCategory) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.taskCategoryBaseUrl, taskCategory, requestHeader);
  }

  processLeave(processLeave: LeaveProcess) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.taskCategoryBaseUrl + '/process-balance', processLeave, requestHeader);
  }
  getAllTaskCategory(companyId: any): Observable<TaskCategory[]> {
    return this.http.get<TaskCategory[]>(this.taskCategoryBaseUrl + '/company/' + companyId);
  }
  deleteTaskCategory(taskCategory: TaskCategory) {
    let httpParams = new HttpParams().set('id', taskCategory.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: taskCategory
    };
    return this.http.delete(this.taskCategoryBaseUrl, requestHeader);


  }
}
