import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';

import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Designation } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class DesignationService extends BaseService {
  private designationBaseUrl = this.Base_API_URL + 'Designation'
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.designationBaseUrl = this.appSettingsService.apiBaseUrl + 'Designation';
  }


  getAllDesignationByDepartmentId(entityId): Observable<Designation[]> {
    return this.http.get<Designation[]>(this.designationBaseUrl + '/entity/' + entityId);
  }
  getAllDesignationByDepartmentIds(designationFilter:any) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.designationBaseUrl}/get-by-departments`,designationFilter,requestHeader );
  }
  getDesignationById(designationId: any): Observable<any[]> {
    return this.http.get<any[]>(this.designationBaseUrl + designationId);
  }

  createDesignation(designation: Designation) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.designationBaseUrl, designation, requestHeader);
  }

  editDesignation(designation: Designation) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.designationBaseUrl, designation, requestHeader);
  }

  deleteDesignationById(designationId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.designationBaseUrl + '/' + designationId, requestHeader);
  }

  deleteDesignation(designation: any) {
    let httpParams = new HttpParams().set('id', designation.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: designation
    };
    return this.http.delete(this.designationBaseUrl, requestHeader);
  }


}
