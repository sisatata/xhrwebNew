import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ShiftGroup } from '../../models';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class ShiftGroupService extends BaseService {
  private shiftGroupBaseUrl = this.Base_API_URL + 'ShiftGroup';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.shiftGroupBaseUrl = this.appSettingsService.apiBaseUrl + 'ShiftGroup';
  }

  getAllShiftGroups(): Observable<ShiftGroup[]> {
    return this.http.get<ShiftGroup[]>(this.shiftGroupBaseUrl);
  }


  getShiftGroupByCompanyId(companyId: any): Observable<any> {
    return this.http.get<any>(this.shiftGroupBaseUrl + "/company/" + companyId);
  }

  createShiftGroup(shiftGroup: ShiftGroup) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.shiftGroupBaseUrl, shiftGroup, requestHeader);
  }

  editShiftGroup(shiftGroup: ShiftGroup) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.shiftGroupBaseUrl, shiftGroup, requestHeader);
  }

  deleteShiftGroup(shiftGroup: any) {

    let httpParams = new HttpParams().set('shiftGroupId', shiftGroup.id);
    var input = { id: shiftGroup.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.shiftGroupBaseUrl, requestHeader);
  }
}



