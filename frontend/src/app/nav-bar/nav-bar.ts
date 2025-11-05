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
  email: string | null = null;
  private routerSub?: Subscription;

  constructor(private router: Router, @Inject(PLATFORM_ID) private platformId: Object) {

  }

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      const params = new URLSearchParams(window.location.search);
      const email = params.get('email');
      const role = params.get('role');

      console.log(email, role);

      if (email) {
        // Save user info locally
        localStorage.setItem('email', email);
        localStorage.setItem('role', role || 'User');

        // Navigate to home or dashboard based on role
        if (role === 'Admin') {
          this.router.navigate(['/home']);
        } else if (role === "User") {
          this.router.navigate(['/home']);
        } else {
          this.router.navigate(['/login']);
        }
      }
    }

    this.refreshUserDisplay();
    // Refresh username after any navigation (e.g., after login redirect)
    this.routerSub = this.router.events
      .pipe(filter((e): e is NavigationEnd => e instanceof NavigationEnd))
      .subscribe(() => this.refreshUserDisplay());
  }

  ngOnDestroy(): void {
    this.routerSub?.unsubscribe();
  }

  private refreshUserDisplay() {
    if (isPlatformBrowser(this.platformId)) {
      this.email = localStorage.getItem('email');
    } else {
      this.email = null;
    }
  }

  // logout
  logout() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      localStorage.removeItem('email');
    }
    this.email = null;
    this.router.navigate(['/login']); // redirect to login page
  }
}
