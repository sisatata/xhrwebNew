import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AppSettingsService {

  private appSettings: any;

  constructor(private http: HttpClient) { }

  loadAppSettings() {
    return this.http.get('/assets/appsettings.json')
      .toPromise()
      .then(data => {
        this.appSettings = data;
      });
  }

  get apiBaseUrl() {
    if (!this.appSettings) {
      throw Error('Config file not loaded!');
    }

    return this.appSettings.apiUrl;
  }

  get siteKey() {
    if (!this.appSettings) {
      throw Error('Config file not loaded!');
    }

    return this.appSettings.siteKey;
  }

  get appConfig() {
    if (!this.appSettings) {
      return null;
    }
    return this.appSettings;
  }
}
