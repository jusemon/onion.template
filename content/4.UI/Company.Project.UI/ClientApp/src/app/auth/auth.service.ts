import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MatSnackBar } from '@angular/material/snack-bar';
import { environment } from 'src/environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserLoginToken, UserLogin } from './login/login.models';
import { Users } from '../security/users/users.models';
import { tap } from 'rxjs/operators';
import { Response } from '../shared/generics/models';
import { handleResponse } from '../shared/utils/rx-pipes';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  api: string;
  key: string;
  isAuthenticated = new BehaviorSubject(false);

  constructor(private http: HttpClient, private snackBar: MatSnackBar) {
    this.api = environment.api;
    this.key = environment.key;
    this.isAuthenticated.next(localStorage.getItem(this.key) != null);
  }

  /**
   * Get the options of the request
   *
   * @returns Return the options of the request
   */
  protected getOptions() {
    return {
      headers: {
        Authorization: `Bearer ${this.getToken()}`
      }
    };
  }

  public clearToken() {
    this.isAuthenticated.next(false);
    localStorage.clear();
  }

  /**
   * Get the security token of the current user
   *
   * @returns A token or a empty string if no token available
   */
  public getToken(): string {
    if (this.isAuthenticated.value) {
      const user: UserLoginToken = JSON.parse(localStorage.getItem(this.key)!);
      return user.token!;
    } else {
      return '';
    }
  }

  /**
   * Check the recovery token
   *
   * @param user the user with id and recovery token
   * @returns A observable with the info of the user
   */
  public checkRecoveryToken(user: UserLoginToken): Observable<Users> {
    return this.http.post<Response<Users>>(`${this.api}/auth/checkRecoveryToken`, user).pipe(
      handleResponse(this.snackBar)
    );
  }

  /**
   * Authenticate the user
   *
   * @param entity the user to authenticate
   * @returns An observable with the security token
   */
  public authenticate(entity: UserLogin): Observable<UserLoginToken> {
    return this.http.post<Response<UserLoginToken>>(`${this.api}/auth/login`, entity).pipe(
      handleResponse(this.snackBar),
      tap((result) => {
        localStorage.setItem(this.key, JSON.stringify(result));
        this.isAuthenticated.next(true);
      }));
  }

  /**
   * Send a recovery email with a recovery link
   *
   * @param email The email of the user who needs to recover the pass
   * @returns true if the email has been sent
   */
  public sendRecovery(email: string): Observable<boolean> {
    return this.http.get<Response<boolean>>(`${this.api}/auth/sendRecovery`, { params: { email } }).pipe(
      handleResponse(this.snackBar)
    );
  }

  /**
   * Update the user password
   *
   * @param user The user with the new pass
   * @returns An observable with the updated user
   */
  public updatePassword(user: Users): Observable<Users> {
    return this.http.post<Response<Users>>(`${this.api}/auth/updatePassword`, user).pipe(
      handleResponse(this.snackBar)
    );
  }
}
