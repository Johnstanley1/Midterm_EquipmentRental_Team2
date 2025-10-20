import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import {map, Observable, of} from 'rxjs';
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
    private rentalService: RentalService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}


  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
      this.rental$ = this.rentalService.getRentalsById(this.rentalId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load rental details';
          console.error(err);
          return of(null);
        })
      );
    }
  }


  // Cancel rental (admin)
  cancel(id: number): void {
    if (confirm('Are you sure you want to cancel this rental?')) {
      this.rentalService.deleteRental(id).subscribe({
        next: () => {
          // Navigate to all rentals screen after successful deletion
          this.router.navigate(['/all-rentals']);
        },
        error: (err) => {
          if (err.status === 404) {
            this.errorMessage = 'Rental not found.';
          } else if (err.status === 403) {
            this.errorMessage = 'You do not have permission to delete this rental.';
          } else {
            this.errorMessage = 'Failed to delete rental. Please try again.';
          }
        }
      });
    }
  }

}
