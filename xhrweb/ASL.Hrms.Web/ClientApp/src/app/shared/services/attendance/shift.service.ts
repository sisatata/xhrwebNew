import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Shift } from '../../models/attendance/shift';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class ShiftService extends BaseService{
  private shiftBaseUrl = this.Base_API_URL + 'Shift';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.shiftBaseUrl = this.appSettingsService.apiBaseUrl + 'Shift';
  }

  getAllShiftByCompany(companyId:any):Observable<Shift[]>{
    return this.http.get<Shift[]>(this.shiftBaseUrl+'/company/'+companyId);
  }

  getShiftById(shiftId: number): Observable<any[]> {
    return this.http.get<any[]>(this.shiftBaseUrl  + shiftId);
  }
  createShift(shift:Shift) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.shiftBaseUrl, shift, requestHeader);
  }

  editShift(shift: Shift) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.shiftBaseUrl, shift, requestHeader);
  }

  deleteShift(shift: any) {

    let httpParams = new HttpParams().set('shiftGroupId', shift.id);
    var input = { id: shift.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: input
    };
    return this.http.delete(this.shiftBaseUrl, requestHeader);
  }

}
