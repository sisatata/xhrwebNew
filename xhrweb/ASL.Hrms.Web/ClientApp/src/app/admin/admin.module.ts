import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { AuthGuard } from '../auth/services/auth-guard';
import { AuthModule } from '../auth/auth.module';
import { RoleManagementComponent } from '../../app/admin/role-management/role-management.component';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';


const routes: Routes = [
  { path: 'assign-role', component:RoleManagementComponent , canActivate: [AuthGuard], data: { roles: ['Administrators'] } },
 
];

@NgModule({
  declarations: [RoleManagementComponent],
  imports: [
    SharedModule,
    AuthModule,
    MainLayoutModule,
    AngularMultiSelectModule,
    RouterModule.forChild(routes)
  ]
})
export class AdminModule { }
