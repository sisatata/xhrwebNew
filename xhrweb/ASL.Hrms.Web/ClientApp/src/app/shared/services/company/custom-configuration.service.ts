import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomConfiguration, CustomEmployeeConfiguration } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class CustomConfigurationService extends BaseService {
  private customConfigurationBaseUrl = this.Base_API_URL + 'CustomConfiguration';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.customConfigurationBaseUrl = this.appSettingsService.apiBaseUrl + 'CustomConfiguration';
  }


  getAllDefaultConfigurations(companyId): Observable<CustomConfiguration[]> {
    return this.http.get<CustomConfiguration[]>(this.customConfigurationBaseUrl + '/company/' + companyId);
  }

  createConfiguration(configuration: CustomConfiguration) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.customConfigurationBaseUrl, configuration, requestHeader);
  }

  editConfiguration(configuration: CustomConfiguration) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.customConfigurationBaseUrl, configuration, requestHeader);
  }

  deleteConfigurationById(configurationId: any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.customConfigurationBaseUrl + '/' + configurationId, requestHeader);
  }

  deleteConfiguration(configuration: any) {

    let httpParams = new HttpParams().set('id', configuration.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: configuration
    };

    return this.http.delete(this.customConfigurationBaseUrl, requestHeader);
  }
  getEmployeesByCustomConfigurationByCopanyIdAndCode(companyId: any, code: any):  Observable<CustomEmployeeConfiguration[]>  {
    return this.http.get<CustomEmployeeConfiguration[]>(this.customConfigurationBaseUrl + '/company/' + companyId + '/code/' + code);
  }
  getEmployeeConfigHistoryByCode(employeeId: any, code:any):Observable<CustomEmployeeConfiguration>{
    return this.http.get<CustomEmployeeConfiguration>(this.customConfigurationBaseUrl + '/employee/' + employeeId +'/code/' + code);
  }



}
