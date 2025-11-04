import {Component, inject, Inject, PLATFORM_ID} from '@angular/core';
import {CommonModule, isPlatformBrowser, NgOptimizedImage} from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators, FormGroup } from '@angular/forms';
import {Router, RouterLink} from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { EquipmentService } from '../../../services/equipment-services';
import { RentalService } from '../../../services/rental-services';
import { CustomerService } from '../../../services/customer-services';
import {Observable, of} from 'rxjs';
import {CustomerDTO, Equipment, RentalDTO} from '../../../services/model-services';

@Component({
  selector: 'app-rental-issue-screen',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './rental-issue-screen.html',
  styleUrls: ['./rental-issue-screen.css'],
})
export class RentalIssueScreen {
  customers$: Observable<CustomerDTO[]>;
  equipments$: Observable<Equipment[]>;
  rentals$: Observable<RentalDTO[]>;
  equipmentStatus$: Observable<Equipment[]>;
  equipmentCondition$: Observable<Equipment[]>;

  constructor(private router: Router,
              private equipmentService: EquipmentService,
              private rentalService: RentalService,
              private customerService: CustomerService,
              @Inject(PLATFORM_ID) platformId: Object) {
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
    equipmentName: [null as Equipment | null, Validators.required],
    customerName: [null as CustomerDTO | null, Validators.required],
    issuedAt: [null as Date | null, [Validators.required]],
    dueDate: [null as Date | null, [Validators.required]],
    returnedAt: [null],
    returnNotes: [null],
    equipmentId: [0],
    status: ["Active"],
    equipmentStatus: ["Rented"],
    equipmentCondition: ["", [Validators.required]],
    equipment: [null],
    customerId: [0]
  })


  // Get data:
  refName = this.form.controls['equipmentName']
  refCusName = this.form.controls['customerName']
  refIssue = this.form.controls['issuedAt']
  refDue = this.form.controls['dueDate']
  refCondition = this.form.controls["equipmentCondition"]
  refStatus = this.form.controls["equipmentStatus"]
  refNotes = this.form.controls["returnNotes"]


  submit() {
    // Check Validation
    if (this.form.valid) {
      console.log("Issue rental form is valid")

      // Get data:
      // const equipment_name = this.form.value.equipmentName!
      // const customerName = this.form.value.customerName!;

      const equipment = this.form.value.equipmentName;
      const customer = this.form.value.customerName;


      const issuedAt = this.form.value.issuedAt!;
      const dueDate = this.form.value.dueDate!;
      const returnedAt = this.form.value.returnedAt!;
      const returnNotes = this.form.value.returnNotes!;
      const status = this.form.value.status!;
      const equipmentStatus = this.form.value.equipmentStatus!;
      const equipmentCondition = this.form.value.equipmentCondition!;

      // const equipment = this.form.value.equipment!;

      const customerId = customer?.id;
      const equipmentId = equipment?.id;
      const customerName = customer?.name;
      const equipmentName = equipment?.name;

      // Create new equipment object, pass data:
      if (customerId != null && equipmentId != null) {
        const rental = new RentalDTO(equipmentName!, customerName!, issuedAt, dueDate, returnedAt, returnNotes, customerId,
          equipmentId, equipmentCondition, equipmentStatus, status, equipment!)

        this.rentalService.createRental(rental).subscribe(() => {
          alert("Rental issued successfully");
          // Route:
          this.router.navigate(["/all-rentals"]);
        })

      }
    }else {
      alert("Issue rental form is invalid")
    }
  }
}
