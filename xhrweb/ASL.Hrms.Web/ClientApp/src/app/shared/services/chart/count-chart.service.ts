import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeAddress } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class CountChartService extends BaseService {
  private countChartNotificationBaseUrl = this.Base_API_URL +'Notification/get-summary-owner/' ;
  private countChartBaseUrl = this.Base_API_URL+ 'api/Chart/';
  private noticeBaseUrl;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.countChartBaseUrl = this.appSettingsService.apiBaseUrl+ 'AdminDashboard/GetAdminDashboardByManagerAndDate' ;
    this.countChartBaseUrl = this.appSettingsService.apiBaseUrl +'Notification/get-summary-owner/';
    this.countChartBaseUrl = this.appSettingsService.apiBaseUrl + 'api/Chart/';
    this.countChartNotificationBaseUrl =  this.appSettingsService.apiBaseUrl +'Notification/get-summary-owner/' ;
    this.noticeBaseUrl =  this.appSettingsService.apiBaseUrl +'OfficeNotice/';
  }

  getNotificationsChartData(managerId:any):Observable<any>{
    return this.http.get<any>(this.countChartNotificationBaseUrl  + managerId);
}
getBillClaimChartAmount(companyId:any, startDate:any, endDate:any):Observable<number>{
    return this.http.get<any>(this.countChartBaseUrl  +'bill-chart/'+ 'company/'+companyId +'/startDate/'+ startDate +'/endDate/'+endDate);
}
getActiveNotice(companyId:any){
  return this.http.get<any>(`${this.noticeBaseUrl}companyId/${companyId}`);
}
getCurrentYearNewEmployees(companyId:any, currentYear):Observable<any>{
  return this.http.get<any>(`${this.countChartBaseUrl}current-year-new-employees/companyId/${companyId}/currentYear/${currentYear}`);
}
getNewConfirmedEmployees(companyId:any, startDate:any, endDate:any):Observable<any>{
  return this.http.get<any>(`${this.countChartBaseUrl}employee-confirmation/companyId/${companyId}/startDate/${startDate}/endDate/${endDate}`);
}
  
  
  
  


}

