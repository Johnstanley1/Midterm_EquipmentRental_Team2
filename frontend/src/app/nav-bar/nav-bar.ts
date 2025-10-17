import { Component, Inject, OnDestroy, OnInit, PLATFORM_ID } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, NavigationEnd } from '@angular/router';
import { isPlatformBrowser, NgIf } from '@angular/common';
import { Subscription, filter } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink, RouterLinkActive, NgIf],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar implements OnInit, OnDestroy {
  username: string | null = null;
  private routerSub?: Subscription;
  private isBrowser: boolean;

  constructor(private router: Router, @Inject(PLATFORM_ID) platformId: Object) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  ngOnInit(): void {
    this.refreshUsername();
    // Refresh username after any navigation (e.g., after login redirect)
    this.routerSub = this.router.events
      .pipe(filter((e): e is NavigationEnd => e instanceof NavigationEnd))
      .subscribe(() => this.refreshUsername());
  }

  ngOnDestroy(): void {
    this.routerSub?.unsubscribe();
  }

  private refreshUsername() {
    if (this.isBrowser) {
      this.username = localStorage.getItem('username');
    } else {
      this.username = null;
    }
  }

  // logout
  logout() {
    if (this.isBrowser) {
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      localStorage.removeItem('username');
    }
    this.username = null;
    this.router.navigate(['/login']); // redirect to login page
  }
}
