import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { GenerateSalary, } from '../../models/payroll/generate-salary';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
import { EmployeePaidSalary } from '../../models/payroll/emp-paid-salary';

@Injectable({
  providedIn: 'root'
})
export class EmployeeSalaryProcessService extends BaseService {

  private generatesalaryBaseUrl = this.Base_API_URL + "EmployeeSalaryProcessedData";
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.generatesalaryBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeSalaryProcessedData';
  }

  createGenerateSalary(generateSalry: GenerateSalary): any {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.generatesalaryBaseUrl + "/generate-salary", generateSalry, requestHeader);
  }
  getEmployeesSalaryProcessedData(companyId: any, financialyearId: any, monthCycleId: any): any {
    return this.http.get(this.generatesalaryBaseUrl + "/company/" + companyId + "/financialYear/" + financialyearId + "/monthCycle/" + monthCycleId);
  }
  getEmployeeSalaryProcessedData(employeeId: any, financialyearId: any, monthCycleId: any): any {
    return this.http.get(this.generatesalaryBaseUrl + "/employee/" + employeeId + "/financialYear/" + financialyearId + "/monthCycle/" + monthCycleId);
  }
getCompanySalaryProcessedData(empPaidSalary : EmployeePaidSalary):Observable<EmployeePaidSalary[]>{
  var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
  return this.http.post<EmployeePaidSalary[]>(`${this.generatesalaryBaseUrl}/employee-salary-processed-data`, empPaidSalary, requestHeader);
}

}
