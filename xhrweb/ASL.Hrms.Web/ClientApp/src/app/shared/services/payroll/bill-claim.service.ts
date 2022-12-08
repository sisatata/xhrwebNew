import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';
import { ApproveBillClaim, BillClaim } from '../../models';
@Injectable({
  providedIn: 'root'
})
export class BenefitBillClaimService extends BaseService {
  private billClaimBaseUrl = `${this.Base_API_URL}BenefitBillClaim`;
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.billClaimBaseUrl = `${this.appSettingsService.apiBaseUrl}BenefitBillClaim`;
  }
  getAllBenefitBillClaimByEmployeesByManagerId(managerId: any, startDate: string, endDate: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.billClaimBaseUrl}/pending-bill-request/${managerId}/startDate/${startDate}/endDate/${endDate}`);
  }
  getAllBenefitBillClaimByEmployeesByEmployeeId(employeeId: any, startDate: string, endDate: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.billClaimBaseUrl}/GetByEmployeeAndDateRange/employeeId/${employeeId}/startDate/${startDate}/endDate/${endDate}`);
  }
  getAllBenefitBillClaim(billClaim: BillClaim): Observable<BillClaim[]> {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post<BillClaim[]>(`${this.billClaimBaseUrl}/pending-bill-request`, billClaim, requestHeader);
  }
  approveBillClaim(approveBillClaim: ApproveBillClaim) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.billClaimBaseUrl}/approve`, approveBillClaim, requestHeader);
  }
  rejectBillClaim(approveBillClaim: ApproveBillClaim) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(`${this.billClaimBaseUrl}/decline-bill-claim`, approveBillClaim, requestHeader);
  }
  createBillClaim(formData: FormData) {
    var requestHeader = { headers: new HttpHeaders({ 'No-Auth': 'False' }) };
    return this.http.post(`${this.billClaimBaseUrl}`, formData, requestHeader);
  }
  updateBillClaim(formData: FormData) {
    var requestHeader = { headers: new HttpHeaders({ 'No-Auth': 'False' }) };
    return this.http.put(`${this.billClaimBaseUrl}`, formData, requestHeader);
  }
}
