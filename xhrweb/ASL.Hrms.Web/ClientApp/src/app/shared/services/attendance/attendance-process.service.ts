import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AttendanceProcess } from '../../models/attendance/attendance-process';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class AttendanceProcessService extends BaseService{
  private attendanceProcessUrl = this.Base_API_URL + 'AttendanceProcessedData'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super()
    this.attendanceProcessUrl = this.appSettingsService.apiBaseUrl + 'AttendanceProcessedData';
  }

  processAttendance(processAttendance:AttendanceProcess) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.attendanceProcessUrl+'/process-attendance',processAttendance, requestHeader);
  }

}
