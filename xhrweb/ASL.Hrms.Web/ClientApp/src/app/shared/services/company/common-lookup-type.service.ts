import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CommonLookUpType } from '../../models';
import { BaseService } from '../base.service';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class CommonLookUpTypeService extends BaseService {

  private commonLookUpTypeBaseUrl = this.Base_API_URL + 'CommonLookUpType';

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.commonLookUpTypeBaseUrl = this.appSettingsService.apiBaseUrl + 'CommonLookUpType';
  }

  getAllCommonLookUpTypeByCompanyId(companyId): Observable<CommonLookUpType[]> {
    return this.http.get<CommonLookUpType[]>(this.commonLookUpTypeBaseUrl + '/companyId/' + companyId);
  }
  //companyId/{companyId}/parentCode/{parentCode}
  getCommonLookUpTypeByParentCode(companyId: any, parentCode: String): Observable<any[]> {
    return this.http.get<any[]>(this.commonLookUpTypeBaseUrl + "/companyId/" + companyId + "/parentCode/" + parentCode);
  }

  getCommonLookUpTypeByCode(commonLookUpTypeCode: any): Observable<any[]> {
    return this.http.get<any[]>(this.commonLookUpTypeBaseUrl + '/parentCode/' + commonLookUpTypeCode);
  }

  createCommonLookUpType(commonLookUpType: CommonLookUpType) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.commonLookUpTypeBaseUrl, commonLookUpType, requestHeader);
  }

  editCommonLookUpType(commonLookUpType: CommonLookUpType) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.commonLookUpTypeBaseUrl, commonLookUpType, requestHeader);
  }

  deleteCommonLookUpTypeById(commonLookUpTypeId: number) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.delete(this.commonLookUpTypeBaseUrl + '/' + commonLookUpTypeId, requestHeader);
  }

  deleteCommonLookUpType(commonLookUpType: any) {

    let httpParams = new HttpParams().set('id', commonLookUpType.id);
    //httpParams.set('bbb', '222');

    let options = { params: httpParams };
    var requestHeader = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }),
      //params: httpParams,
      body: commonLookUpType
    };

    return this.http.delete(this.commonLookUpTypeBaseUrl, requestHeader);
  }


}
