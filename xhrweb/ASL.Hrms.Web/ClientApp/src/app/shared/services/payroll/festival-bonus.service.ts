import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FestivalBonus, FestivalBonusProcess, IncomeTaxInvestment, IncomeTaxParameter, IncomeTaxPayerType, IncomeTaxSlab, PaymentOption } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
@Injectable({
  providedIn: 'root'
})
export class BonsuConfigurationService extends BaseService {
  private bonusConfigurationBaseUrl = this.Base_API_URL + 'BonusConfiguration';
  private bonusProcessBaseUrl = `${this.Base_API_URL}EmployeeBonusProcessedData`; 
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.bonusConfigurationBaseUrl = this.appSettingsService.apiBaseUrl + 'BonusConfiguration';
    this.bonusProcessBaseUrl = `${this.appSettingsService.apiBaseUrl}EmployeeBonusProcessedData`;
  }
  getAllBonusConfigurationByCompany(companyId: any): Observable<FestivalBonus[]> {
    return this.http.get<FestivalBonus[]>(`${this.bonusConfigurationBaseUrl}/companyId/${companyId}`);
  }
  createFestivalBonusConfig(festivalBonus: FestivalBonus) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.bonusConfigurationBaseUrl, festivalBonus, requestHeader);
  }
  editFestivalBonusConfig(festivalBonus: FestivalBonus) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.bonusConfigurationBaseUrl, festivalBonus, requestHeader);
  }
  bonusProcess(bonusProcess: FestivalBonusProcess) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.bonusProcessBaseUrl , bonusProcess, requestHeader);

  }
  deleteFestivalBonusConfiguration(festivalBonus: FestivalBonus) {
    let httpParams = new HttpParams().set('id', festivalBonus.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: festivalBonus
    };
    return this.http.delete(this.bonusConfigurationBaseUrl, requestHeader);
  }
}
