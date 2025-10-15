import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Equipment } from './model.services';

@Injectable({
  providedIn: 'root',
})
export class EquipmentService {
  constructor(private http: HttpClient) {}

  private baseUrl = '/api/Equipment';

  // get all equipments
  getAllEquipments(): Observable<Equipment[]> {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<Equipment[]>(this.baseUrl, { headers });
  }

  // get equipment by id
  getEquipmentById(id: number): Observable<Equipment> {
    return this.http.get<Equipment>(`${this.baseUrl}/${id}`);
  }

  // get all equipment status
  getEquipmentStatus() {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<Equipment[]>(`${this.baseUrl}/status`, { headers });
  }

  // get all equipment condition
  getEquipmentCondition() {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<Equipment[]>(`${this.baseUrl}/condition`, { headers });
  }

  // get all equipment category
  getEquipmentCategory() {
    let headers = new HttpHeaders();

    if (typeof window !== 'undefined') {
      const token = localStorage.getItem('token');
      if (token) {
        headers = headers.set('Authorization', `Bearer ${token}`);
      }
    }
    return this.http.get<Equipment[]>(`${this.baseUrl}/category`, { headers });
  }

  // create new equipment
  createEquipment(equipment: Equipment): Observable<Equipment> {
    return this.http.post<Equipment>(this.baseUrl, equipment);
  }

  // modify existing equipment
  updateEquipment(id: number, equipment: Equipment): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, equipment);
  }

  // delete existing equipment
  deleteEquipment(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
