import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import {CustomerDTO, Equipment} from '../../../services/model-services';
import { CustomerService } from '../../../services/customer-services';
import { Observable, of, switchMap, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RentalService, Rental } from '../../../services/rental-services';
import {catchError} from 'rxjs/operators';

@Component({
  selector: 'app-customer-detail-screen',
  standalone: true,
  imports: [CommonModule, AsyncPipe, RouterLink],
  templateUrl: './customer-detail-screen.html',
  styleUrls: ['./customer-detail-screen.css'],
})
export class CustomerDetailScreen {
  customer$ =  of<CustomerDTO | null>(null);
  activeRental$ =  of<Rental | null>(null);
  customerId!: number;
  rentalId!: number;
  errorMessage: string | null = null;


  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService,
    private http: HttpClient,
    private rentalService: RentalService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  // ngOnInit() {
  //   const idParam = this.route.snapshot.paramMap.get('id');
  //   const id = idParam ? Number(idParam) : NaN;
  //   const isBrowser = isPlatformBrowser(this.platformId);
  //
  //   if (!isBrowser || isNaN(id)) {
  //     this.customer$ = of(null);
  //     return;
  //   }
  //   this.customer$ = this.service.getCustomerById(id);
  //   // fetch active rental for this customer
  //   this.activeRental$ = this.http.get<Rental>(`/api/Customer/${id}/active-rental`).pipe(
  //     map((r) => r as Rental),
  //     switchMap((r) => of(r))
  //     // if not found, API may return 404; keep null in that case
  //   );
  // }

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.customerId = Number(this.route.snapshot.paramMap.get('id'));
      this.customer$ = this.customerService.getCustomerById(this.customerId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load customer details';
          console.error(err);
          return of(null);
        })
      );

      this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
      this.activeRental$ = this.rentalService.getById(this.rentalId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load customer details';
          console.error(err);
          return of(null);
        })
      );
    }
  }

  onReturn(rental: Rental) {
    if (!confirm('Return this equipment now?')) return;
    this.rentalService.returnRental(rental.id).subscribe({
      next: () => {
        // refresh active rental after return
        this.activeRental$ = this.http.get<Rental>(
          `/api/Customer/${this.customerId}/active-rental`
        );
      },
    });
  }
}
