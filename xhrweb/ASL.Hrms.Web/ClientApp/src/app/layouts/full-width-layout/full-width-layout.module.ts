import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FullWidthLayoutComponent } from './full-width-layout/full-width-layout.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';



@NgModule({
  declarations: [FullWidthLayoutComponent],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule
  ]
})
export class FullWidthLayoutModule { }
