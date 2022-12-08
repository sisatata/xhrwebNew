import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AttendanceProcessData } from '../../models/attendance/attendance-process-data';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';
import { UserRoleDto } from '../../models';
import { AnyMxRecord } from 'dns';

@Injectable({
  providedIn: 'root'
})
export class RoleManagementDataService extends BaseService {
  private roleManagementBaseUrl = this.Base_API_URL + 'api/Account'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super()
    this.roleManagementBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Account';
  }

  getAttendanceProcessDataByEmployee(employeeId: any, startDate: any, endDate: any): Observable<AttendanceProcessData[]> {
    return this.http.get<AttendanceProcessData[]>(this.roleManagementBaseUrl + "/GetAttendanceDataByEmployeeAndDateRange" + "/employeeId/" + employeeId + "/startDate/" + startDate + "/endDate/" + endDate);
  }
  getAttendanceProcessDataByCompany(compnayId: any, startDate: any, endDate: any): Observable<AttendanceProcessData[]> {
    return this.http.get<AttendanceProcessData[]>(this.roleManagementBaseUrl + "/GetByCompanyAndDateRange/" + "companyId/" + compnayId + "/startDate/" + startDate + "/endDate/" + endDate);
  }
  getRoles():Observable<any>{
      
     return this.http.get<any>(this.roleManagementBaseUrl + '/roles'); 
  }
  getRolesByEmployee(employeeId:any):Observable<any>{
    return this.http.get<any>(this.roleManagementBaseUrl + '/roles/'+employeeId); 
  }
  assignRoles(userDto:any)
  {
     // console.log(userDto);
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.roleManagementBaseUrl+'/users/assign-role',userDto, requestHeader);
 }
}
