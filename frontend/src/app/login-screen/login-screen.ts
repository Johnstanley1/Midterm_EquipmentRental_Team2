import {AfterViewInit, Component, inject} from '@angular/core';
import { FormBuilder, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import {ApiClient} from '../../../services/api-client';

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
export class LoginScreen implements AfterViewInit {
  min_Length = 4;
  max_length = 20;
  private google: any;
  private zone: any;

  constructor(private http: HttpClient, private router: Router, private api: ApiClient) {}

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


  ngAfterViewInit(): void {
    this.google.accounts.id.initialize({
      client_id: "397528110694-gvm5c9acstpr4jh30hmn1p4of4r18ah1.apps.googleusercontent.com",
      callback: (response: any) => this.zone.run(() => this.handleCredentialResponse(response))
    });

    this.google.accounts.id.renderButton(document.getElementById('google')!,{ theme: 'outline', size: 'large' });
  }

  private handleCredentialResponse(response: any) {
    const googleToken = response.credential; // JWT from Google

    this.api.login(googleToken).subscribe({
      next: (res) => {
        this.api.setToken(res.token); // store your app JWT
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Login failed', err);
      }
    });
  }

  login(): void {
    this.google.accounts.id.prompt()
  }

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
