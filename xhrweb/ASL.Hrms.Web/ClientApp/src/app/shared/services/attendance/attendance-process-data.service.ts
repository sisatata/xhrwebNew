import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AttendanceProcessData } from '../../models/attendance/attendance-process-data';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class AttendanceProcessDataService extends BaseService {
  private attendanceProcessUrl = this.Base_API_URL + 'AttendanceProcessedData'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super()
    this.attendanceProcessUrl = this.appSettingsService.apiBaseUrl + 'AttendanceProcessedData';
  }

  getAttendanceProcessDataByEmployee(employeeId: any, startDate: any, endDate: any): Observable<AttendanceProcessData[]> {
    return this.http.get<AttendanceProcessData[]>(this.attendanceProcessUrl + "/GetAttendanceDataByEmployeeAndDateRange" + "/employeeId/" + employeeId + "/startDate/" + startDate + "/endDate/" + endDate);
  }
  getAttendanceProcessDataByCompany(compnayId: any, startDate: any, endDate: any): Observable<AttendanceProcessData[]> {
    return this.http.get<AttendanceProcessData[]>(this.attendanceProcessUrl + "/GetByCompanyAndDateRange/" + "companyId/" + compnayId + "/startDate/" + startDate + "/endDate/" + endDate);
  }
  getEmployeeAttendanceData(input: any){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.attendanceProcessUrl}/GetAttendanceByCompany`, input, requestHeader);
  }
}
