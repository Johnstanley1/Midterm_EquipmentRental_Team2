import {ChangeDetectorRef, Component, Inject, OnDestroy, OnInit, PLATFORM_ID} from '@angular/core';
import { Router, RouterLink, RouterLinkActive, NavigationEnd } from '@angular/router';
import { isPlatformBrowser, NgIf } from '@angular/common';
import { Subscription, filter } from 'rxjs';
import {HttpClient} from '@angular/common/http';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink, RouterLinkActive, NgIf],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar implements OnInit, OnDestroy {
  email: string | null = null;
  private routerSub?: Subscription;

  constructor(private router: Router,
              private http: HttpClient,
              private cdr: ChangeDetectorRef,
              @Inject(PLATFORM_ID) private platformId: Object) {

  }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId)) {
      this.http.get<{email: string, role: string}>('http://localhost:5027/api/auth/profile', {
        withCredentials: true
      }).subscribe({
        next: data => {
          localStorage.setItem('role', data.role);
          localStorage.setItem('email', data.email);
          this.email = data.email;
          this.cdr.detectChanges(); // 👈 Force view update
        },
        error: err => {
          this.email = null;
          this.cdr.detectChanges(); // 👈 Force view update
        }
      })

      // Refresh username after any navigation (e.g., after login redirect)
      this.routerSub = this.router.events
        .pipe(filter((e): e is NavigationEnd => e instanceof NavigationEnd))
        .subscribe(() => this.refreshUserDisplay());
    }
  }

  private refreshUserDisplay() {
    if (isPlatformBrowser(this.platformId)) {
      this.email = localStorage.getItem('email');
    } else {
      this.email = null;
    }
  }

  ngOnDestroy(): void {
    this.routerSub?.unsubscribe();
  }

  // logout
  logout() {
    if (isPlatformBrowser(this.platformId)) {
      // localStorage.removeItem('token');
      localStorage.removeItem('role');
      localStorage.removeItem('email');
    }
    this.email = null;
    window.location.href = 'http://localhost:5027/api/auth/logout';
    // this.router.navigate(['/login']); // redirect to login page
  }
}
