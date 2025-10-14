import { Component } from '@angular/core';
import {Router, RouterLink, RouterLinkActive} from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  imports: [
    RouterLink,
    RouterLinkActive
  ],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css'
})
export class NavBar {
    constructor(private router: Router) {}

    // logout
    logout() {
      localStorage.removeItem('token') // clear token
      this.router.navigate(['/login']) // redirect to login page
    }
}
