import { Injectable, Injector } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Division } from '../../shared/models';
import { BaseService } from 'src/app/shared/services/base.service';


@Injectable({
  providedIn: 'root'
})

export class DivisionService extends BaseService {

  private divisionBaseUrl = this.Base_API_URL + 'Division';

  constructor(private http: HttpClient, private injector: Injector
  ) { super(); }

  getAllDivisions(): Observable<Division[]> {
    return this.http.get<any[]>(this.divisionBaseUrl);
  }
}
