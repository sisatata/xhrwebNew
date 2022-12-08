import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Holiday } from '../../models';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class HolidayService extends BaseService {
  private holidayBaseUrl = this.Base_API_URL + 'Holiday';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.holidayBaseUrl = this.appSettingsService.apiBaseUrl + 'Holiday';}

  getAllHolidays(): Observable<Holiday[]> {
    return this.http.get<Holiday[]>(this.holidayBaseUrl);
  }


  getHolidayByCompanyId(companyId: any): Observable<any> {
    return this.http.get<any>(this.holidayBaseUrl + "/company/" + companyId);
  }

  getHolidayByFinancialYear(companyId: any, financialyear: string): Observable<any> {
    return this.http.get<any>(`${this.holidayBaseUrl}/company/${companyId}/financialYear/${financialyear}`);
  }

  createHoliday(holiday: Holiday) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.holidayBaseUrl, holiday, requestHeader);
  }

  editHoliday(holiday: Holiday) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.holidayBaseUrl, holiday, requestHeader);
  }

  deleteHoliday(holiday: any) {

    let httpParams = new HttpParams().set('holidayId', holiday.id);
    var input = { id: holiday.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.holidayBaseUrl, requestHeader);
  }
}



