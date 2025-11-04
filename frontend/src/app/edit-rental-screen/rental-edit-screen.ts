import {Component, Inject, PLATFORM_ID, ChangeDetectorRef, inject} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RentalService } from '../../../services/rental-services';
import { isPlatformBrowser } from '@angular/common';
import {Observable, of} from 'rxjs';
import {CustomerDTO, Equipment, RentalDTO} from '../../../services/model-services';
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
  rental!: RentalDTO;
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
    this.rentalService.getRentalsById(this.rentalId).subscribe(rental => {
      if (rental) {
        this.rental = rental; // <-- save original rental
        this.form.patchValue({
          equipmentName: rental.equipmentName,
          customerName: rental.customerName,
          equipmentCondition: rental.equipmentCondition,
          equipmentStatus: rental.equipmentStatus,
          issuedAt: rental.issuedAt,
          dueDate: rental.dueDate,
          returnedAt: rental.returnedAt,
          returnNotes: rental.returnNotes,
          customerId: rental.customerId,
          equipmentId: rental.equipmentId,
          status: rental.status,
          equipment: rental.equipment,
        });
      }
    });
  }

  onSubmit() {
    // Check Validation
    if (this.form.valid) {
      console.log("modify rental form is valid")

      // get existing entries
      const updatedRental: RentalDTO = { ...this.rental };

      updatedRental.equipmentName = this.form.value.equipmentName!
      updatedRental.customerName = this.form.value.customerName!
      updatedRental.issuedAt = this.form.value.issuedAt!
      updatedRental.dueDate = this.form.value.dueDate!
      updatedRental.returnedAt = this.form.value.returnedAt!
      updatedRental.returnNotes = this.form.value.returnNotes!
      updatedRental.customerId = this.form.value.customerId!
      updatedRental.equipmentId = this.form.value.equipmentId!
      updatedRental.equipmentCondition = this.form.value.equipmentCondition!
      updatedRental.equipmentStatus = this.form.value.equipmentStatus!
      updatedRental.status = this.form.value.status!
      updatedRental.equipment = this.form.value.equipment!

      updatedRental.id = this.rentalId;

      this.rentalService.updateRental(this.rentalId, updatedRental).subscribe(() => {
        alert("Rental modified successfully");
        this.router.navigate(["/all-rentals"]);
      });
    }else {
      alert("Modify rental form is invalid")
    }
  }

  back() {
    this.router.navigate(['/rental-detail', this.rentalId]);
    console.log(this.rentalId);
  }
}
