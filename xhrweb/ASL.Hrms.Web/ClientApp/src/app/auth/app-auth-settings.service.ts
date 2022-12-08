import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppAuthSettingsService {

  private appSettings: any;

  constructor(private http: HttpClient) { }

  loadAppSettings() {
    try {
      return this.http.get('/assets/appsettings.json')
        .subscribe((result: any) => {
          let test = result;
          this.appSettings = result; 
        });
        //.toPromise()
        //.then(data => {
        //  debugger;
        //  this.appSettings = data;
        //  return Promise.resolve(data);
        //});
    }
    catch (e) {
      console.log(e);
    }
    
  }

  get apiAuthBaseUrl() {
    if (!this.appSettings) {
      
      throw Error('Config file not loaded!');
    }

    return this.appSettings.apiUrl;
  }

  get appConfig() {
    if (!this.appSettings) {
      return null;
    }
    return this.appSettings;
  }
}
