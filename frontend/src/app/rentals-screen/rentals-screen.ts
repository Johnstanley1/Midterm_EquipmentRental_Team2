import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable, of } from 'rxjs';
import { RentalService } from '../../../services/rental-services';
import {RentalDTO} from '../../../services/model-services';

@Component({
  selector: 'app-rentals-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './rentals-screen.html',
  styleUrls: ['./rentals-screen.css'],
})
export class RentalsScreen {
  rentals$: Observable<RentalDTO[]> = of([]);

  constructor(private rentals: RentalService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.rentals$ = isBrowser ? this.rentals.getAllRentals() : of([]);
  }

  trackId(_: number, r: RentalDTO) {
    return r.id;
  }
}
