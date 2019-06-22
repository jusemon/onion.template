import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RecoveryComponent } from './recovery/recovery.component';
import { SharedModule } from '../shared/shared.module';
import { Route, RouterModule } from '@angular/router';
import { RecoveryGuard } from './recovery/recovery.guard';
import { ReactiveFormsModule } from '@angular/forms';

const routes: Route[] = [
  { path: '', component: LoginComponent },
  { path: 'recoveryPassword', component: RecoveryComponent, canActivate: [RecoveryGuard] }
];

@NgModule({
  declarations: [LoginComponent, RecoveryComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    ReactiveFormsModule,
    SharedModule
  ],
  providers: [RecoveryGuard]
})
export class AuthModule { }
