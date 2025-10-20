import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable, of } from 'rxjs';
import { RentalService } from '../../../services/rental-services';
import {CustomerDTO, RentalDTO} from '../../../services/model-services';
import {CustomerService} from '../../../services/customer-services';
import {catchError} from 'rxjs/operators';

@Component({
  selector: 'app-rental-detail-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './rental-detail-screen.html',
  styleUrls: ['./rental-detail-screen.css'],
})
export class RentalDetailScreen {
  rental$!: Observable<RentalDTO | null>;
  rentalId!: number;
  errorMessage: string | null = null;


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private rentals: RentalService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}


  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
      this.rental$ = this.rentals.getRentalsById(this.rentalId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load rental details';
          console.error(err);
          return of(null);
        })
      );
    }
  }




  // // Navigate back to rentals list
  // backToList() {
  //   this.router.navigate(['/all-rentals']);
  // }
  //
  // // Navigate to new edit screen
  // extend(_r: RentalDTO) {
  //   this.router.navigate(['/rental-edit', this.rentalId]);
  // }

  // // Cancel rental (admin)
  // cancel(force = false) {
  //   if (!confirm(force ? 'Force cancel this rental?' : 'Cancel this rental?')) return;
  //   this.rentals.deleteRental(this.rentalId).subscribe({
  //     next: () => this.backToList(),
  //     error: (err) => {
  //       const status = err?.status ?? 0;
  //       if (!force && (status === 401 || status === 403 || status === 400)) {
  //         if (confirm('Unauthorized or blocked to cancel. Force cancel as admin?')) {
  //           this.cancel(true);
  //           return;
  //         }
  //       }
  //       alert('Failed to cancel: ' + (err?.error || err?.message || err));
  //     },
  //   });
  // }
  //
  // // Mark as returned
  // markReturned() {
  //   const notes = prompt('Return notes (optional):', 'Returned in good condition') || '';
  //   const condition = prompt('Return condition:', 'Good') || 'Good';
  //   this.rentals.returnRental().subscribe({
  //     next: () => this.backToList(),
  //     error: (err) => {
  //       const status = err?.status ?? 0;
  //       if (status === 401 || status === 403) {
  //         if (confirm('Unauthorized to return. Force return as admin?')) {
  //           this.rentals.returnRental().subscribe({
  //             next: () => this.backToList(),
  //             error: (e2) => alert('Failed to force return: ' + (e2?.error || e2?.message || e2)),
  //           });
  //           return;
  //         }
  //       }
  //       alert('Failed to return: ' + (err?.error || err?.message || err));
  //     },
  //   });
  // }
}
