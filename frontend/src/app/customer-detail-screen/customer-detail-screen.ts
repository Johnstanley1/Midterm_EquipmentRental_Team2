import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';
import {CustomerDTO, RentalDTO} from '../../../services/model-services';
import { CustomerService } from '../../../services/customer-services';
import { Observable, of, switchMap, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { RentalService } from '../../../services/rental-services';
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
  activeRentals$ =  of<RentalDTO []| null>(null);
  customerId!: number;
  errorMessage: string | null = null;


  constructor(
    private route: ActivatedRoute,
    private customerService: CustomerService,
    private http: HttpClient,
    private rentalService: RentalService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

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

      this.activeRentals$ = this.rentalService.getActiveRental().pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load customer details';
          console.error(err);
          return of(null);
        })
      );
    }
  }

  onReturn(rentalDTO: RentalDTO) {
    if (confirm('Return this equipment now?')) return;


  }
}
