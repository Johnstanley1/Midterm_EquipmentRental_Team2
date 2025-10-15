import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Rental {
  id: number;
  customerId: number;
  equipmentId: number;
  issuedAt: string;
  dueDate: string;
  returnedAt?: string | null;
  status: string;
  returnNotes?: string | null;
  returnCondition?: string | null;
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
}
