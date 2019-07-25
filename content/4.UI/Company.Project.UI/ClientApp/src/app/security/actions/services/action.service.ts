import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material';
import { Actions } from '../actions.models';
import { AuthService } from 'src/app/auth/auth.service';
import { BaseService } from 'src/app/shared/utils/base-service';

@Injectable({
  providedIn: 'root'
})
export class ActionService extends BaseService<Actions> {
  constructor(http: HttpClient, snackBar: MatSnackBar, auth: AuthService) {
    super(http, 'action', snackBar, auth);
  }
}
