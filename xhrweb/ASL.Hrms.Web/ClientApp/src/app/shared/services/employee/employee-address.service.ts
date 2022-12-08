import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeAddress } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeeAddressService extends BaseService {
  private employeeAddressBaseUrl = this.Base_API_URL + 'EmployeeAddress';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeAddressBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeeAddress';
  }

  getAllEmployeeAddresss(): Observable<EmployeeAddress[]> {
    return this.http.get<EmployeeAddress[]>(this.employeeAddressBaseUrl);
  }


  getEmployeeAddressByEmployeeId(employeeId: any): Observable<any> {
    return this.http.get<any>(this.employeeAddressBaseUrl + "/employeeId/" + employeeId);
  }

  createEmployeeAddress(employeeAddress: EmployeeAddress) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeeAddressBaseUrl, employeeAddress, requestHeader);
  }

  editEmployeeAddress(employeeAddress: EmployeeAddress) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeeAddressBaseUrl, employeeAddress, requestHeader);
  }
  getAllDistricts() {
    return this.http.get<any>(this.Base_API_URL + "District");
  }
  getAllThanas(districtId: any) {
    let params = new HttpParams().set("id", districtId);
    return this.http.get<any>(this.Base_API_URL + "Upazila/" + "districtId/" + "id/", { params });
  }


  //deleteEmployeeAddressById(id: any) {
  //  var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
  //  return this.http.delete(this.employeeAddressBaseUrl + '/' + id, requestHeader);
  //}

  deleteEmployeeAddress(employeeAddress: any) {

    let httpParams = new HttpParams().set('employeeAddressId', employeeAddress.id);
    //httpParams.set('bbb', '222');
    var input = { employeeAddressId: employeeAddress.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: input
    };

    return this.http.delete(this.employeeAddressBaseUrl, requestHeader);
  }


}

