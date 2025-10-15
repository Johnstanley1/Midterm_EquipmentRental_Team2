import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Rental {
  id: number;
  customerId: number;
  equipmentId: number;
  issuedAt: string;
  dueDate?: string | null;
  returnedAt?: string | null;
  status: string;
  returnNotes?: string | null;
  returnCondition?: string | null;
  // optional navigation props when included by API
  customer?: { id: number; name: string; username: string };
  equipment?: { id: number; name: string };
}

@Injectable({ providedIn: 'root' })
export class RentalService {
  private baseUrl = '/api/rentals';

  constructor(private http: HttpClient) {}

  // Return a rental by id
  returnRental(rentalId: number, notes: string = '', condition: string = 'Good'): Observable<any> {
    const payload = { id: rentalId, returnNotes: notes, returnCondition: condition } as any;
    return this.http.post(`${this.baseUrl}/return`, payload);
  }

  // Get all rentals (Admin: all, User: own)
  getAll(): Observable<Rental[]> {
    return this.http.get<Rental[]>(this.baseUrl);
  }

  // Get one rental by id
  getById(id: number): Observable<Rental> {
    return this.http.get<Rental>(`${this.baseUrl}/${id}`);
  }

  // Issue a rental
  issueRental(data: {
    equipmentId: number;
    customerId?: number;
    dueDate?: string | null;
  }): Observable<any> {
    const payload: any = {
      equipmentId: data.equipmentId,
      customerId: data.customerId,
      dueDate: data.dueDate ?? null,
    };
    return this.http.post(`${this.baseUrl}/issue`, payload);
  }
}
