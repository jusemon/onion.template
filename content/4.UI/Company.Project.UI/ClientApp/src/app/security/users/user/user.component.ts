import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { UserService } from '../services/user.service';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material';
import { Base64 } from 'src/app/shared/utils/base64';
import { finalize, take } from 'rxjs/operators';
import { LoadingService } from 'src/app/shared/services/loading/loading.service';
import { Users } from '../users.models';
import { FormConfig, FormResponse } from 'src/app/shared/components/form/form.models';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit, OnDestroy {
  editMode: boolean;
  defaultPass = '**********';
  // userForm = this.fb.group({
  //   username: ['', Validators.compose([
  //     Validators.required, Validators.minLength(3), Validators.maxLength(15)])],
  //   email: ['', Validators.compose([
  //     Validators.required, Validators.minLength(3), Validators.maxLength(50)])],
  //   roleId: [null, Validators.compose([
  //     Validators.required])],
  //   password: ['', Validators.compose([
  //     Validators.required, Validators.minLength(8), Validators.maxLength(50)])]
  // });

  config: FormConfig = {
    appearance: 'standard',
    colsPerRow: 2,
    fields: [{
      name: 'username',
      label: 'Username',
      type: 'text',
      validators: {
        required: {
          validator: Validators.required,
          message: 'Username is <strong>required</strong>'
        }
      }
    }, {
      name: 'email',
      label: 'Email',
      type: 'text',
      validators: {
        required: {
          validator: Validators.required,
          message: 'Email <strong>required</strong>'
        },
        email: {
          validator: Validators.email,
          message: 'Email has a <strong>incorrect format</strong>'
        }
      }
    }, {
      name: 'roleId',
      label: 'Role',
      type: 'text',
      validators: {
        required: {
          validator: Validators.required,
          message: 'Role is <strong>required</strong>'
        }
      }
    }, {
      name: 'password',
      label: 'Password',
      type: 'password',
      validators: {
        required: {
          validator: Validators.required,
          message: 'Password is <strong>required</strong>'
        }
      }
    }]
  };

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar,
    private loading: LoadingService,
    private userService: UserService
  ) { }

  ngOnInit() {
    this.route.params.pipe(take(1)).subscribe((data) => {
      this.editMode = typeof (data.id) !== 'undefined';
      if (this.editMode) {
        this.get(data.id);
      }
    });
  }

  get(id: number) {
    this.userService.get(id).pipe(take(1)).subscribe((user) => {
      user.password = this.defaultPass;
      // this.userForm.setValue({
      //   username: user.username,
      //   email: user.email,
      //   roleId: user.roleId,
      //   password: user.password
      // });
    });
  }

  // onSubmit() {
  //   if (this.userForm.valid) {
  //     this.loading.show();
  //     const finalizeFunction = finalize(() => this.loading.hide());
  //     const user = this.userForm.value as Users;
  //     user.password = Base64.encode(user.password);
  //     if (this.editMode) {
  //       if (user.password === Base64.encode(this.defaultPass)) {
  //         user.password = '';
  //       }
  //       this.userService.update(user).pipe(take(1), finalizeFunction).subscribe(() => {
  //         this.snackBar.open(`User "${user.username}" has been updated.`, 'Dismiss', { duration: 3000 });
  //         this.goBack();
  //       });
  //     } else {
  //       this.userService.create(user).pipe(take(1), finalizeFunction).subscribe(() => {
  //         this.snackBar.open(`User "${user.username}" has been created.`, 'Dismiss', { duration: 3000 });
  //         this.goBack();
  //       });
  //     }
  //   }
  // }

  onSubmit(user: Users) {
    console.log(user)
    this.loading.show();
    const finalizeFunction = finalize(() => this.loading.hide());
    user.password = Base64.encode(user.password);
    if (this.editMode) {
      if (user.password === Base64.encode(this.defaultPass)) {
        user.password = '';
      }
      this.userService.update(user).pipe(take(1), finalizeFunction).subscribe(() => {
        this.snackBar.open(`User "${user.username}" has been updated.`, 'Dismiss', { duration: 3000 });
        this.goBack();
      });
    } else {
      this.userService.create(user).pipe(take(1), finalizeFunction).subscribe(() => {
        this.snackBar.open(`User "${user.username}" has been created.`, 'Dismiss', { duration: 3000 });
        this.goBack();
      });
    }
  }

  goBack() {
    this.router.navigate(['security/users']);
  }

  ngOnDestroy(): void { }
}
