import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [MainLayoutComponent],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ]
})
export class MainLayoutModule { }
