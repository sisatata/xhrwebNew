import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenerateSalary, } from '../../models/payroll/generate-salary';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
import { EmployeePaidSalary } from '../../models/payroll/emp-paid-salary';
import {EarnLeaveEncashment} from '../../models/payroll/earn-leave-encashment';

@Injectable({
  providedIn: 'root'
})
export class EarnLeaveEncashmentService extends BaseService {

  private employeeLeaveEncashmentBaseUrl = `${this.Base_API_URL}EmployeeLeaveEncashment`;
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeLeaveEncashmentBaseUrl = `${this.appSettingsService.apiBaseUrl}EmployeeLeaveEncashment`;
  }

  createEmployeeLeaveEncashment(earnLeaveEncashment: EarnLeaveEncashment): any {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeLeaveEncashmentBaseUrl, earnLeaveEncashment, requestHeader);
  }
  getEarnLeaveEncashmentByCompany(companyId: any): Observable<EarnLeaveEncashment[]> {
    return this.http.get<EarnLeaveEncashment[]>(`${this.employeeLeaveEncashmentBaseUrl}/company/${companyId}`);
  }
  getEmployeeSalaryProcessedData(employeeId: any, financialyearId: any, monthCycleId: any): any {
   // return this.http.get(this.generatesalaryBaseUrl + "/employee/" + employeeId + "/financialYear/" + financialyearId + "/monthCycle/" + monthCycleId);
  }
// getCompanySalaryProcessedData(empPaidSalary : EmployeePaidSalary):Observable<EmployeePaidSalary[]>{
//   var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
//  // return this.http.post<EmployeePaidSalary[]>(`${this.generatesalaryBaseUrl}/employee-salary-processed-data`, empPaidSalary, requestHeader);
// }

}
