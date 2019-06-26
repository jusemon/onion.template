import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  MatSnackBarModule,
  MatToolbarModule,
  MatButtonModule,
  MatSidenavModule,
  MatIconModule,
  MatListModule,
  MatCardModule,
  MatFormFieldModule,
  MatInputModule,
  MatProgressSpinnerModule,
  MatDialogModule,
  MatProgressBarModule,
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
} from '@angular/material';
import { LayoutModule } from '@angular/cdk/layout';
import { LoadingComponent } from './services/loading/loading.component';
import { ConfirmComponent } from './dialogs/confirm/confirm.component';
import { InputDialogComponent } from './dialogs/input-dialog/input-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { FormComponent } from './components/form/form.component';
import { TrimDirective } from './directives/trim.directive';

@NgModule({
  declarations: [
    LoadingComponent,
    ConfirmComponent,
    InputDialogComponent,
    FormComponent,
    TrimDirective
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatIconModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule
  ],
  exports: [
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatSnackBarModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    FormComponent,
    ReactiveFormsModule,
    TrimDirective
  ],
  entryComponents: [ConfirmComponent, InputDialogComponent]
})
export class SharedModule { }
