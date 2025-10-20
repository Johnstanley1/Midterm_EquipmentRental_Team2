import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { RouterLink } from '@angular/router';
import {map, Observable, of} from 'rxjs';
import { RentalService } from '../../../services/rental-services';
import {RentalDTO} from '../../../services/model-services';
import {catchError} from 'rxjs/operators';

@Component({
  selector: 'app-rentals-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './rentals-screen.html',
  styleUrls: ['./rentals-screen.css'],
})
export class RentalsScreen {
  rentals$: Observable<RentalDTO[]> = of([]);
  errorMessage: string | null = null;

  constructor(private rentals: RentalService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.rentals$ = isBrowser
      ? this.rentals.getAllRentals().pipe(
        catchError((err) => {
          // Friendly message for permission issues or others
          if (err?.status === 403) {
            this.errorMessage = 'You do not have permission to view rentals.';
          } else if (err?.status === 401) {
            this.errorMessage = 'Your session has expired. Please log in again.';
          } else {
            this.errorMessage = 'Failed to load customers. Please try again.';
          }
          return of([] as RentalDTO[]);
        })
      )
      : of([] as RentalDTO[]);
  }

  onDelete(id: number) {
    if (!confirm('Delete this customer?')) return;

    this.rentals$ = this.rentals$.pipe(
      map((rentals) => rentals.filter((r) => r.id !== id))
    );

    this.rentals.deleteRental(id).subscribe({
      // Refresh the list after deletion
      next:(data) =>{
        this.rentals$ = this.rentals$.pipe(
          map((data) => data.filter((d) => d.id != id))
        )
      },
      error:(err) => {
        if (err.status === 404) {
          this.errorMessage = 'Rental not found.';
        } else if (err.status === 403) {
          this.errorMessage = 'You do not have permission to delete rental.';
        } else {
          this.errorMessage = 'Failed to delete rental. Please try again.';
        }
      }
    });
  }
}
