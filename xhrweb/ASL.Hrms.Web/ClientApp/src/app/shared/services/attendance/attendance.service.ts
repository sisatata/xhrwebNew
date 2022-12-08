import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Attendance, EmployeeAttendanceDetails } from '../../models';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
import { EmployeeAttendanceDetailsComponent } from 'src/app/attendance/employee-attendance/employee-attendance-details/employee-attendance-details.component';

@Injectable({
  providedIn: 'root'
})

export class AttendanceService extends BaseService {
  private attendanceBaseUrl = this.Base_API_URL + 'Attendance';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.attendanceBaseUrl = this.appSettingsService.apiBaseUrl + 'Attendance';
  }

  getAllAttendances(): Observable<Attendance[]> {
    return this.http.get<Attendance[]>(this.attendanceBaseUrl);
  }
  getEmployeeAttendanceDetails(employeeId:any, requestDate:any): Observable<EmployeeAttendanceDetails[]> {
    return this.http.get<EmployeeAttendanceDetails[]>(`${this.attendanceBaseUrl}/GetAllClockInOutListByEmployeeAndDate/employeeId/${employeeId}/requestDate/${requestDate}`);
  }

  getAttendanceByCompanyId(companyId: any): Observable<any> {
    return this.http.get<any>(this.attendanceBaseUrl + "/company/" + companyId);
  }

  createAttendance(attendance: Attendance) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.attendanceBaseUrl, attendance, requestHeader);
  }

  editAttendance(attendance: Attendance) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.attendanceBaseUrl, attendance, requestHeader);
  }

  deleteAttendance(attendance: any) {

    let httpParams = new HttpParams().set('attendanceId', attendance.id);
    var input = { id: attendance.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.attendanceBaseUrl, requestHeader);
  }
}



