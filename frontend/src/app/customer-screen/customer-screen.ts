import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CustomerDTO } from '../../../services/model-services';
import { CustomerService } from '../../../services/customer-services';
import {AsyncPipe, CommonModule, isPlatformBrowser, NgOptimizedImage} from '@angular/common';
import {map, Observable, of} from 'rxjs';
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
  customers$: Observable<CustomerDTO[]>;
  errorMessage: string | null = null;

  constructor(private customers: CustomerService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.customers$ = isBrowser
      ? this.customers.getAllCustomers().pipe(
          catchError((err) => {
            // Friendly message for permission issues or others
            if (err?.status === 403) {
              this.errorMessage = 'You do not have permission to view customers.';
            } else if (err?.status === 401) {
              this.errorMessage = 'Your session has expired. Please log in again.';
            } else {
              this.errorMessage = 'Failed to load customers. Please try again.';
            }
            return of([] as CustomerDTO[]);
          })
        )
      : of([] as CustomerDTO[]);
  }

  onDelete(id: number) {
    if (!confirm('Delete this customer?')) return;

    const role = localStorage.getItem('role');
    if (role == "Admin") {
      this.customers$ = this.customers$.pipe(
        map((customers) => customers.filter((c) => c.id !== id))
      );

      this.customers.deleteCustomer(id).subscribe({
        error: (err) => {
          console.error('Delete failed:', err);
        }
      })
    }else{
      alert("You don't have permission to delete the customer");
    }
  }
}
