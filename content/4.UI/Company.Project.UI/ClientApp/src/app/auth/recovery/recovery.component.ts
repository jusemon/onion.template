import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Validators } from '@angular/forms';
import { Base64 } from 'src/app/shared/utils/base64';
import { MatSnackBar } from '@angular/material/snack-bar';
import { finalize, take } from 'rxjs/operators';
import { FormConfig, FormResponse } from 'src/app/shared/components/form/form.models';
import { CustomValidators } from 'src/app/shared/utils/custom-validators';
import { LoadingService } from 'src/app/shared/services/loading/loading.service';
import { AuthService } from '../auth.service';
import { Users } from 'src/app/security/users/users.models';

@Component({
  selector: 'app-recovery',
  templateUrl: './recovery.component.html',
  styleUrls: ['./recovery.component.scss']
})
export class RecoveryComponent implements OnInit, OnDestroy {
  user?: Users;
  title?: string;
  config: FormConfig = {
    fields: [{
      name: 'newPassword',
      label: 'New password',
      type: 'password',
      validators: {
        required: {
          validator: Validators.required,
          message: 'New password is <strong>required</strong>'
        }
      }
    }, {
      name: 'repeatPassword',
      label: 'Repeat password',
      type: 'password',
      validators: {
        required: {
          validator: Validators.required,
          message: 'Repeat password is <strong>required</strong>'
        }
      }
    }],
    validators: {
      mustBeEquals: {
        validator: CustomValidators.mustBeEquals('newPassword', 'repeatPassword'),
        message: 'The passwords must be equals'
      }
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private loading: LoadingService,
    private auth: AuthService,
    private snackBar: MatSnackBar) { }

  ngOnInit() {
    this.route.queryParams.pipe(take(1)).subscribe((data) => {
      const tokenUser = { token: data['token'] || null, id: data['id'] || null };
      this.auth.checkRecoveryToken(tokenUser).subscribe(user => {
        this.user = user;
        this.user.token = tokenUser.token;
        this.title = `Welcome back <strong>${this.user.username}<strong>!`;
      }, () => {
        this.router.navigate(['/auth']);
      });
    });
  }

  onSubmit(data: FormResponse) {
    if (data['newPassword'] && this.user) {
      this.loading.show();
      this.user.password = Base64.encode(data['newPassword']);
      this.auth.updatePassword(this.user).pipe(take(1), finalize(() => this.loading.hide())).subscribe(() => {
        this.router.navigate(['/auth']);
        this.snackBar.open('The password has been updated');
      });
    }
  }

  onCancel() {
    this.router.navigate(['/auth']);
  }

  ngOnDestroy(): void { }
}

