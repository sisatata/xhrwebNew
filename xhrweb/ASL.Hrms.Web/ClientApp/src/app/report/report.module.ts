import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportsComponent } from './reports/reports.component';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../auth/services/auth-guard';
import { SharedModule } from '../shared/shared.module';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { AttendanceReportComponent } from './attendance-report/attendance-report.component';
import { SalaryReportComponent } from './salary-report/salary-report.component';
import { LeaveReportComponent } from './leave-report/leave-report.component';
import { PaySlipReportComponent } from './pay-slip-report/pay-slip-report.component';
import { BillReportComponent } from './bill-report/bill-report.component';
import { BonusReportComponent } from './bonus-report/bonus-report.component';
import { EmployeeEnrollReportComponent } from './employee-enroll-report/employee-enroll-report.component';
import { ConfirmationReportComponent } from './confirmation-report/confirmation-report.component';
import { LeaveDetailsReportComponent } from './leave-details-report/leave-details-report.component';
import { OtSummaryReportComponent } from './ot-summary-report/ot-summary-report.component'; 
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';
import { EarnLeaveEncashmentReportComponent } from './earn-leave-encashment-report/earn-leave-encashment-report.component';
import { TaskReportComponent } from './task-report/task-report.component';
import { ProvidentFundReportComponent } from './provident-fund-report/provident-fund-report.component';

const routes: Routes = [
  { path: 'attendance-report', component: AttendanceReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin','EmployeeRole'] }},
  { path: 'attendance', component: ReportsComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'salary', component: SalaryReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole' ,'PayrollManager'] } },
  { path: 'leave', component: LeaveReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'payslip', component: PaySlipReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole' ,'PayrollManager'] } },
  { path: 'bill', component: BillReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'bonus', component: BonusReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin', 'EmployeeRole'] } },
  { path: 'employee-enroll', component: EmployeeEnrollReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'confirmation', component: ConfirmationReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
   { path: 'leave-details', component: LeaveDetailsReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin','EmployeeRole'] } },
   { path: 'ot-summary-report', component: OtSummaryReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin','PayrollManager'] } },
   { path: 'earn-leave-encashment-report', component: EarnLeaveEncashmentReportComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin','PayrollManager'] } },
   { path: 'task-report', component: TaskReportComponent, canActivate: [AuthGuard], data: { roles: ['EmployeeRole'] } },
   { path: 'provident-fund-report', component: ProvidentFundReportComponent, canActivate: [AuthGuard], data: { roles: ['EmployeeRole','Administrators'] } },
]
 

@NgModule({
  declarations: [ReportsComponent, AttendanceReportComponent, SalaryReportComponent, LeaveReportComponent, PaySlipReportComponent, BillReportComponent, BonusReportComponent, EmployeeEnrollReportComponent, ConfirmationReportComponent, LeaveDetailsReportComponent, OtSummaryReportComponent, EarnLeaveEncashmentReportComponent, TaskReportComponent, ProvidentFundReportComponent],
  imports: [
    CommonModule,
    SharedModule,
    MainLayoutModule,
    RouterModule.forChild(routes),
    AngularMultiSelectModule
  
  ]
})
export class ReportModule { }
