import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeAddress } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class DistrictsService extends BaseService {
  private employeeAddressBaseUrl = this.Base_API_URL ;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.employeeAddressBaseUrl = this.appSettingsService.apiBaseUrl;
  }

  getAllDistricts(){
    return this.http.get<any>(this.appSettingsService.apiBaseUrl + "District");
  }
  
  getAllThanas(districtId:any){
    let params = new HttpParams().set("id", districtId);
    return this.http.get<any>(this.appSettingsService.apiBaseUrl  + "Upazila/" + "districtId/" + "id/" , {params} );
  }
  
  
  


}

