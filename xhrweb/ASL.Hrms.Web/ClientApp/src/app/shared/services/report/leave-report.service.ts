
import { BaseService } from '../base.service';


import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { LeaveReport } from '../../models/report/leave-report';
import { AppSettingsService } from '../../../app-settings.service';
import { LeaveDetailsReport } from '../../models';


@Injectable({
  providedIn: 'root'
})
export class LeaveReportService extends BaseService{
  private leaveReportBaseUrl = this.Base_API_URL + 'api/Report/leave-report';
  private leaveDetailsReportBaseUrl = this.Base_API_URL + 'api/Report/leave-details-report';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.leaveReportBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/leave-report';
    this.leaveDetailsReportBaseUrl = `${this.appSettingsService.apiBaseUrl}api/Report/leave-details-report`;
  }

  createLeavePdfReport(leaveReport: LeaveReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.leaveReportBaseUrl+'-pdf' , leaveReport, requestHeader);
  }

  createLeaveDetailsPdfReport(leaveDetailsReport: LeaveDetailsReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(`${this.leaveDetailsReportBaseUrl}-pdf` , leaveDetailsReport, requestHeader);
  }
 
}




