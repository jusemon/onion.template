import { Component, OnInit, OnDestroy } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Subscription, merge } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from '../auth/auth.service';
import { openClose } from '../shared/utils/animations';


@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
  animations: [
    openClose
  ]
})
export class NavigationComponent implements OnInit, OnDestroy {

  private sub: Subscription;
  isHandset: boolean;
  navegationVisible: boolean;

  constructor(
    private breakpointObserver: BreakpointObserver,
    private router: Router,
    private auth: AuthService
  ) { }

  ngOnInit() {
    this.sub = merge(
      this.auth.isAuthenticated.pipe(tap(v => {
        this.navegationVisible = v;
        if (!v) {
          this.auth.clearToken();
        }
      })),
      this.breakpointObserver.observe(Breakpoints.Handset).pipe(tap(v => this.isHandset = v.matches))).subscribe();
  }

  onClose() {
    this.auth.clearToken();
    this.router.navigate(['/auth']);
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }
}
