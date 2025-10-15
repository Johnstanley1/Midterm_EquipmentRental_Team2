import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CustomerDto } from '../../../services/model.services';
import { CustomerService } from '../../../services/customer.services';
import { AsyncPipe, CommonModule, isPlatformBrowser } from '@angular/common';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-customer-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './customer-screen.html',
  styleUrls: ['./customer-screen.css'],
})
export class CustomerScreen {
  customers$: Observable<CustomerDto[]>;
  errorMessage: string | null = null;

  constructor(private customers: CustomerService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.customers$ = isBrowser
      ? this.customers.getAll().pipe(
          catchError((err) => {
            // Friendly message for permission issues or others
            if (err?.status === 403) {
              this.errorMessage = 'You do not have permission to view customers.';
            } else if (err?.status === 401) {
              this.errorMessage = 'Your session has expired. Please log in again.';
            } else {
              this.errorMessage = 'Failed to load customers. Please try again.';
            }
            return of([] as CustomerDto[]);
          })
        )
      : of([] as CustomerDto[]);
  }

  onDelete(id: number) {
    if (!confirm('Delete this customer?')) return;
    this.customers.delete(id).subscribe(() => {
      // refresh stream after delete
      this.customers$ = this.customers.getAll();
    });
  }
  // purely list view per request
}
