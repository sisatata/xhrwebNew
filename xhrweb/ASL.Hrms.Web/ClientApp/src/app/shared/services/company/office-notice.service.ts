import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OfficeNotice } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class OfficeNoticeService extends BaseService {
  private officeNoticeBaseUrl = this.Base_API_URL + 'OfficeNotice';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.officeNoticeBaseUrl = this.appSettingsService.apiBaseUrl + 'OfficeNotice';
  }

  getOfficeNoticeListByCompany(companyId: any): Observable<OfficeNotice[]> {
    return this.http.get<OfficeNotice[]>(this.officeNoticeBaseUrl + '/companyId/' + companyId);
  }

  getOfficeNoticeById(id: any): Observable<OfficeNotice> {
    return this.http.get<OfficeNotice>(this.officeNoticeBaseUrl + id);
  }

  createOfficeNotice(officeNotice: OfficeNotice) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.officeNoticeBaseUrl, officeNotice, requestHeader);
  }

  updateOfficeNotice(officeNotice: OfficeNotice) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.officeNoticeBaseUrl, officeNotice, requestHeader);
  }

  deleteOfficeNotice(officeNotice: OfficeNotice) {
    let httpParams = new HttpParams().set('officeNoticeId', officeNotice.id);
    var input = { id: officeNotice.id };
    var requestHeader = {
      headers: new HttpHeaders({
        'Content-Type': 'apllication/json', 'No-Auth': 'False'
      }),
      body: input
    };
    return this.http.delete(this.officeNoticeBaseUrl, requestHeader);
  }

}
