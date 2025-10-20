import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable, of } from 'rxjs';
import { RentalService } from '../../../services/rental-services';
import {RentalDTO} from '../../../services/model-services';

@Component({
  selector: 'app-rental-detail-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './rental-detail-screen.html',
  styleUrls: ['./rental-detail-screen.css'],
})
export class RentalDetailScreen {
  rental$!: Observable<RentalDTO | null>;
  private rentalId: number = 0;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private rentals: RentalService,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.rentalId = id;
    this.rental$ = isBrowser && !isNaN(id) ? this.rentals.getRentalsById(id) : of(null);
  }

  // Navigate back to rentals list
  backToList() {
    this.router.navigate(['/all-rentals']);
  }

  // Navigate to new edit screen
  extend(_r: RentalDTO) {
    this.router.navigate(['/rental-edit', this.rentalId]);
  }

  // Cancel rental (admin)
  cancel(force = false) {
    if (!confirm(force ? 'Force cancel this rental?' : 'Cancel this rental?')) return;
    this.rentals.deleteRental(this.rentalId).subscribe({
      next: () => this.backToList(),
      error: (err) => {
        const status = err?.status ?? 0;
        if (!force && (status === 401 || status === 403 || status === 400)) {
          if (confirm('Unauthorized or blocked to cancel. Force cancel as admin?')) {
            this.cancel(true);
            return;
          }
        }
        alert('Failed to cancel: ' + (err?.error || err?.message || err));
      },
    });
  }

  // Mark as returned
  markReturned() {
    const notes = prompt('Return notes (optional):', 'Returned in good condition') || '';
    const condition = prompt('Return condition:', 'Good') || 'Good';
    this.rentals.returnRental().subscribe({
      next: () => this.backToList(),
      error: (err) => {
        const status = err?.status ?? 0;
        if (status === 401 || status === 403) {
          if (confirm('Unauthorized to return. Force return as admin?')) {
            this.rentals.returnRental().subscribe({
              next: () => this.backToList(),
              error: (e2) => alert('Failed to force return: ' + (e2?.error || e2?.message || e2)),
            });
            return;
          }
        }
        alert('Failed to return: ' + (err?.error || err?.message || err));
      },
    });
  }
}
