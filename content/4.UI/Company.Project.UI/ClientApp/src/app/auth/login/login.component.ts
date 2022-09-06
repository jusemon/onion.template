import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserLogin } from './login.models';
import { AuthService } from '../auth.service';
import { take, finalize } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Base64 } from 'src/app/shared/utils/base64';
import { LoadingService } from 'src/app/shared/services/loading/loading.service';
import { InputDialogData, InputDialogResponse } from 'src/app/shared/dialogs/input-dialog/input-dialog.models';
import { InputDialogComponent } from 'src/app/shared/dialogs/input-dialog/input-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  encapsulation: ViewEncapsulation.None,
})
export class LoginComponent implements OnInit {

  authForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required]
  });

  recoveryPasswordData: InputDialogData = {
    content: 'Please enter your email',
    fields: [
      {
        label: 'Email',
        name: 'email',
        validators: {
          required: {
            message: 'Email is <strong>required</strong>',
            validator: Validators.required
          },
          email: {
            message: 'Must be a <strong>valid</strong> email',
            validator: Validators.email
          }
        },
        icon: 'mail_outline'
      }
    ]
  };

  constructor(
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router,
    private loading: LoadingService,
    private dialog: MatDialog,
    private auth: AuthService) { }

  ngOnInit() {
  }

  openRecoveryPassword() {
    this.dialog.open(InputDialogComponent, { width: '350px', data: this.recoveryPasswordData })
      .beforeClosed().pipe(take(1)).subscribe((result: InputDialogResponse) => {
        if (result) {
          this.loading.show();
          this.auth.sendRecovery(result['email']).pipe(take(1), finalize(() => this.loading.hide())).subscribe(() => {
            this.snackBar.open(`Please check your mailbox`, 'Dismiss', { duration: 3000 });
          });
        }
      });
  }

  onSubmit() {
    if (this.authForm.valid) {
      const user = this.authForm.value as UserLogin;
      user.password = Base64.encode(user.password!);
      this.loading.show();
      this.auth.authenticate(user).pipe(take(1), finalize(() => this.loading.hide())).subscribe(() => {
        this.snackBar.open(`User "${user.username}" has logged on.`, 'Dismiss', { duration: 3000 });
        this.router.navigate(['home']);
      });
    }
  }
}
