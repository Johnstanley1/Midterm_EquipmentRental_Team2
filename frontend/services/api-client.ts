import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GoogleLogin} from './model-services';

@Injectable({
  providedIn: 'root'
})
export class ApiClient {
  private baseUrl = 'https://localhost:7220/api/auth';

  constructor(private http: HttpClient) {}

  login(googleToken: string): Observable<GoogleLogin> {
    return this.http.post<GoogleLogin>(`${this.baseUrl}/login`, { token: googleToken });
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
