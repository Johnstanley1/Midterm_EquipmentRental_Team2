import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer, CustomerDto } from './model-services';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private baseUrl = '/api/Customer';

  constructor(private http: HttpClient) {}

  getAllCustomers(): Observable<CustomerDto[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<CustomerDto[]>(this.baseUrl, { headers });
  }

  getCustomerById(id: number): Observable<CustomerDto> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<CustomerDto>(`${this.baseUrl}/${id}`, { headers });
  }

  createCustomer(customer: Customer): Observable<Customer> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.post<Customer>(this.baseUrl, customer, { headers });
  }

  updateCustomer(id: number, customer: Customer): Observable<Customer> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.put<Customer>(`${this.baseUrl}/${id}`, customer, { headers });
  }

  deleteCustomer(id: number): Observable<Customer> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.delete<Customer>(`${this.baseUrl}/${id}`, { headers });
  }
}
