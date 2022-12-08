import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeAddress } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class AttendanceChartService extends BaseService {
  private attendanceChartBaseUrl = this.Base_API_URL +'AdminDashboard/GetAdminDashboardByManagerAndDate' ;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
   this.attendanceChartBaseUrl = this.appSettingsService.apiBaseUrl+'AdminDashboard/GetAdminDashboardByManagerAndDate' ;
  }

  getAttendanceChartData(companyId:any, managerId:any, punchDate:any):Observable<any>{
    return this.http.get<any>(this.attendanceChartBaseUrl  + "/companyId/"+companyId+"/managerId/"+managerId+"/punchDate/"+punchDate);
}
  
  
  
  


}

