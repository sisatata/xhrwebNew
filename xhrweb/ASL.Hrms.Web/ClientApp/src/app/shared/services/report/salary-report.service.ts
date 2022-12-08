
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { AttendanceReport, BillClaimReport, LeaveBalance, PayslipReport, TaskCategory } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption, SalaryReport } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class SalaryReportService extends BaseService {
  private salaryReportBaseUrl = this.Base_API_URL + 'api/Report/salary-report';
  private payslipReportBaseUrl = this.Base_API_URL + 'api/Report/payslip-report'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.salaryReportBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/salary-report';
    this.payslipReportBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/payslip-report';
  }

  createSalaryPdfReport(salaryReport: SalaryReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.salaryReportBaseUrl + '-pdf', salaryReport, requestHeader);
  }

  createPayslipPdfReport(payslipReport: PayslipReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.payslipReportBaseUrl + '-pdf', payslipReport, requestHeader);
  }

}
