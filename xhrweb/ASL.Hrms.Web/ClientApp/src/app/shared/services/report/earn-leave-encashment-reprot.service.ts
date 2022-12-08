import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
import {EarnLeaveEncashmentReport} from '../../models/report/earn-leave-encashment-report';

@Injectable({
  providedIn: 'root'
})
export class EarnLeaveEncashmentReportService extends BaseService {

  private employeeLeaveEncashmentReportBaseUrl = `${this.Base_API_URL}api/Report`;
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeLeaveEncashmentReportBaseUrl = `${this.appSettingsService.apiBaseUrl}api/Report`;
  }

  createEmployeeLeaveEncashmentReport(earnLeaveEncashmentReport: EarnLeaveEncashmentReport): any {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.employeeLeaveEncashmentReportBaseUrl}/earn-leave-encashment-report`, earnLeaveEncashmentReport, requestHeader);
  }
 
}
