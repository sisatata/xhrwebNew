import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CompanyComponent } from './company.component';
import { CreateCompanyComponent } from './create-company/create-company.component';
import { SharedModule } from '../shared/shared.module';
import { BranchComponent } from './branch/branch.component';
import { CreateBranchComponent } from './branch/create-branch/create-branch.component';
import { DepartmentComponent } from './department/department.component';
import { CreateDepartmentComponent } from './department/create-department/create-department.component';
import { DesignationComponent } from './designation/designation.component';
import { CreateDesignationComponent } from './designation/create-designation/create-designation.component';
import { FinancialYearComponent } from './financial-year/financial-year.component';
import { CreateFinancialyearComponent } from './financial-year/create-financialyear/create-financialyear.component';
import { CommonLookUpTypeComponent } from './common-look-up-type/common-look-up-type.component';
import { CreateCommonLookUpTypeComponent } from './common-look-up-type/create-common-look-up-type/create-common-look-up-type.component';
import { MonthCycleComponent } from './month-cycle/month-cycle.component';
import { CreateMonthCycleComponent } from './month-cycle/create-month-cycle/create-month-cycle.component';
import { OfficeNoticeComponent } from './office-notice/office-notice.component';
import { CreateOfficeNoticeComponent } from './office-notice/create-office-notice/create-office-notice.component';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { AuthGuard } from '../auth/services/auth-guard';
import { AuthModule } from '../auth/auth.module';
import { ConfigurationComponent } from './configuration/configuration.component';
import { CreateDefaultConfigurationComponent } from './configuration/create-default-configuration/create-default-configuration.component';
import { CreateCustomConfigurationComponent } from './configuration/create-custom-configuration/create-custom-configuration.component';
import { CompanyListComponent } from './company-list/company-list.component';
import { CompanyDetailBaseComponent } from './company-detail-base/company-detail-base.component';
import { CompanyDetailComponent } from './company-detail/company-detail.component';
import { CompanyOtherInfoBaseComponent } from './company-other-info-base/company-other-info-base.component';
import { CompanyPhoneComponent } from './company-phone/company-phone.component';
import { CreateCompanyPhoneComponent } from './company-phone/create-company-phone/create-company-phone.component';
import { CompanyEmailComponent } from './company-email/company-email.component';
import { CreateCompanyEmailComponent } from './company-email/create-company-email/create-company-email.component';
import { CompanyAddressComponent } from './company-address/company-address.component';
import { CreateCompanyAddressComponent } from './company-address/create-company-address/create-company-address.component';
import { CreateCustomEmployeeConfigurationComponent } from './configuration/create-custom-employee-configuration/create-custom-employee-configuration.component';
import {MatExpansionModule} from '@angular/material/expansion';

import { RecaptchaModule } from 'ng-recaptcha';
const routes: Routes = [
  { path: 'create-company', component: CreateCompanyComponent },
  { path: ':id/:name/:sname/:slogan/:website/:facebook/:logo/:active', component: CompanyDetailBaseComponent },
  { path: 'list', component: CompanyComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'branch', component: BranchComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'department', component: DepartmentComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'designation', component: DesignationComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'common-lookup', component: CommonLookUpTypeComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'financialYear', component: FinancialYearComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'monthCycle', component: MonthCycleComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'configuration', component: ConfigurationComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'request-list', component: CompanyListComponent, canActivate: [AuthGuard], data: { roles: ['Administrators','SystemAdmin'] } },
];

@NgModule({
  declarations: [CompanyComponent, CreateCompanyComponent, BranchComponent, CreateBranchComponent,
    DepartmentComponent, CreateDepartmentComponent,
    DesignationComponent, CreateDesignationComponent,
    FinancialYearComponent, CreateFinancialyearComponent,
    CommonLookUpTypeComponent, CreateCommonLookUpTypeComponent,
    MonthCycleComponent, CreateMonthCycleComponent, OfficeNoticeComponent,
    CreateOfficeNoticeComponent, ConfigurationComponent, CreateDefaultConfigurationComponent,
    CreateCustomConfigurationComponent,
    CompanyListComponent,
    CompanyDetailBaseComponent,
    CompanyDetailComponent,
    CompanyOtherInfoBaseComponent,
    CompanyPhoneComponent,
    CreateCompanyPhoneComponent,
    CompanyEmailComponent,
    CreateCompanyEmailComponent,
    CompanyAddressComponent,
    CreateCompanyAddressComponent,
    CreateCustomEmployeeConfigurationComponent],
  imports: [
    SharedModule,
    AuthModule,
    MainLayoutModule,
    RecaptchaModule,
    MatExpansionModule,
    RouterModule.forChild(routes)
  ],
  entryComponents: [CreateCompanyComponent, CreateBranchComponent,
    CreateDepartmentComponent, CreateDesignationComponent,
    CreateFinancialyearComponent, CreateMonthCycleComponent,
    CreateCommonLookUpTypeComponent, CreateOfficeNoticeComponent,
    CreateCompanyEmailComponent, CreateCompanyPhoneComponent,
    CreateDefaultConfigurationComponent, CreateCustomConfigurationComponent,
    CreateCustomEmployeeConfigurationComponent]
})
export class CompanyModule { }

