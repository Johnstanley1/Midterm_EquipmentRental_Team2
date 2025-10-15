import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
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

  constructor(
    private route: ActivatedRoute,
    private rentals: RentalService,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.rental$ = isBrowser && !isNaN(id) ? this.rentals.getById(id) : of(null);
  }
}
