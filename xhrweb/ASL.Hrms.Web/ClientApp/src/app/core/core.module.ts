import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CoreComponent } from './core.component';
import { DivisionsComponent } from './divisions/divisions.component';
import { CreateDivisionComponent } from './divisions/create-division/create-division.component';
import { MainLayoutModule } from '../layouts/main-layout/main-layout.module';



const routes: Routes = [
  { path: '', component: CoreComponent },
  { path: 'divisions', component: DivisionsComponent },
];

@NgModule({
  declarations: [CoreComponent, DivisionsComponent, CreateDivisionComponent],
  imports: [
    MainLayoutModule,
    RouterModule.forChild(routes)
  ]
})
export class CoreModule { }
