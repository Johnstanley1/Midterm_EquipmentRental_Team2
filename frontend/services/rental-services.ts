import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import { Observable } from 'rxjs';
import {Rental, RentalDTO} from './model-services';

@Injectable({ providedIn: 'root' })

export class RentalService {
  private baseUrl = '/api/rentals';

  constructor(private http: HttpClient) {}


  // Get all rentals (Admin: all, User: own)
  getAllRentals(): Observable<RentalDTO[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<RentalDTO[]>(this.baseUrl, { headers });
  }


  // Get one rental by id
  getRentalsById(id: number): Observable<RentalDTO> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<RentalDTO>(`${this.baseUrl}/${id}`, { headers });
  }

  // Get one rental by id
  GetRentalEntityById(id: number): Observable<Rental> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<Rental>(`${this.baseUrl}/${id}`, { headers });
  }


  // Get overdue rentals (admin sees all, user sees own)
  getOverdueRental(): Observable<RentalDTO[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<RentalDTO[]>(`${this.baseUrl}/overdue`, { headers });
  }


  // Get equipment rental history (admin sees all, user sees own)
  getEquipmentRentalHistory(id: number): Observable<RentalDTO[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<RentalDTO[]>(`${this.baseUrl}/equipment/${id}`, { headers });
  }


  // Get active rentals (admin sees all, user sees own)
  getActiveRental(): Observable<RentalDTO[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<RentalDTO[]>(`${this.baseUrl}/active`, { headers });
  }


  // Get active rentals (admin sees all, user sees own)
  getCompletedRental(): Observable<RentalDTO[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<RentalDTO[]>(`${this.baseUrl}/completed`, { headers });
  }


  // Issue a rental
  createRental(rentalDTO: RentalDTO) {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.post(`${this.baseUrl}/issue`, rentalDTO, { headers });
  }


  // Extend rental due date by sending a new due date and reason
  updateRental(id: number, rental: Rental){
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.put(`${this.baseUrl}/${id}/extend`, rental, { headers });
  }


  // Cancel a rental (Admin only). Optionally force cancel
  deleteRental(id: number): Observable<any> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.delete(`${this.baseUrl}/${id}`, { headers });
  }


  // Return a rental by id
  returnRental(rentalDTO: RentalDTO): Observable<any> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.post(`${this.baseUrl}/return`, rentalDTO, { headers });
  }
}
