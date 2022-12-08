import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeAddress, PayrollChart } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class PayrollChartService extends BaseService {
  private payrollChartBaseUrl = this.Base_API_URL +'api/Chart' ;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
   // this.attendanceChartBaseUrl = this.appSettingsService.apiBaseUrl+ +'AdminDashboard/GetAdminDashboardByManagerAndDate' ;
   this.payrollChartBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Chart';
  }

 getPayrollChart(companyId:any, yearName:string, monthName:string): Observable<PayrollChart>{
    return this.http.get<any>(`${this.payrollChartBaseUrl}/payroll-chart/company/${companyId}/financialYearName/${yearName}/monthCycleName/${monthName}`);
 }
 getYearlyPayrollChartByCompanyIdAndYearId(companyId:any, financilaYearId:any): Observable<PayrollChart>{
  return this.http.get<any>(`${this.payrollChartBaseUrl}/yearly-payroll-chart/companyId/${companyId}/financialYearId/${financilaYearId}`);
 }
 getDepartmentMonthlySalary(companyId:any, financilaYearName:any, monthCYcleName:any): Observable<any>{
  return this.http.get<any>(`${this.payrollChartBaseUrl}/department-monthly-salary/companyId/${companyId}/financialYearName/${financilaYearName}/monthCycleName/${monthCYcleName}`);
 }
  
  
  


}

