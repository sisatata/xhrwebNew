import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaskComponent } from './task.component';
import { SharedModule } from '../shared/shared.module';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';
import { AuthGuard } from '../auth/services/auth-guard';
import { AuthModule } from '../auth/auth.module';
import { TaskCategoryComponent } from './task-category/task-category.component';
import { CreateTaskCategoryComponent } from './task-category/create-task-category/create-task-category.component';
import { RecTestComponent } from './rec-test/rec-test.component';
import { TaskDetailComponent } from './task-detail/task-detail.component';
import { CreateTaskDetailComponent } from './task-detail/create-task-detail/create-task-detail.component';
import { TaskDetailBaseComponent } from './task-detail-base/task-detail-base.component';
import { CreateTaskDetailChildComponent } from './task-detail/create-task-detail-child/create-task-detail-child.component';
import { CreateTaskDetailLogComponent } from './task-detail/create-task-detail-log/create-task-detail-log.component';


const routes: Routes = [
  { path: '', component: TaskComponent, canActivate: [AuthGuard], data: { roles: ['Administrators'] } },
  { path: 'task-category', component: TaskCategoryComponent, canActivate: [AuthGuard], data: { roles: ['Administrators','EmployeeRole'] } },
  { path: 'task-detail', component: TaskDetailBaseComponent, canActivate: [AuthGuard], data: { roles: ['Administrators','EmployeeRole'] } },
  { path: 'task-detail-base', component: TaskDetailBaseComponent, canActivate: [AuthGuard], data: { roles: ['Administrators','EmployeeRole'] } },
  

];

@NgModule({
  declarations: [TaskComponent, TaskCategoryComponent, CreateTaskCategoryComponent, RecTestComponent, TaskDetailComponent, CreateTaskDetailComponent, TaskDetailBaseComponent, CreateTaskDetailChildComponent, CreateTaskDetailLogComponent],
  imports: [
    SharedModule,
    AuthModule,
    MainLayoutModule,
    RouterModule.forChild(routes)
  ]
})
export class TaskModule { }
