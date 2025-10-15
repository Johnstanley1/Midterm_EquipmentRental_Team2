import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, AsyncPipe, isPlatformBrowser } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Observable, of } from 'rxjs';
import { Rental, RentalService } from '../../../services/rental.services';

@Component({
  selector: 'app-rentals-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  template: `
    <div class="container mt-5">
      <div class="d-flex justify-content-between align-items-center mb-3">
        <h2 class="m-0">Rentals</h2>
        <a routerLink="/rental-issue" class="btn btn-success">Issue Rental</a>
      </div>
      <div class="table-responsive">
        <table class="table table-hover table-striped-columns table-bordered">
          <thead>
            <tr>
              <th>Equipment</th>
              <th>Customer</th>
              <th>Issue Date</th>
              <th>Status</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <ng-container *ngIf="rentals$ | async as rentals; else loading">
              <tr *ngIf="rentals.length === 0">
                <td colspan="5" class="text-center">No rentals found</td>
              </tr>
              <tr *ngFor="let r of rentals; trackBy: trackId">
                <td>{{ r.equipment?.name || r.equipmentId }}</td>
                <td>{{ r.customer?.name || r.customerId }}</td>
                <td>{{ r.issuedAt | date : 'mediumDate' }}</td>
                <td>
                  <span
                    class="badge"
                    [ngClass]="{
                      'bg-success': r.status === 'Completed',
                      'bg-danger': r.status === 'Overdue',
                      'bg-secondary': r.status === 'Cancelled',
                      'bg-primary': r.status === 'Active'
                    }"
                    >{{ r.status }}</span
                  >
                </td>
                <td class="text-end">
                  <a [routerLink]="['/rental-detail', r.id]" class="btn btn-outline-primary btn-sm"
                    >View</a
                  >
                </td>
              </tr>
            </ng-container>
          </tbody>
        </table>
        <ng-template #loading>
          <div class="text-center p-3">Loading...</div>
        </ng-template>
      </div>
    </div>
  `,
})
export class RentalsScreen {
  rentals$: Observable<Rental[]> = of([]);

  constructor(private rentals: RentalService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.rentals$ = isBrowser ? this.rentals.getAll() : of([]);
  }

  trackId(_: number, r: Rental) {
    return r.id;
  }
}
