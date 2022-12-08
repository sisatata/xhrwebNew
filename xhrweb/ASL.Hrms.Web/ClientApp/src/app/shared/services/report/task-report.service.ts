
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { AttendanceReport, BillClaimReport, LeaveBalance, PayslipReport, TaskCategory, TaskReport } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption, SalaryReport } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class TaskReportService extends BaseService {
  private taskReportBaseUrl = `${this.Base_API_URL}api/Report/task-detail-report`;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.taskReportBaseUrl = `${this.appSettingsService.apiBaseUrl}api/Report/task-detail-report`;
    
  }

 

  createTaskPdfReport(taskReport: TaskReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.taskReportBaseUrl}-pdf`, taskReport, requestHeader);
  }

}
