import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ForgotPasswordModel } from '../models/forgot-password';
import { ResetPasswordModel } from '../models/reset-password';
import { environment } from '../../../environments/environment';
import { AppAuthSettingsService } from '../app-auth-settings.service';
import { AppSettingsService } from '../../app-settings.service';
import { ChangePassword } from 'src/app/shared/models';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private accountBaseUri = environment.baseApiUrl + "api/account";

  constructor(private http: HttpClient, private appSettingsService: AppSettingsService) {
  }

  forgotPassword(model: ForgotPasswordModel) {
    this.accountBaseUri = this.appSettingsService.apiBaseUrl + "api/account";
    var requestHeader = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "No-Auth": "True",
      }),
    };
    return this.http.post(
      this.accountBaseUri + "/forgot-password",
      model,
      requestHeader
    );
  }

  resetPassword(model: ResetPasswordModel) {
    this.accountBaseUri = this.appSettingsService.apiBaseUrl + "api/account";
    var requestHeader = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "No-Auth": "True",
      }),
    };
    return this.http.post(
      this.accountBaseUri + "/ResetPassword",
      model,
      requestHeader
    );
  }
  changePassword(model: ChangePassword){
    this.accountBaseUri = this.appSettingsService.apiBaseUrl + "api/account";
    var requestHeader = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "No-Auth": "True",
      }),
    };
    return this.http.post(
      this.accountBaseUri + "/change-password",
      model,
      requestHeader
    );
  }
}
