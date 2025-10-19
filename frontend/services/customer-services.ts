import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {Customer, CustomerDTO, Equipment} from './model-services';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private baseUrl = '/api/Customer';

  constructor(private http: HttpClient) {}

  getAllCustomers(): Observable<CustomerDTO[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<CustomerDTO[]>(this.baseUrl, { headers });
  }

  getCustomerById(id: number): Observable<CustomerDTO> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<CustomerDTO>(`${this.baseUrl}/${id}`, { headers });
  }

  // get all customer roles
  getCustomerRoles(): Observable<CustomerDTO[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<CustomerDTO[]>(`${this.baseUrl}/roles`, { headers });
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
