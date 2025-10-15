import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Rental, RentalService } from '../../../services/rental.services';

type OverdueRental = Rental & { daysOverdue: number };

@Component({
  selector: 'app-overdue-rentals-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './overdue-rentals-screen.html',
  styleUrls: ['./overdue-rentals-screen.css'],
})
export class OverdueRentalsScreen {
  overdue$: Observable<OverdueRental[]> = of([]);
  stats$!: Observable<{ total: number; avgDays: number; maxDays: number }>;

  constructor(private rentals: RentalService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    if (isBrowser) {
      this.overdue$ = this.rentals.getOverdue().pipe(
        map((list) =>
          list.map(
            (r) =>
              ({
                ...r,
                daysOverdue: r.dueDate
                  ? Math.max(
                      0,
                      Math.ceil((Date.now() - Date.parse(r.dueDate)) / (1000 * 60 * 60 * 24))
                    )
                  : 0,
              } as OverdueRental)
          )
        )
      );
      this.stats$ = this.overdue$.pipe(
        map((list) => {
          const days = list.map((r) => r.daysOverdue || 0);
          const total = list.length;
          const avgDays = total ? days.reduce((a, b) => a + b, 0) / total : 0;
          const maxDays = days.length ? Math.max(...days) : 0;
          return { total, avgDays, maxDays };
        })
      );
    }
  }

  trackId(_: number, r: OverdueRental) {
    return r.id;
  }

  onExtend(r: Rental) {
    const newDue = prompt('Enter new due date (YYYY-MM-DD):');
    if (!newDue) return;
    const reason = prompt('Reason for extension:') || '';
    this.rentals.extendRental(r.id, newDue, reason).subscribe(() => {
      // refresh
      this.overdue$ = this.rentals.getOverdue() as any;
    });
  }

  onForceReturn(r: Rental) {
    if (!confirm('Force return this equipment now?')) return;
    this.rentals.forceReturn(r.id, 'Force returned by admin', 'Good').subscribe(() => {
      this.overdue$ = this.rentals.getOverdue() as any;
    });
  }
}
