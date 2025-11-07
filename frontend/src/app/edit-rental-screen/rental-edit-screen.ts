import {Component, Inject, PLATFORM_ID, ChangeDetectorRef, inject} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RentalService } from '../../../services/rental-services';
import { isPlatformBrowser } from '@angular/common';
import {Observable, of} from 'rxjs';
import {CustomerDTO, Equipment, Rental, RentalDTO} from '../../../services/model-services';
import {CustomerService} from '../../../services/customer-services';
import {EquipmentService} from '../../../services/equipment-services';

@Component({
  selector: 'app-rental-edit-screen',
  standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './rental-edit-screen.html',
  styleUrls: ['./rental-edit-screen.css'],
})
export class RentalEditScreen {
  rentals$: Observable<RentalDTO []>;
  equipments$: Observable<Equipment []>;
  customers$: Observable<CustomerDTO []>;
  equipmentStatus$: Observable<Equipment[]>;
  equipmentCondition$: Observable<Equipment[]>;
  rental!: Rental;
  rentalId = 0;
  errorMessage: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private rentalService: RentalService,
    private equipmentService: EquipmentService,
    private customerService: CustomerService,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    this.equipments$ = isBrowser ? this.equipmentService.getAllEquipments() : of([]);
    this.customers$ = isBrowser ? this.customerService.getAllCustomers() : of([]);
    this.rentals$ = isBrowser ? this.rentalService.getAllRentals() : of([]);
    this.equipmentStatus$ = isBrowser ? this.equipmentService.getEquipmentStatus() : of([]);
    this.equipmentCondition$ = isBrowser ? this.equipmentService.getEquipmentCondition() : of([]);
  }

  // Inject builder
  builder = inject(FormBuilder)

  // Validation
  form = this.builder.group({
    equipmentName: ["", Validators.required],
    customerName: ["", Validators.required],
    issuedAt: [null as Date | null, [Validators.required]],
    dueDate: [null as Date | null, [Validators.required]],
    returnedAt: [null  as Date | null],
    returnNotes: [""],
    equipmentId: [0],
    status: [""],
    equipmentStatus: ["", [Validators.required]],
    equipmentCondition: ["", [Validators.required]],
    equipment: [null as Equipment | null],
    customerId: [0]
  })

  // Get data:
  refName = this.form.controls['equipmentName']
  refCusName = this.form.controls['customerName']
  refCondition = this.form.controls["equipmentCondition"]
  refStatus = this.form.controls["equipmentStatus"]
  refIssue = this.form.controls['issuedAt']
  refDue = this.form.controls['dueDate']


  ngOnInit(): void {
    this.rentalId = this.route.snapshot.params['id'];
    this.rentalService.GetRentalEntityById(this.rentalId).subscribe(rental => {
        console.log('Rental received from API:', rental);
        if (!rental) {
          console.warn('No rental returned from backend.');
          return;
        }
        this.rental = rental;
        console.log('Equipment:', rental.equipment);
        console.log('Customer:', rental.customer);

      // if (rental) {
      //   this.rental = rental; // <-- save original rental
      //   console.log(this.rental.equipment.name);
      //   this.form.patchValue({
      //     equipmentName: rental.equipment.name,
      //     customerName: rental.customer.name,
      //     equipmentCondition: rental.equipmentCondition,
      //     equipmentStatus: rental.equipmentStatus,
      //     issuedAt: rental.issuedAt,
      //     dueDate: rental.dueDate,
      //     returnedAt: rental.returnedAt,
      //     returnNotes: rental.returnNotes,
      //     customerId: rental.customerId,
      //     equipmentId: rental.equipmentId,
      //     status: rental.status,
      //     equipment: rental.equipment,
      //   });
      // }
    });
  }

  onSubmit() {
  //   // Check Validation
  //   if (this.form.valid) {
  //     console.log("modify rental form is valid")
  //
  //     // get existing entries
  //     const updatedRental = new Rental(
  //       this.form.value.issuedAt!,
  //       this.form.value.dueDate!,
  //       this.form.value.returnedAt!,
  //       this.form.value.returnNotes!,
  //       this.form.value.status!,
  //       this.form.value.equipmentCondition!,
  //       this.form.value.equipmentStatus!,
  //       this.form.value.customerId!,
  //       this.form.value.equipmentId!,
  //       this.form.value.equipment!,
  //       this.form.value.customerName!
  //     );
  //
  //     updatedRental.id = this.rentalId;
  //     console.log(updatedRental);
  //
  //     this.rentalService.updateRental(this.rentalId, updatedRental).subscribe(() => {
  //       alert("Rental modified successfully");
  //       this.router.navigate(["/all-rentals"]);
  //     });
  //   }else {
  //     alert("Modify rental form is invalid")
  //   }
  }

  back() {
    this.router.navigate(['/rental-detail', this.rentalId]);
    console.log(this.rentalId);
  }
}
