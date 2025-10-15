import {Component, inject} from '@angular/core';
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';


/*
* login logic for the login screen
* this stores the token generated in the local storage then navigates to the home page after successful login
*/
@Component({
  selector: 'app-login-screen',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
  ],
  templateUrl: './login-screen.html',
  styleUrl: './login-screen.css'
})
export class LoginScreen {
  min_Length = 4;
  max_length = 20;

  constructor(private http: HttpClient, private router: Router) {}


  // Inject builder
  builder = inject(FormBuilder)

  // Validation
  loginForm = this.builder.group({
    _username: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]
    ],

    _password: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]

    ]
  })

  // Get data:
  refName = this.loginForm.controls['_username']
  refPassword = this.loginForm.controls['_password']

  // on login
  login(){

    if(this.loginForm.invalid){
      console.log(this.refName, this.refPassword)
      this.loginForm.markAllAsTouched();
    }

    // Get data:
    const username = this.loginForm.value._username;
    const password = this.loginForm.value._password;

    // Create credentials object
    const credentials = { username, password };

    // create credentials object and pass data
    this.http.post<any>('https://localhost:7024/api/Auth/login', credentials)
      .subscribe({
        next: (res) => {

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
          console.log('Invalid credentials')
        }
      })
  }
}
