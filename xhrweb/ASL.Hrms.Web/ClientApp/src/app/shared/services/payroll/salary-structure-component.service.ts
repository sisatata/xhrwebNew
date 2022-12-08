import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SalaryStructureComp } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class SalaryStructureComponentService extends BaseService {

  private salarystructurecomponentBaseUrl = this.Base_API_URL + 'SalaryStructureComponent';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.salarystructurecomponentBaseUrl = this.appSettingsService.apiBaseUrl + 'SalaryStructureComponent';
  }

  getAllSalaryStructureComponentByCompanyId(companyId): Observable<SalaryStructureComp[]> {
    return this.http.get<SalaryStructureComp[]>(this.salarystructurecomponentBaseUrl + '/company/' + companyId);
  }

  getAllSalaryStructureComponentByStructureId(structureId): Observable<SalaryStructureComp[]> {
    return this.http.get<SalaryStructureComp[]>(this.salarystructurecomponentBaseUrl + '/salarystructure/' + structureId);
  }

  getSalaryStructureComponentById(salarystructurecomponentId: number): Observable<any[]> {
    return this.http.get<any[]>(this.salarystructurecomponentBaseUrl + salarystructurecomponentId);
  }

  createSalaryStructureComponent(salarystructurecomponent: SalaryStructureComp) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.salarystructurecomponentBaseUrl, salarystructurecomponent, requestHeader);
  }

  editSalaryStructureComponent(salarystructurecomponent: SalaryStructureComp) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.salarystructurecomponentBaseUrl, salarystructurecomponent, requestHeader);
  }

  deleteSalaryStructureComponentById(salarystructurecomponentId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.salarystructurecomponentBaseUrl + '/' + salarystructurecomponentId, requestHeader);
  }

  deleteSalaryStructureComponent(salarystructurecomponent: any) {
    let httpParams = new HttpParams().set('id', salarystructurecomponent.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: salarystructurecomponent
    };
    return this.http.delete(this.salarystructurecomponentBaseUrl, requestHeader);
  }

}
