import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient,HttpHeaders,HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShiftAllocation } from '../../models/attendance/shift-allocation';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class ShiftAllocationService  extends BaseService{
  private shiftAllocationBaseUrl = this.Base_API_URL + 'ShiftAllocation';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.shiftAllocationBaseUrl = this.appSettingsService.apiBaseUrl + 'ShiftAllocation';
  }
  
  getShiftAllocationByCompanyAndBranch(companyId:any,branchId:any,dutyYear:any,dutyMonth:any,departmentId?:any,designationId?:any,employeeId?:any):Observable<ShiftAllocation[]>{
   return this.http.get<ShiftAllocation[]>(this.shiftAllocationBaseUrl+'/company/'+companyId+'/branch/'+branchId+'/financialYear/'+dutyYear+'/monthCycle/'+dutyMonth+'/department/'+departmentId+'/designation/'+designationId+'/employee/'+employeeId);
 }

  getShiftAllocationById(shiftAllocationId: number): Observable<any[]> {
    return this.http.get<any[]>(this.shiftAllocationBaseUrl  + shiftAllocationId);
  }
  createShiftAllocation(shiftAllocation:ShiftAllocation) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.shiftAllocationBaseUrl, shiftAllocation, requestHeader);
  }

  editShiftAllocation(shiftAllocation: ShiftAllocation) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.shiftAllocationBaseUrl, shiftAllocation, requestHeader);
  }

  deleteShiftAllocation(shiftAllocation: any) {
    let httpParams = new HttpParams().set('shiftAllocationId', shiftAllocation.id);
    var input = { id: shiftAllocation.id };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: input
    };
    return this.http.delete(this.shiftAllocationBaseUrl, requestHeader);
  }

}
