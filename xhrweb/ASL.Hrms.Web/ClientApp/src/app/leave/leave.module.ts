import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LeaveComponent } from './leave.component';
import { SharedModule } from '../shared/shared.module';
import { LeaveTypeComponent } from './leave-type/leave-type.component';
import { CreateLeaveTypeComponent } from './leave-type/create-leave-type/create-leave-type.component';
import { LeaveApplicationComponent } from './leave-application/leave-application.component';
import { CreateLeaveApplicationComponent } from './leave-application/create-leave-application/create-leave-application.component';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { LeaveProcessComponent } from './leave-process/leave-process.component';
import { AuthGuard } from '../auth/services/auth-guard';
import { AuthModule } from '../auth/auth.module';
import { LeaveBalanceComponent } from './leave-balance/leave-balance.component';
import { LeaveGroupComponent } from './leave-group/leave-group.component';
import { CreateLeaveGroupComponent } from './leave-group/create-leave-group/create-leave-group.component';
import { LeaveRequestComponent } from './leave-request/leave-request.component';
import { ApproveOrRejectLeaveRequestComponent } from './leave-request/approve-or-reject-leave-request/approve-or-reject-leave-request.component';


const routes: Routes = [
  { path: '', component: LeaveComponent },
  { path: 'leaveType', component: LeaveTypeComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] }},
  { path: 'leave-process', component: LeaveProcessComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'leaveApplication', component: LeaveApplicationComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] }},
  { path: 'leave-balance', component: LeaveBalanceComponent , canActivate: [AuthGuard], data: { roles: ['Administrators', 'EmployeeRole', 'SystemAdmin'] }},
  { path: 'leave-group', component: LeaveGroupComponent , canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] }},
  { path: 'leave-request', component: LeaveRequestComponent , canActivate: [AuthGuard], data: { roles: ['SystemAdmin','ReportingManager'] }},
  
];

@NgModule({
  declarations: [LeaveComponent, LeaveTypeComponent, CreateLeaveTypeComponent, LeaveApplicationComponent, CreateLeaveApplicationComponent, LeaveProcessComponent, LeaveBalanceComponent, LeaveGroupComponent, CreateLeaveGroupComponent, LeaveRequestComponent, ApproveOrRejectLeaveRequestComponent],
  imports: [
    SharedModule,
    AuthModule,
    MainLayoutModule,
    RouterModule.forChild(routes)
  ],
  entryComponents:[CreateLeaveTypeComponent,CreateLeaveApplicationComponent, CreateLeaveGroupComponent]
})
export class LeaveModule { }
