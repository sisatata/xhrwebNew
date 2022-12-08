import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeCard, EmployeeEducation } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class CommonSettingsService extends BaseService {
  private commmonSettingsBaseUrl = `${this.Base_API_URL}CommonLookUpType`;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.commmonSettingsBaseUrl = `${this.appSettingsService.apiBaseUrl}CommonLookUpType`;
    
  }

  getEmployeeCommonSettings(companyId:any, employeeId:any) {
    return this.http.get<any>(`${this.commmonSettingsBaseUrl}/GetCommonSettingsByCompany/companyId/${companyId}/employeeId/${employeeId}`);
  }


 

  
}




