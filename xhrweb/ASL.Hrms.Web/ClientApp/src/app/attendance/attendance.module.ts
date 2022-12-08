import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AttendanceComponent } from './attendance.component';
import { ShiftGroupComponent } from './shift-group/shift-group.component';
import { CreateShiftGroupComponent } from './shift-group/create-shift-group/create-shift-group.component';
import { SharedModule } from '../shared/shared.module';
import { ShiftComponent } from './shift/shift.component';
import { CreateShiftComponent } from './shift/create-shift/create-shift.component';
import { ShiftAllocationComponent } from './shift-allocation/shift-allocation.component';
import { CreateShiftAllocationComponent } from './shift-allocation/create-shift-allocation/create-shift-allocation.component';
import { AttendanceProcessComponent } from './attendance-process/attendance-process.component';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { AuthGuard } from '../auth/services/auth-guard';
import { AuthModule } from '../auth/auth.module';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { HolidayComponent } from './holiday/holiday.component';
import { CreateHolidayComponent } from './holiday/create-holiday/create-holiday.component';
import { EmployeeAttendanceComponent } from './employee-attendance/employee-attendance.component';
import { CreateEmployeeAttendanceComponent } from './employee-attendance/create-employee-attendance/create-employee-attendance.component';
import { AttendanceRequestComponent } from './attendance-request/attendance-request.component';
import { CreateAttendanceRequestComponent } from './attendance-request/create-attendance-request/create-attendance-request.component';
import { ApproveOrRejectAttendanceRequestComponent } from './attendance-request/approve-or-reject-attendance-request/approve-or-reject-attendance-request.component';
import {MatListModule} from '@angular/material/list';
import {LeafletModule} from '@asymmetrik/ngx-leaflet';
import { EmployeeAttendanceDetailsComponent } from './employee-attendance/employee-attendance-details/employee-attendance-details.component';
const routes: Routes = [
  { path: '', component: AttendanceComponent },
  { path: 'shiftGroup', component: ShiftGroupComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: "shift", component: ShiftComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] }},
  { path: 'shiftAllocation', component: ShiftAllocationComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] }},
  { path: 'attendanceProcess', component: AttendanceProcessComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] }},
  { path: 'holiday', component: HolidayComponent, canActivate: [AuthGuard], data: { roles: ['Administrators', 'SystemAdmin'] } },
  { path: 'employee-attendance', component: EmployeeAttendanceComponent, canActivate: [AuthGuard], data: { roles: ['EmployeeRole', 'SystemAdmin','Administrators'] } },
  { path: 'attendance-request', component: AttendanceRequestComponent, canActivate: [AuthGuard], data: { roles: ['EmployeeRole', 'ReportingManager'] } },

];

@NgModule({
  declarations: [AttendanceComponent, ShiftGroupComponent, CreateShiftGroupComponent, ShiftComponent, CreateShiftComponent, ShiftAllocationComponent, CreateShiftAllocationComponent,
    AttendanceProcessComponent, HolidayComponent, CreateHolidayComponent, EmployeeAttendanceComponent, CreateEmployeeAttendanceComponent, AttendanceRequestComponent, CreateAttendanceRequestComponent, ApproveOrRejectAttendanceRequestComponent, EmployeeAttendanceDetailsComponent],
  imports: [
    SharedModule,
    AuthModule,
    MainLayoutModule,
    NgxMaterialTimepickerModule,
    LeafletModule,
    MatListModule,
    RouterModule.forChild(routes)
  ],
  entryComponents: [
    CreateShiftGroupComponent, CreateShiftComponent, CreateShiftAllocationComponent, CreateEmployeeAttendanceComponent,
    ApproveOrRejectAttendanceRequestComponent,
    CreateAttendanceRequestComponent,ApproveOrRejectAttendanceRequestComponent,
    EmployeeAttendanceDetailsComponent
  ]
})
export class AttendanceModule { }
