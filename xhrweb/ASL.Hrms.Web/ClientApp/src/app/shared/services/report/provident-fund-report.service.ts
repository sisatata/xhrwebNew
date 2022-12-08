
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { AttendanceReport, BillClaimReport, LeaveBalance, PayslipReport, ProvidentFundReport, TaskCategory, TaskReport } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption, SalaryReport } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class ProvidentFundReportService extends BaseService {
  private providentFundReportBaseUrl = `${this.Base_API_URL}api/Report/provident-fund-report`;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.providentFundReportBaseUrl = `${this.appSettingsService.apiBaseUrl}api/Report/provident-fund-report`;
    
  }

  createProvidenfundPdfReport(providentFundReport: ProvidentFundReport) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.providentFundReportBaseUrl}`, providentFundReport, requestHeader);
  }

}
