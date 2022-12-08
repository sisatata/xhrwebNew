import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeExperience } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';


@Injectable({
  providedIn: 'root'
})

export class EmployeeExperienceService extends BaseService {
  private employeeExperienceBaseUrl = this.Base_API_URL + 'EmployeeExperience';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeExperienceBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeExperience';
  }

  getAllEmployeeExperiences(): Observable<EmployeeExperience[]> {
    return this.http.get<EmployeeExperience[]>(this.employeeExperienceBaseUrl);
  }


  getEmployeeExperienceByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeExperienceBaseUrl + "/employeeId/" + employeeId);
  }

  createEmployeeExperience(employeeExperience: EmployeeExperience) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeExperienceBaseUrl, employeeExperience, requestHeader);
  }

  editEmployeeExperience(employeeExperience: EmployeeExperience) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeExperienceBaseUrl, employeeExperience, requestHeader);
  }

  deleteEmployeeExperience(employeeExperience: any) {

    let httpParams = new HttpParams().set('employeeExperienceId', employeeExperience.id);
    var input = { id: employeeExperience.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeeExperienceBaseUrl, requestHeader);
  }
}


