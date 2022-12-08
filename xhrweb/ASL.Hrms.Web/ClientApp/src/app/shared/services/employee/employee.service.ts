import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Department } from '../../models/company/department';
import { Employee, EmployeeFilter, EmployeePagedListInput } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeService extends BaseService {
  private employeeBaseUrl = this.Base_API_URL + 'Employee';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeBaseUrl = this.appSettingsService.apiBaseUrl + 'Employee';
  }

  getAllEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.employeeBaseUrl);
  }

  getAllEmployeePagedList(input: any): any {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post<any>(this.employeeBaseUrl + "/paged-list", input, requestHeader);
  }
  getAllEmployeePagedListWithoutPagination(input: EmployeeFilter){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeBaseUrl + "/filtered-list", input, requestHeader);
  }

  getAllEmployeeByBranchId(branchId): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.employeeBaseUrl + '/branch/' + branchId);
  }

  getBranchById(employeeId: any): Observable<any[]> {
    return this.http.get<any[]>(this.employeeBaseUrl + employeeId);
  }

  createEmployee(employee: Employee) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeBaseUrl, employee, requestHeader);
  }
  getAllEmployeeFilteredList(input: EmployeeFilter) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.employeeBaseUrl}/filtered-paged-list`, input, requestHeader);
  }

   employeeListExport(input: EmployeeFilter) {
    const httpOption: Object = {
      observe: 'response',
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'blob'
    };
    return this.http.post<any>(`${this.employeeBaseUrl}/employee-list-export`, input, httpOption);
  }


  editEmployee(employee: Employee) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeBaseUrl, employee, requestHeader);
  }
importEmployeeExcel(employeeExcel:any){
  var requestHeader = { headers: new HttpHeaders({  'No-Auth': 'False' }) };
  return this.http.post(this.employeeBaseUrl, employeeExcel, requestHeader);
}
  deleteEmployeeById(employeeId: any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.employeeBaseUrl + '/' + employeeId, requestHeader);
  }

  deleteEmployee(employee: any) {

    let httpParams = new HttpParams().set('id', employee.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: employee
    };

    return this.http.delete(this.employeeBaseUrl, requestHeader);
  }

}

