import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/shared/utils/base-service';
import { Users } from '../users.models';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from 'src/app/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService<Users> {

  constructor(http: HttpClient, snackBar: MatSnackBar, auth: AuthService) {
    super(http, 'user', snackBar, auth);
  }
}
