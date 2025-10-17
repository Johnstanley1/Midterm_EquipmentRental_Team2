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
  // flattened names from DTO
  customerName?: string;
  equipmentName?: string;
  equipmentStatus?: string;
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

  // Force return (admin only)
  forceReturn(rentalId: number, notes: string = '', condition: string = 'Good'): Observable<any> {
    const payload = { id: rentalId, returnNotes: notes, returnCondition: condition } as any;
    return this.http.post(`${this.baseUrl}/return?force=true`, payload);
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
    issuedAt?: string | null;
  }): Observable<any> {
    const payload: any = {
      equipmentId: data.equipmentId,
      customerId: data.customerId,
      dueDate: data.dueDate ?? null,
      issuedAt: data.issuedAt ?? undefined,
    };
    return this.http.post(`${this.baseUrl}/issue`, payload);
  }

  // Extend rental due date by sending a new due date and reason
  extendRental(id: number, newDueDate: string, reason: string): Observable<any> {
    const payload: any = { dueDate: newDueDate, reason };
    return this.http.put(`${this.baseUrl}/${id}/extend`, payload);
  }

  // Cancel a rental (Admin only). Optionally force cancel
  cancelRental(id: number, force: boolean = false): Observable<any> {
    const q = force ? '?force=true' : '';
    return this.http.delete(`${this.baseUrl}/${id}${q}`);
  }

  // Get overdue rentals (admin sees all, user sees own)
  getOverdue(): Observable<Rental[]> {
    return this.http.get<Rental[]>(`${this.baseUrl}/overdue`);
  }
}
