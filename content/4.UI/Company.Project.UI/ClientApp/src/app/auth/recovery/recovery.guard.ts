import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';

@Injectable()
export class RecoveryGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean | UrlTree {
      if ((next.queryParams['token'] || false) && (next.queryParams['id'] || false)) {
        return true;
      }
      return this.router.createUrlTree(['/auth']);
  }
}
