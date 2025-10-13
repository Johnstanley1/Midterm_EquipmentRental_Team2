import { Component } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {NgOptimizedImage} from '@angular/common';

@Component({
  selector: 'app-login-screen',
  standalone: true,
  imports: [
    FormsModule,
    NgOptimizedImage,
  ],
  templateUrl: './login-screen.html',
  styleUrl: './login-screen.css'
})
export class LoginScreen {
  username: string = "";
  password: string = "";

  constructor(private http: HttpClient, private router: Router) {}

  /*
  * login logic for the login screen
  * this stores the token generated in the local storage then navigates to the home page after successful login
  */
  login(){
    const credentials = {
      username: this.username,
      password: this.password
    }

    this.http.post<any>('https://localhost:7024/api/Auth/login', credentials)
      .subscribe({
        next: (res) => {
          console.log('Login successful', res)

          localStorage.setItem('token', res.token) // save token to local storage

          if(res.role === 'Admin'){
            this.router.navigate(['/home'])
          }else if(['User1', 'User2', 'User3', 'User4', 'User5'].includes(res.role)){
            this.router.navigate(['/home'])
          }else{
            this.router.navigate(['/login'])
          }
        },
        error: (err) => {
          console.log('Login failed', err)
          alert('Invalid credentials')
        }
      })
  }
}
