import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { EmployeeAddress, EmployeeImportFile } from '../../models';
import { Observable } from 'rxjs';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})

export class FileService extends BaseService {
  private fileService = `${this.Base_API_URL}RawFileDetail` ;

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.fileService = `${this.appSettingsService.apiBaseUrl}RawFileDetail`;
  }

  importEmployeeExcel(formData:any){
    var requestHeader = { headers: new HttpHeaders({ 'No-Auth': 'False' }) };
    return this.http.post(this.fileService+"/employee-import-excel", formData, requestHeader);
  }
  getRawFileDataByCompanyId(companyId : any):Observable<EmployeeImportFile[]>{
    return this.http.get<EmployeeImportFile[]>(`${this.fileService}/companyId/${companyId}`);
  }
  
  downloadEmployeeExcelTemplate(input: any){
    const httpOption: Object = {
      observe: 'response',
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'blob'
    };
    return this.http.post<any>(`${this.fileService}/download-employee-template`,input, httpOption);
  }

  downloadEmployeeImportFileHistory(input: any){
    const httpOption: Object = {
      observe: 'response',
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      }),
      responseType: 'blob'
    };
   
    return this.http.post<any>(`${this.fileService}/download-employee-file-status`,input, httpOption);
  }
 

}

