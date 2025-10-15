import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer, CustomerDto } from './model.services';

@Injectable({ providedIn: 'root' })
export class CustomerService {
  private baseUrl = '/api/Customer';

  constructor(private http: HttpClient) {}

  getAll(): Observable<CustomerDto[]> {
    return this.http.get<CustomerDto[]>(this.baseUrl);
  }

  getById(id: number): Observable<CustomerDto> {
    return this.http.get<CustomerDto>(`${this.baseUrl}/${id}`);
  }

  create(customer: Customer): Observable<Customer> {
    return this.http.post<Customer>(this.baseUrl, customer);
  }

  update(id: number, customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(`${this.baseUrl}/${id}`, customer);
  }

  delete(id: number): Observable<Customer> {
    return this.http.delete<Customer>(`${this.baseUrl}/${id}`);
  }
}
