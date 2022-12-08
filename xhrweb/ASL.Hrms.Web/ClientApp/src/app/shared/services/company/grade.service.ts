import { Injectable } from '@angular/core';
import { BaseService } from '../base.service';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Grade } from '../../models';
import { AppSettingsService } from '../../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class GradeService extends BaseService {
  private gradeBaseUrl = this.Base_API_URL + 'Grade';
  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
    super();
    this.gradeBaseUrl = this.appSettingsService.apiBaseUrl + 'Grade';
  }

  getGradeListByCompany(companyId: any): Observable<Grade[]> {
    return this.http.get<Grade[]>(this.gradeBaseUrl + '/company/' + companyId);
  }

  getGradeById(id: any): Observable<Grade> {
    return this.http.get<Grade>(this.gradeBaseUrl + id);
  }

  createGrade(grade: Grade) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.post(this.gradeBaseUrl, grade, requestHeader);
  }

  updateGrade(grade: Grade) {
    var requestHeader = { headers: new HttpHeaders({ 'Content-Type': 'application/json', 'No-Auth': 'False' }) };
    return this.http.put(this.gradeBaseUrl, grade, requestHeader);
  }

  deleteGrade(grade: Grade) {
    let httpParams = new HttpParams().set('gradeId', grade.id);
    var input = { id: grade.id };
    var requestHeader = {
      headers: new HttpHeaders({
        'Content-Type': 'apllication/json', 'No-Auth': 'False'
      }),
      body: input
    };
    return this.http.delete(this.gradeBaseUrl, requestHeader);
  }

}
