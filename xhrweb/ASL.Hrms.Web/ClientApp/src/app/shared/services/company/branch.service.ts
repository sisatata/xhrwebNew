import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Branch } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class BranchService extends BaseService {

  private branchBaseUrl = this.Base_API_URL + 'Branch';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.branchBaseUrl = this.appSettingsService.apiBaseUrl + 'Branch';
  }

  getAllBranchByCompanyId(companyId): Observable<Branch[]> {
    return this.http.get<Branch[]>(this.branchBaseUrl + '/company/' + companyId);
  }

  getBranchById(branchId: number): Observable<any[]> {
    return this.http.get<any[]>(this.branchBaseUrl + branchId);
  }

  createBranch(branch: Branch) {

    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.branchBaseUrl, branch, requestHeader);
  }

  editBranch(branch: Branch) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.branchBaseUrl, branch, requestHeader);
  }

  deleteBranchById(branchId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.branchBaseUrl + '/' + branchId, requestHeader);
  }

  deleteBranch(branch: any) {
    let httpParams = new HttpParams().set('id', branch.id);
    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      body: branch
    };
    return this.http.delete(this.branchBaseUrl, requestHeader);
  }


}
