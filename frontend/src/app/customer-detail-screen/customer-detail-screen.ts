import { Component, Inject, PLATFORM_ID } from '@angular/core';
import {CommonModule, isPlatformBrowser, NgOptimizedImage} from '@angular/common';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import { AsyncPipe } from '@angular/common';
import {CustomerDTO, RentalDTO} from '../../../services/model-services';
import { CustomerService } from '../../../services/customer-services';
import { of } from 'rxjs';
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
  activeRentals$ =  of<CustomerDTO | null>(null);
  customerRentals$ =  of<CustomerDTO | null>(null);
  customerId!: number;
  rentalId!: number;
  errorMessage: string | null = null;


  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService,
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

      this.activeRentals$ = this.customerService.getCustomerActiveRental(this.customerId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load customer active rental';
          console.error(err);
          return of(null);
        })
      );

      this.customerRentals$ = this.customerService.getAllCustomerRentals(this.customerId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load customer rentals';
          console.error(err);
          return of(null);
        })
      );
    }
  }

  onReturn(rentalDTO: RentalDTO) {
    confirm('Return this equipment now?');
    if (confirm()){
      this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
      this.router.navigate(["/rental-detail/:id"]);
    }
  }
}
