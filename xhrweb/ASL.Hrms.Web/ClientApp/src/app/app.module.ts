import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule, DatePipe } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AuthGuard } from './auth/services/auth-guard';
import { AuthInterceptor } from './auth/services/auth-interceptor';
import { MainLayoutComponent } from './layouts/main-layout/main-layout/main-layout.component';
import { FullWidthLayoutComponent } from './layouts/full-width-layout/full-width-layout/full-width-layout.component';
import { MainLayoutModule } from './layouts/main-layout/main-layout.module';
import { FullWidthLayoutModule } from './layouts/full-width-layout/full-width-layout.module';
import { NgIdleKeepaliveModule } from '@ng-idle/keepalive'; // this includes the core NgIdleModule but includes keepalive providers for easy wireup
import { MaterialModule } from './shared/material.module';
import { MomentModule } from 'angular2-moment'; // optional, provides moment-style pipes for date formatting
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AppSettingsService } from './app-settings.service';
import { AppAuthSettingsService } from './auth/app-auth-settings.service';
import {LeafletModule} from '@asymmetrik/ngx-leaflet';
import {MatListModule} from '@angular/material/list';


export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,

  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    BrowserAnimationsModule,
    MatProgressSpinnerModule,
    MainLayoutModule,
    FullWidthLayoutModule,
    MaterialModule,
    NgIdleKeepaliveModule.forRoot(),
    MomentModule,
    LeafletModule,
    MatListModule,
    TranslateModule.forRoot({
      defaultLanguage: 'en',

      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),  
    RouterModule.forRoot([
      { path: '', component: MainLayoutComponent, loadChildren: () => import('./home/home.module').then(m => m.HomeModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'core', component: MainLayoutComponent, loadChildren: () => import('./core/core.module').then(m => m.CoreModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'company', component: MainLayoutComponent, loadChildren: () => import('./company/company.module').then(m => m.CompanyModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'employee', component: MainLayoutComponent, loadChildren: () => import('./employee/employee.module').then(m => m.EmployeeModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'attendance', component: MainLayoutComponent, loadChildren: () => import('./attendance/attendance.module').then(m => m.AttendanceModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'leave', component: MainLayoutComponent, loadChildren: () => import('./leave/leave.module').then(m => m.LeaveModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'payroll', component: MainLayoutComponent, loadChildren: () => import('./salary/payroll.module').then(m => m.PayrollModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'task', component: MainLayoutComponent, loadChildren: () => import('./task/task.module').then(m => m.TaskModule), canActivate: [AuthGuard], canActivateChild: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] } },
      { path: 'auth', component: FullWidthLayoutComponent, loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) },
      { path: 'report', component: MainLayoutComponent, loadChildren: () => import('./report/report.module').then(m => m.ReportModule) },
      { path: 'admin', component: MainLayoutComponent, loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },

    ],
      { scrollPositionRestoration: 'enabled' }),
  ],
  providers: [
    AppSettingsService,
    {
      provide: APP_INITIALIZER,
      useFactory: (appSettingsService: AppSettingsService) => {
        return () => {
          return appSettingsService.loadAppSettings();
        };
      },
      deps: [AppSettingsService],
      multi: true
    }
    ,
    AppAuthSettingsService,
    {
      provide: APP_INITIALIZER,
      useFactory: (appAuthSettingsService: AppAuthSettingsService) => {
        return () => {

          var tt = appAuthSettingsService.loadAppSettings();
          return appAuthSettingsService.loadAppSettings();
        };
      },
      deps: [AppAuthSettingsService],
      multi: true
    }
    , AuthGuard, { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }, DatePipe],
  bootstrap: [AppComponent,],
  entryComponents: [

  ]
})
export class AppModule { }
