import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Rental, RentalService } from '../../../services/rental-services';

type OverdueRental = Rental & { daysOverdue: number };

@Component({
  selector: 'app-overdue-rentals-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './overdue-rentals-screen.html',
  styleUrls: ['./overdue-rentals-screen.css'],
})
export class OverdueRentalsScreen {
  private overdueSubject = new BehaviorSubject<OverdueRental[]>([]);
  overdue$: Observable<OverdueRental[]> = this.overdueSubject.asObservable();
  stats$!: Observable<{ total: number; avgDays: number; maxDays: number }>;
  isAdmin = false;
  private lastList: OverdueRental[] = [];

  constructor(
    private rentals: RentalService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    if (isBrowser) {
      this.isAdmin = (localStorage.getItem('role') || '').toLowerCase() === 'admin';
      this.load();
    }
  }

  trackId(_: number, r: OverdueRental) {
    return r.id;
  }

  onExtend(r: Rental) {
    this.router.navigate(['/rental-edit', r.id]);
  }

  onForceReturn(r: Rental) {
    if (!confirm('Force return this equipment now?')) return;
    if (this.isAdmin) {
      this.rentals.returnRental(r.id, 'Force returned by admin', 'Good', true).subscribe({
        next: () => {
          // Optimistically remove the item and push to stream
          this.lastList = this.lastList.filter((x) => x.id !== r.id);
          this.overdueSubject.next([...this.lastList]);
          // Then reload from server to ensure consistency
          this.load();
        },
        error: (err) => alert('Failed to force return: ' + (err?.error || err?.message || err)),
      });
    } else {
      // Non-admins cannot force; attempt normal return (allowed if it's their own rental)
      this.rentals.returnRental(r.id, 'Returned by user').subscribe({
        next: () => {
          this.lastList = this.lastList.filter((x) => x.id !== r.id);
          this.overdueSubject.next([...this.lastList]);
          this.load();
        },
        error: (err) => alert('Failed to return: ' + (err?.error || err?.message || err)),
      });
    }
  }

  private load() {
    this.rentals
      .getOverdue()
      .pipe(
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
      )
      .subscribe((list) => {
        this.lastList = list;
        this.overdueSubject.next(list);
      });
    this.stats$ = this.overdueSubject.asObservable().pipe(
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
