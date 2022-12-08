
import { BaseService } from '../base.service';

import { LeaveProcess } from '../../models/leave/leave-process';
import { AttendanceReport, BillClaimReport, BonusReport, LeaveBalance, PayslipReport, TaskCategory } from '../../models';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IncomeTaxPayerType, IncomeTaxSlab, PaymentOption, SalaryReport } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class BonusReportService extends BaseService {
  private bonusReportBaseUrl = this.Base_API_URL + 'api/Report/bonus-report';
 
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.bonusReportBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Report/bonus-report';
   
  }

  createBonusPdfReport(bonusReport: BonusReport) {
     
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.bonusReportBaseUrl + '-pdf', bonusReport, requestHeader);
  }

 

}
