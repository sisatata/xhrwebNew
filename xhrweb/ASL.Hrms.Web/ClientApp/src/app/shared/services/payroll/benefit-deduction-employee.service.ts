import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Bank } from '../../models/payroll/bank';
import { BaseService } from '../base.service';



import { BenefitDeductionEmployee } from '../../models/payroll/benefit-deduction-employee';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class BenefitDeductionEmployeeService extends BaseService {

  private benefitDeductionEmployeeBaseUrl = this.Base_API_URL + 'BenefitDeductionEmployeeAssigned';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.benefitDeductionEmployeeBaseUrl = this.appSettingsService.apiBaseUrl + 'BenefitDeductionEmployeeAssigned';
  }


  getAllBenefitDeductionEmployeesByCompanyId(companyId: any): Observable<BenefitDeductionEmployee[]> {
    return this.http.get<BenefitDeductionEmployee[]>(this.benefitDeductionEmployeeBaseUrl + '/companyId/' + companyId);
  }

  deleteEmployeeBenefitDeduction(BenefitDeductionEmployee: BenefitDeductionEmployee) {
    let httpParams = new HttpParams().set('id', BenefitDeductionEmployee.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: BenefitDeductionEmployee
    };
    return this.http.delete(this.benefitDeductionEmployeeBaseUrl, requestHeader);
  }
  createEmployeeBenefitDeduction(BenefitDeductionEmployee: BenefitDeductionEmployee) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.benefitDeductionEmployeeBaseUrl, BenefitDeductionEmployee, requestHeader);
  }

  editEmployeeBenefitDeduction(BenefitDeductionEmployee: BenefitDeductionEmployee) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.benefitDeductionEmployeeBaseUrl, BenefitDeductionEmployee, requestHeader);
  }




}
