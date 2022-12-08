
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { AttendanceReport, BillClaimReport, ConfirmationReport, EmployeeEnrollReport, LeaveBalance, PayslipReport, TaskCategory } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption, SalaryReport } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class EmployeeEnrollReportService extends BaseService {
  private employeeEnrollReportBaseUrl = this.Base_API_URL + 'api/Report/employee-enroll-report-pdf';
  private confirmationReportBaseUrl = this.Base_API_URL + 'api/Report/employee-confirmation-report-pdf';
  
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeEnrollReportBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/employee-enroll-report-pdf';
    this.confirmationReportBaseUrl =  this.appSettingsService.apiBaseUrl + 'api/Report/employee-confirmation-report-pdf';
    
  }

  createEmployeeEnrollPdfReport(employeeEnrollReport: EmployeeEnrollReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeEnrollReportBaseUrl , employeeEnrollReport, requestHeader);
  }
  createConfirmationPdfReport(confirmationReport: ConfirmationReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.confirmationReportBaseUrl , confirmationReport, requestHeader);
  }

  

}
