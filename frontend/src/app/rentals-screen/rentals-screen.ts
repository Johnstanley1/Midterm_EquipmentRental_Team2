import { Component, Inject, PLATFORM_ID } from '@angular/core';
import {CommonModule, AsyncPipe, isPlatformBrowser} from '@angular/common';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {map, Observable, of} from 'rxjs';
import { RentalService } from '../../../services/rental-services';
import {CustomerDTO, RentalDTO} from '../../../services/model-services';
import {catchError} from 'rxjs/operators';

@Component({
  selector: 'app-rentals-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './rentals-screen.html',
  styleUrls: ['./rentals-screen.css'],
})
export class RentalsScreen {
  rentals$!: Observable<RentalDTO []>;
  rentalId!: number;
  errorMessage: string | null = null;
  activeRentals$ =  of<RentalDTO [] | null>(null);
  completedRentals$ =  of<RentalDTO [] | null>(null);
  overdueRentals$ =  of<RentalDTO [] | null>(null);
  equipmentRentalHistory =  of<RentalDTO [] | null>(null);
  today = new Date;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private rentals: RentalService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    this.rentals$ = isBrowser
      ? this.rentals$ = this.rentals.getAllRentals().pipe(
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


  getDaysRented(issuedAt: Date | null): number {
    if (!issuedAt) return 0;
    const issued = new Date(issuedAt).getTime();
    const now = this.today.getTime();
    const diffMs = now - issued;
    return Math.floor(diffMs / (1000 * 60 * 60 * 24));
  }

  getDuration(issuedAt: Date | null): number {
    if (!issuedAt) return 0;
    const issued = new Date(issuedAt).getTime();
    const now = this.today.getTime();
    const diffMs = now - issued;
    return Math.floor(diffMs / (1000 * 60 * 60 * 24));
  }

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
      this.activeRentals$ = this.rentals.getActiveRental().pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load active rental';
          console.error(err);
          return of(null);
        })
      );

      this.completedRentals$ = this.rentals.getCompletedRental().pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load completed rentals';
          console.error(err);
          return of(null);
        })
      );

      this.overdueRentals$ = this.rentals.getOverdueRental().pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load overdue rentals';
          console.error(err);
          return of(null);
        })
      );

      this.equipmentRentalHistory = this.rentals.getEquipmentRentalHistory(this.rentalId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load equipment rental history';
          console.error(err);
          return of(null);
        })
      );
    }
  }


  // Cancel rental (admin)
  onDelete(id: number){
    if (confirm('Are you sure you want to delete this rental?')){

      const role = localStorage.getItem('role');
      if (role == "Admin") {
        this.rentals$ = this.rentals$.pipe(
          map((rentals) => rentals.filter((e) => e.id !== id))
        );

        this.rentals.deleteRental(id).subscribe({
          error: (err) => {
            console.error('Delete failed:', err);
          }
        })
      }else{
        alert("You don't have permission to delete the equipment");
      }
    }
  }
}
