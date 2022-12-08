import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService extends BaseService {
  private configurationBaseUrl = this.Base_API_URL + 'DefaultConfiguration';
  private customConfigurationBaseUrl = this.Base_API_URL + 'CustomConfiguration';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.configurationBaseUrl = this.appSettingsService.apiBaseUrl + 'DefaultConfiguration';
  }


  getAllDefaultConfigurations(companyId): Observable<Configuration[]> {
    return this.http.get<Configuration[]>(this.configurationBaseUrl+'/company/'+companyId);
  }

  createConfiguration(configuration: Configuration) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.configurationBaseUrl, configuration, requestHeader);
  }

  editConfiguration(configuration: Configuration) {
    
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.configurationBaseUrl, configuration, requestHeader);
  }

  deleteConfigurationById(configurationId: any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.configurationBaseUrl + '/' + configurationId, requestHeader);
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

    return this.http.delete(this.configurationBaseUrl, requestHeader);
  }


}
