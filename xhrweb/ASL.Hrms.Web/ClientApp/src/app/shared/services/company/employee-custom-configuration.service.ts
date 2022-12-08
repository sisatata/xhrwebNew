import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CustomConfiguration, CustomEmployeeConfiguration } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class CustomEmployeeConfigurationService extends BaseService {
  private customEmployeeConfigurationBaseUrl = this.Base_API_URL + 'EmployeeCustomConfiguration';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.customEmployeeConfigurationBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeCustomConfiguration';
  }


  getAllDefaultConfigurations(companyId): Observable<CustomConfiguration[]> {
    return this.http.get<CustomConfiguration[]>(this.customEmployeeConfigurationBaseUrl+'/company/'+companyId);
  }

  createConfiguration(configuration: CustomEmployeeConfiguration) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.customEmployeeConfigurationBaseUrl, configuration, requestHeader);
  }

  editConfiguration(configuration: CustomEmployeeConfiguration) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.customEmployeeConfigurationBaseUrl, configuration, requestHeader);
  }

  deleteConfigurationById(configurationId: any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.customEmployeeConfigurationBaseUrl + '/' + configurationId, requestHeader);
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

    return this.http.delete(this.customEmployeeConfigurationBaseUrl, requestHeader);
  }
  deleteEmployeeCustomConfig(configuration :CustomEmployeeConfiguration){
    let httpParams = new HttpParams().set('id', configuration.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: configuration
    };

    return this.http.delete(this.customEmployeeConfigurationBaseUrl, requestHeader);
  }


}
