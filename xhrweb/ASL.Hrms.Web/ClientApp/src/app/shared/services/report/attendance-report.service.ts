
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { AttendanceDetailsReport, AttendanceReport, LeaveBalance, TaskCategory } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class AttendanceReportService extends BaseService {
  private attendanceBaseUrl = this.Base_API_URL + 'api/Report/attendance-report';
  private reportBaseUrl = this.Base_API_URL + 'api/Report/';
  private attendanceDetailsBaseUrl = this.Base_API_URL + 'api/Report/attendance-details-report';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.attendanceBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/attendance-report';
    this.attendanceDetailsBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/attendance-details-report';
    this.reportBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/';
  }

  createAttendancePdfReport(attendanceReport: AttendanceReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.attendanceBaseUrl}-pdf`, attendanceReport, requestHeader);
  }
  createAttendanceSummaryPdfReport(attendanceReport: AttendanceReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.attendanceBaseUrl+'-summary-pdf' , attendanceReport, requestHeader);
  }
  createJobCardSummaryReport(jobCardReport: AttendanceReport){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(`${this.reportBaseUrl}job-card-summary` , jobCardReport, requestHeader);
  }
  createAttendanceDetailsPdfReport(attendanceDetailsReport: AttendanceDetailsReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(`${this.attendanceDetailsBaseUrl}-pdf` , attendanceDetailsReport, requestHeader);
  }

  createAttendanceExcelReport(attendanceReport: AttendanceReport) {
    const httpOption: Object = {
      observe: 'response',
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'blob'
    };
    return this.http.post<any>(this.attendanceBaseUrl, attendanceReport, httpOption);
  }

}




