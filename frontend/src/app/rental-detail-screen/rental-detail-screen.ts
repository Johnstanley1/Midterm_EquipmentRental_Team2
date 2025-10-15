import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Rental, RentalService } from '../../../services/rental.services';

@Component({
  selector: 'app-rental-detail-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './rental-detail-screen.html',
  styleUrls: ['./rental-detail-screen.css'],
})
export class RentalDetailScreen {
  rental$!: Observable<Rental | null>;
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
    this.rental$ = isBrowser && !isNaN(id) ? this.rentals.getById(id) : of(null);
  }

  // Navigate back to rentals list
  backToList() {
    this.router.navigate(['/all-rentals']);
  }

  // Open extend flow: simple prompt-based for now
  extend(r: Rental) {
    const current = r.dueDate ? new Date(r.dueDate) : new Date();
    const next = new Date(current.getTime());
    next.setDate(current.getDate() + 7);
    const suggested = next.toISOString().slice(0, 16);
    const newDue = prompt('New Due Date (ISO, e.g., 2025-10-31T12:00):', suggested);
    if (!newDue) return;
    const reason = prompt('Reason for extension:', 'Customer requested extension');
    if (reason === null) return;
    let iso: string;
    try {
      iso = new Date(newDue).toISOString();
    } catch {
      iso = newDue; // fallback to what user entered
    }
    this.rentals.extendRental(this.rentalId, iso, reason).subscribe({
      next: () => {
        // refresh
        this.rental$ = this.rentals.getById(this.rentalId);
      },
      error: (err) => alert('Failed to extend: ' + (err?.error || err?.message || err)),
    });
  }

  // Cancel rental (admin)
  cancel(force = false) {
    if (!confirm(force ? 'Force cancel this rental?' : 'Cancel this rental?')) return;
    this.rentals.cancelRental(this.rentalId, force).subscribe({
      next: () => this.backToList(),
      error: (err) => alert('Failed to cancel: ' + (err?.error || err?.message || err)),
    });
  }

  // Mark as returned
  markReturned() {
    const notes = prompt('Return notes (optional):', 'Returned in good condition') || '';
    const condition = prompt('Return condition:', 'Good') || 'Good';
    this.rentals.returnRental(this.rentalId, notes, condition).subscribe({
      next: () => this.backToList(),
      error: (err) => {
        const status = err?.status ?? 0;
        if (status === 401 || status === 403) {
          if (confirm('Unauthorized to return. Force return as admin?')) {
            this.rentals.forceReturn(this.rentalId, notes, condition).subscribe({
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
