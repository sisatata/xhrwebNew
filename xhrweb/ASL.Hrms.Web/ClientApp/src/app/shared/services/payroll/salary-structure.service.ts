import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SalaryStructure } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class SalaryStructureService extends BaseService{
  
  private salarystructureBaseUrl = this.Base_API_URL +'SalaryStructure';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.salarystructureBaseUrl = this.appSettingsService.apiBaseUrl + 'SalaryStructure';
  }

  getAllSalaryStructureByCompanyId(companyId):Observable<SalaryStructure[]>{
    return this.http.get<SalaryStructure[]>(this.salarystructureBaseUrl+'/company/'+companyId);
   }
 
   getSalaryStructureById(salarystructureId: number): Observable<any[]> {
     return this.http.get<any[]>(this.salarystructureBaseUrl  + salarystructureId);
   }

  createSalaryStructure(salarystructure: SalaryStructure) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False'}) };
    return this.http.post(this.salarystructureBaseUrl, salarystructure, requestHeader);
  }

  editSalaryStructure(salarystructure: SalaryStructure) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.salarystructureBaseUrl, salarystructure, requestHeader);
  }

  deleteSalaryStructureById(salarystructureId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.salarystructureBaseUrl + '/' + salarystructureId, requestHeader);
  }

  deleteSalaryStructure(salarystructure: any) {
    let httpParams=new HttpParams().set('id',salarystructure.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body:salarystructure
    };
    return this.http.delete(this.salarystructureBaseUrl , requestHeader);
  }
  
}
