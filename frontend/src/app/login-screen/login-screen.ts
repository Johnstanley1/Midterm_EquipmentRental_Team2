import {AfterViewInit, Component, Inject, inject, NgZone, OnInit, PLATFORM_ID} from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {ApiClient} from '../../../services/api-client';
import {isPlatformBrowser} from '@angular/common';

/*
 * login logic for the login screen
 * this stores the token generated in the local storage then navigates to the home page after successful login
 */
@Component({
  selector: 'app-login-screen',
  standalone: true,
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './login-screen.html',
  styleUrl: './login-screen.css',
})
export class LoginScreen {
  min_Length = 4;
  max_length = 20;

  constructor(private router: Router,
              @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  // Inject builder
  builder = inject(FormBuilder);

  // Validation
  loginForm = this.builder.group({
    _username: [
      '',
      [
        Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length),
      ],
    ],

    _password: [
      '',
      [
        Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length),
      ],
    ],
  });

  loginWithGoogle(): void {
    if (isPlatformBrowser(this.platformId)) {
      window.location.href = 'http://localhost:5027/api/auth/login';
    }
  }

  private handleRedirect() {
    // After backend redirects back to Angular with ?email=&role= query params

  }

  // private handleCredentialResponse(response: any) {
  //   const googleToken = response.credential; // JWT from Google
  //
  //   this.api.login(googleToken).subscribe({
  //     next: (res) => {
  //       this.api.setToken(res.token); // store your app JWT
  //       this.router.navigate(['/home']);
  //         if (res.role) localStorage.setItem('role', res.role);
  //         if (res.email) localStorage.setItem('username', res.email);
  //
  //         if (res.role === 'Admin') {
  //           this.router.navigate(['/home']);
  //         } else if (res.role === "User") {
  //           this.router.navigate(['/home']);
  //         } else {
  //           this.router.navigate(['/login']);
  //         }
  //       },
  //       error: (err) => {
  //         console.log('Invalid credentials', err);
  //       },
  //   });
  // }


  // // Get data:
  // refName = this.loginForm.controls['_username'];
  // refPassword = this.loginForm.controls['_password'];

  // on login
  // login() {
  //   if (this.loginForm.invalid) {
  //     console.log(this.refName, this.refPassword);
  //     this.loginForm.markAllAsTouched();
  //   }
  //
  //   // Get data:
  //   const username = this.loginForm.value._username;
  //   const password = this.loginForm.value._password;
  //
  //   // Create credentials object
  //   const credentials = { username, password };
  //
  //   // create credentials object and pass data
  //   this.http.post<any>('/api/Auth/login', credentials).subscribe({
  //     next: (res) => {
  //       localStorage.setItem('token', res.token); // save token to local storage
  //       if (res.role) localStorage.setItem('role', res.role);
  //       if (res.username) localStorage.setItem('username', res.username);
  //
  //       if (res.role === 'Admin') {
  //         this.router.navigate(['/home']);
  //       } else if (res.role === "User") {
  //         this.router.navigate(['/home']);
  //       } else {
  //         this.router.navigate(['/login']);
  //       }
  //     },
  //     error: (err) => {
  //       console.log('Invalid credentials');
  //     },
  //   });
  // }


}
