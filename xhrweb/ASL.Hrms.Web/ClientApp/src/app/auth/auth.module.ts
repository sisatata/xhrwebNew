import { APP_INITIALIZER,NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent} from './login/login.component';
import { ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import { ResetPasswordComponent} from './reset-password/reset-password.component';
import { RegisterComponent} from './register/register.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { FullWidthLayoutModule } from '../layouts/full-width-layout/full-width-layout.module';
import { CompanyRegisterComponent } from './company-register/company-register.component';
//import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';
import { RecaptchaModule } from 'ng-recaptcha';
import { AppAuthSettingsService } from './app-auth-settings.service';
import { ChangePasswordComponent } from './change-password/change-password.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'company-register', component: CompanyRegisterComponent }
];

@NgModule({
  declarations: [LoginComponent, RegisterComponent, ForgotPasswordComponent, ResetPasswordComponent, CompanyRegisterComponent, ChangePasswordComponent],
  imports: [
    SharedModule,
    FullWidthLayoutModule,
    RecaptchaModule,
    RouterModule.forChild(routes)
  ],
  entryComponents:[
    ChangePasswordComponent
  ]
  //,
  // providers:[
  //   AppAuthSettingsService,
  //   {
  //     provide: APP_INITIALIZER,
  //     useFactory: (appAuthSettingsService: AppAuthSettingsService) => {
  //       return () => {
  //         return appAuthSettingsService.loadAppSettings();
  //       }; 
  //     },
  //     deps: [AppAuthSettingsService],
  //     multi: true
  //   }
  // ]
})
export class AuthModule { }
