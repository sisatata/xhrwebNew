import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { ApproveOrRejectEmployeePromotionTransfer, EmployeePhone, EmployeePromotionTransfer } from '../../models';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class EmployeePromotionTransferService extends BaseService {
  private employeePrmotionTransferBaseUrl = `${this.Base_API_URL}EmployeePromotionTransfer`;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeePrmotionTransferBaseUrl = this.appSettingsService.apiBaseUrl + 'EmployeePromotionTransfer';
  }

  
getEmployeePromotionTransferByCompanyId(companyId:any):Observable<EmployeePromotionTransfer[]>{
  return this.http.get<EmployeePromotionTransfer[]>(`${this.employeePrmotionTransferBaseUrl}/company/${companyId}`)
}
  createEmployeePromotionTransfer(employeePromotionTransfer: EmployeePromotionTransfer) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.employeePrmotionTransferBaseUrl, employeePromotionTransfer, requestHeader);
  }
  approveEmployeePromtionTransfer(data: ApproveOrRejectEmployeePromotionTransfer){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.employeePrmotionTransferBaseUrl}/approve-employee-promotion-transfer`, data, requestHeader);
  }
  rejectEmployeePromtionTransfer(data: ApproveOrRejectEmployeePromotionTransfer){
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.employeePrmotionTransferBaseUrl}/decline-employee-promotion-transfer`, data, requestHeader);
  }
  getEmployeePromotionTransferById(Id:any):Observable<EmployeePromotionTransfer>{
    return this.http.get<EmployeePromotionTransfer>(`${this.employeePrmotionTransferBaseUrl}/${Id}`)
  }
  updateEmployeePromotionTransfer(employeePromotionTransfer: EmployeePromotionTransfer) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.employeePrmotionTransferBaseUrl, employeePromotionTransfer, requestHeader);
  }

  
}


