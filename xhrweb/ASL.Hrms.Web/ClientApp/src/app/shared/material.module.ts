import { NgModule } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatRadioModule } from '@angular/material/radio';
import { MatChipsModule } from '@angular/material/chips';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatCardModule, MatCard } from '@angular/material/card';

import { MatTabsModule } from '@angular/material/tabs';

import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatNativeDateModule, MAT_DATE_LOCALE } from '@angular/material/core';
import { MatSnackBarModule } from '@angular/material/snack-bar';


@NgModule({
  imports: [MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTooltipModule,
    MatRadioModule,
    MatChipsModule,
    MatProgressBarModule,
    MatCardModule,
    MatSnackBarModule
  ],
  exports: [MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatDialogModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatInputModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTooltipModule,
    MatRadioModule,
    MatChipsModule,
    MatProgressBarModule,
    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatCardModule,
    MatSnackBarModule
  ],
  providers: [
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }
  ],
})
export class MaterialModule { }
