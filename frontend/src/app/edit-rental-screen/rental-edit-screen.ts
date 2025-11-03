import {Component, Inject, PLATFORM_ID, ChangeDetectorRef, inject} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RentalService } from '../../../services/rental-services';
import { isPlatformBrowser } from '@angular/common';
import {Observable, of} from 'rxjs';
import {Customer, CustomerDTO, Equipment, RentalDTO} from '../../../services/model-services';
import {catchError} from 'rxjs/operators';
import {CustomerService} from '../../../services/customer-services';
import {EquipmentService} from '../../../services/equipment-services';

@Component({
  selector: 'app-rental-edit-screen',
  standalone: true,
    imports: [CommonModule, ReactiveFormsModule, NgOptimizedImage],
  templateUrl: './rental-edit-screen.html',
  styleUrls: ['./rental-edit-screen.css'],
})
export class RentalEditScreen {
  rentals$: Observable<RentalDTO []>;
  equipments$: Observable<Equipment []>;
  customers$: Observable<CustomerDTO []>;
  equipmentStatus$: Observable<Equipment[]>;
  equipmentCondition$: Observable<Equipment[]>;
  rental!: RentalDTO;
  rentalId = 0;
  errorMessage: string | null = null;
  private today: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private rentalService: RentalService,
    private equipmentService: EquipmentService,
    private customerService: CustomerService,
    private cdr: ChangeDetectorRef,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    this.equipments$ = isBrowser ? this.equipmentService.getAllEquipments() : of([]);
    this.customers$ = isBrowser ? this.customerService.getAllCustomers() : of([]);
    this.rentals$ = isBrowser ? this.rentalService.getAllRentals() : of([]);
    this.equipmentStatus$ = isBrowser ? this.equipmentService.getEquipmentStatus() : of([]);
    this.equipmentCondition$ = isBrowser ? this.equipmentService.getEquipmentCondition() : of([]);
  }

  builder = inject(FormBuilder);

  form = this.builder.group({
    equipmentName: [null as Equipment | null, Validators.required],
    customerName: [null as CustomerDTO | null, Validators.required],
    issuedAt: [null as Date | null, [Validators.required]],
    dueDate: [null as Date | null, [Validators.required]],
    returnedAt: [null as Date | null, [Validators.required]],
    returnNotes: ["", [Validators.required]],
    equipmentId: [0],
    status: ["Active"],
    equipmentStatus: ["Rented"],
    equipmentCondition: ["", [Validators.required]],
    equipment: [null as Equipment | null],
    customerId: [0]
  })

  // Get data:
  refName = this.form.controls['equipmentName']
  refCusName = this.form.controls['customerName']
  refIssue = this.form.controls['issuedAt']
  refDue = this.form.controls['dueDate']
  refCondition = this.form.controls["equipmentCondition"]
  refStatus = this.form.controls["equipmentStatus"]

  ngOnInit(): void {
    this.rentalId = this.route.snapshot.params['id'];
    this.rentalService.getRentalsById(this.rentalId).subscribe(rental => {
      if (rental) {
        const mappedRental = {
          ...rental,
          equipmentName: rental.equipmentName ? {name: rental.equipmentName} as Equipment: null,
          customerName: rental.customerName ? {name: rental.customerName} as CustomerDTO: null
        }
        this.form.patchValue(mappedRental);
        console.log(mappedRental);
      }
    });
  }

  onSubmit() {
    // Check Validation
    if (this.form.valid) {
      console.log("modify rental form is valid")

      // get new entries
      const updateRental = new RentalDTO(
        this.form.value.issuedAt!,
        this.form.value.dueDate!,
        this.form.value.returnedAt!,
        this.form.value.returnNotes!,
        this.form.value.customerId!,
        this.form.value.equipmentId!,
        this.form.value.equipmentCondition!,
        this.form.value.equipmentStatus!,
        this.form.value.status!,
      );

      updateRental.id = this.rentalId;

      this.rentalService.updateRental(this.rentalId, updateRental).subscribe(() => {
        alert("Rental modified successfully");
        this.router.navigate(["/all-rentals"]);
      });
    }else {
      alert("Modify rental form is invalid")
    }
  }

  back() {
    this.router.navigate(['/rental-detail', this.rentalId]);
  }
}
