import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import { BehaviorSubject, Observable, of } from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import { RentalService } from '../../../services/rental-services';
import {RentalDTO} from '../../../services/model-services';
import {ReactiveFormsModule} from '@angular/forms';

type OverdueRental = RentalDTO & { daysOverdue: number };

@Component({
  selector: 'app-overdue-rentals-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink, ReactiveFormsModule],
  templateUrl: './overdue-rentals-screen.html',
  styleUrls: ['./overdue-rentals-screen.css'],
})
export class OverdueRentalsScreen {
  errorMessage: string | null = null;
  overdueRentals$ =  of<RentalDTO [] | null>(null);
  rental!: RentalDTO;
  today = new Date;



  private overdueSubject = new BehaviorSubject<OverdueRental[]>([]);
  stats$!: Observable<{ total: number; avgDays: number; maxDays: number }>;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private rentalService: RentalService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    this.overdueRentals$ = isBrowser
      ? this.overdueRentals$ = this.rentalService.getOverdueRental().pipe(
        catchError((err) => {
          // Friendly message for permission issues or others
          if (err?.status === 403) {
            this.errorMessage = 'You do not have permission to view overdue rentals.';
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

  ngOnInit(): void {
    this.stats$ = this.overdueRentals$.pipe(
      map((list) => {
        if (!list || !list.length) return { total: 0, avgDays: 0, maxDays: 0 };
        const days = list.map((r) => this.getDaysOverdue(r?.dueDate));
        const total = list.length;
        const avgDays = total ? days.reduce((a, b) => a + b, 0) / total : 0;
        const maxDays = Math.max(...days);
        return { total, avgDays, maxDays };
      })
    );

  }

  getDaysOverdue(dueDate: Date | null): number {
    if (!dueDate) return 0;
    const due = new Date(dueDate).getTime();
    const now = this.today.getTime();
    if (now <= due) return 0;
    const diffMs = now - due;
    return Math.floor(diffMs / (1000 * 60 * 60 * 24));
  }

  getDaysRented(issuedAt: Date | null): number {
    if (!issuedAt) return 0;
    const issued = new Date(issuedAt).getTime();
    const now = this.today.getTime();
    const diffMs = now - issued;
    return Math.floor(diffMs / (1000 * 60 * 60 * 24));
  }

  onExtend(r: RentalDTO) {
    this.router.navigate(['/edit-rental', r.id]);
  }

  onForceReturn(r: RentalDTO) {
    if (!confirm('Force return this equipment now?')) return;

    const role = localStorage.getItem("role")

    if (role == "Admin") {
      this.rentalService.returnRental(this.rental).subscribe(() =>{
        alert("Rental returned successfully");
        this.router.navigate(["/all-rentals"]);
      });
    } else {
      alert("You are not authorised to force return a rental")
    }
  }
}
