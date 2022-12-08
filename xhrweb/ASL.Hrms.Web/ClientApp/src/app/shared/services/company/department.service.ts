import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService extends BaseService {
  private departmentBaseUrl = this.Base_API_URL + 'Department';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.departmentBaseUrl = this.appSettingsService.apiBaseUrl + 'Department';}


  getAllDepartmentByBranchId(branchId): Observable<Department[]> {
    return this.http.get<Department[]>(this.departmentBaseUrl + '/branch/' + branchId);
  }
  getAllDepartmentByBranchIds(departmentFilter:any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.departmentBaseUrl}/get-by-branches`,departmentFilter,requestHeader );
  }
  getBranchById(departmentId: any): Observable<any[]> {
    return this.http.get<any[]>(this.departmentBaseUrl + departmentId);
  }

  createDepartment(department: Department) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.departmentBaseUrl, department, requestHeader);
  }

  editDepartment(department: Department) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.departmentBaseUrl, department, requestHeader);
  }

  deleteDepartmentById(departmentId: any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.departmentBaseUrl + '/' + departmentId, requestHeader);
  }

  deleteDepartment(department: any) {

    let httpParams = new HttpParams().set('id', department.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: department
    };

    return this.http.delete(this.departmentBaseUrl, requestHeader);
  }


}
