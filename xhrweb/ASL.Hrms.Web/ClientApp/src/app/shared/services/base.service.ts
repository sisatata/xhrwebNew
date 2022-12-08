import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class BaseService {

  constructor() { }

  Base_API_URL = environment.baseApiUrl;

  //getGenders() {
  //  var genders = [{ text: 'Male', value: 1 }, { text: 'Female', value: 2 }, { text: 'Prefer not to say', value: 3 }];
  //  return genders;
  //}
}
