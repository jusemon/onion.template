import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { AuthService } from './auth.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private auth: AuthService) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | UrlTree {
    const inAuth = next?.routeConfig?.path === 'auth';
    if (this.auth.isAuthenticated.value) {
      return inAuth ? this.router.createUrlTree(['/home']) : true;
    }
    return inAuth ? true : this.router.createUrlTree(['/auth']);
  }
}
