import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiClient {
  private baseUrl = '/api/auth';

  constructor(private http: HttpClient) {}

  login(email: string): Observable<LoginResponse> {
    return this.http.get<LoginResponse>(`${this.baseUrl}/login?email=${email}`);
  }

  setToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }
}
