import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MaterialModule } from './material.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ConfirmationDialogComponent } from './components/confirmation-dialog/confirmation-dialog.component';
import { ConfirmationWithTextComponent } from './components/confirmation-dialog-with-text/confirmation-dialog-with-text.component';
import { LogoutConfirmationComponent } from './components/logout-confirmation/logout-confirmation.component';
import { CommonModule } from '@angular/common';
import { TranslateModule } from '@ngx-translate/core';
import { BlockUIModule } from 'ng-block-ui';
import { AlertService } from './services/alert.service';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { BaseComponent } from "./components/base/base.component";
import { BaseAuthorizedComponent } from "./components/base-authorized/base-authorized.component";
import { FlexLayoutModule } from '@angular/flex-layout';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { SearchFilterComponent } from './components/search-filter/search-filter.component';
import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';
//import { BranchComponent } from './components/branch/branch.component';


export const dialogComponents = [
  ConfirmationDialogComponent,
  ConfirmationWithTextComponent,
  LogoutConfirmationComponent
];

export const otherComponents = [
  HeaderComponent, FooterComponent, SidebarComponent, BaseComponent, BaseAuthorizedComponent
];


@NgModule({
  declarations: [...dialogComponents, ...otherComponents, SearchFilterComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [AlertService,SearchFilterComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    RouterModule,
    TranslateModule,
    BlockUIModule.forRoot(),
    FlexLayoutModule,
    NgxPaginationModule,
    AngularMultiSelectModule

  ],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    TranslateModule,
    BlockUIModule,
    NgxPaginationModule,
    SearchFilterComponent,
    ...dialogComponents, ...otherComponents
  ],
  entryComponents: [
    ...dialogComponents
  ]
})
export class SharedModule { }

