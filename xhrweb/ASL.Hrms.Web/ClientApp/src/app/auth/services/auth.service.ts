import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../models/User'
import { environment } from '../../../environments/environment';
import { LoginModel } from '../models/Login';
import { JwtHelperService } from '@auth0/angular-jwt';
import { AppAuthSettingsService } from '../app-auth-settings.service';
import { AppSettingsService } from '../../app-settings.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  private authenticationBaseUri = environment.baseApiUrl + "api/auth";

  jwtHelperService: JwtHelperService= new JwtHelperService();

  constructor(private http: HttpClient, private router: Router, private appSettingsService: AppSettingsService) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
    

  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(loginModel: LoginModel) {
    this.authenticationBaseUri = this.appSettingsService.apiBaseUrl + "api/auth";
    var requestHeader = {
      headers: new HttpHeaders({
        "Content-Type": "application/json",
        "No-Auth": "True",
      }),
    };
    return this.http.post(
      this.authenticationBaseUri,
      loginModel,
      requestHeader
    );
  }

  loginCallback() {
    let user = JSON.parse(localStorage.getItem('currentUser'));
    this.currentUserSubject.next(user);
  }

  get isAdmin() {
    return this.currentUserValue && this.currentUserValue.userRoles.includes('Administrators');
  }
  get isEmployee() {
    return this.currentUserValue && this.currentUserValue.userRoles.includes('EmployeeRole');
  }

  get isSystemAdmin() {
    return this.currentUserValue && this.currentUserValue.userRoles.includes('SystemAdmin');
  }
  get isPayrollManger() {
    return this.currentUserValue && this.currentUserValue.userRoles.includes('PayrollManager');
  }
  get isHRManager(){
    return this.currentUserValue && this.currentUserValue.userRoles.includes('HRManager');
  }
get isSuperAdmin(){
  return this.currentUserValue && this.currentUserValue.userRoles.includes('SuperAdmin');
}

get isReportingManager(){
  return this.currentUserValue && this.currentUserValue.userRoles.includes('ReportingManager');
}
  checkAuthenticated(): boolean {
    if (this.isTokenExpired())
      return false;

    else if (this.currentUserValue && this.currentUserValue.bearerToken) return true;

    else return false;
  }

  checkAuthenticationAndRedirectToLoginPage() {
    if (this.checkAuthenticated() == false) {
      this.router.navigateByUrl('/auth/login');
    }
  }

  logout() {

    localStorage.removeItem('userToken');
    localStorage.removeItem('currentUser');
    localStorage.clear();

    this.currentUserSubject.next(null);
    this.router.navigateByUrl('/auth/login');
  }

  getTokenExpirationDate(): Date {
    var myRawToken = this.getToken();
    

    //const decodedToken = this.jwtHelperService.decodeToken(myRawToken);

    const expirationDate = this.jwtHelperService.getTokenExpirationDate(myRawToken);

    return expirationDate;
  }

  isTokenExpired(token?: string): boolean {
    
    if (!token) token = this.getToken();
    if (!token) return true;

    var myRawToken = this.getToken();

    const isExpired = this.jwtHelperService.isTokenExpired(myRawToken);
    return isExpired;
  }


  getToken(): string {
    return localStorage.getItem('userToken');
  }


  getLoggedInUserInfo() {
    if (this.checkAuthenticated) {
      var userId = localStorage.getItem('userId');
      var loginId = localStorage.getItem('loginId');
      var displayName = localStorage.getItem('displayName');
      var email = localStorage.getItem('email');
      var phone = localStorage.getItem('phone');
      var companyId = localStorage.getItem('companyId');
      var companyName = localStorage.getItem('companyName');
      var employeeId = localStorage.getItem('employeeId');
      var profilePictureUri = localStorage.getItem('profilePictureUri');
      var userRoles = localStorage.getItem('userRoles');
      return {
        userId: userId, loginId: loginId, displayName: displayName, email: email, phone: phone,
        companyId: companyId, companyName: companyName, employeeId: employeeId,
        profilePictureUri: profilePictureUri, userRoles: userRoles
      };
    }
    else {
      return null;
    }
  }
 
}
