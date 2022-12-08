import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home/home.component';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { HomeBaseComponent } from './home-base/home-base.component';
import { AttendanceChartComponent } from './attendance-chart/attendance-chart.component';
// HomeBaseComponent
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { LeaveChartComponent } from './leave-chart/leave-chart.component';
import { GenderChartComponent } from './gender-chart/gender-chart.component';
import { PayrollChartComponent } from './payroll-chart/payroll-chart.component';
import { YearlyPayrollChartComponent } from './yearly-payroll-chart/yearly-payroll-chart.component';
import { NoticeDetailsComponent } from './notice-details/notice-details.component';
import { DepartmentSalaryChartComponent } from './department-salary-chart/department-salary-chart.component';
import {MatExpansionModule} from '@angular/material/expansion';
import { NewEmployeeListComponent } from './new-employee-list/new-employee-list.component';
import { ActiveEmployeeListComponent } from './active-employee-list/active-employee-list.component';
import { EmployeeConfirmationListComponent } from './employee-confirmation-list/employee-confirmation-list.component';
import { NotificationDetailsComponent } from './notification-details/notification-details.component';
const routes: Routes = [
  { path: '', component: HomeBaseComponent },
];

@NgModule({
  declarations: [HomeComponent, HomeBaseComponent, AttendanceChartComponent, LeaveChartComponent, GenderChartComponent, PayrollChartComponent, YearlyPayrollChartComponent, NoticeDetailsComponent, DepartmentSalaryChartComponent, NewEmployeeListComponent, ActiveEmployeeListComponent, EmployeeConfirmationListComponent, NotificationDetailsComponent],
  imports: [
    SharedModule,
    MainLayoutModule,
    NgxChartsModule,
    MatExpansionModule,
    RouterModule.forChild(routes)
  ]
})
export class HomeModule { }
