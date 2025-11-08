import {Component, Inject, PLATFORM_ID, ChangeDetectorRef, inject} from '@angular/core';
import {CommonModule, NgOptimizedImage} from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { RentalService } from '../../../services/rental-services';
import { isPlatformBrowser } from '@angular/common';
import {combineLatest, Observable, of} from 'rxjs';
import {Customer, CustomerDTO, Equipment, Rental, RentalDTO} from '../../../services/model-services';
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
  equipments$!: Observable<Equipment []>;
  customers$!: Observable<CustomerDTO []>;
  equipmentStatus$: Observable<Equipment[]>;
  equipmentCondition$: Observable<Equipment[]>;
  rental!: Rental;
  equipments!: Equipment []
  customers!: CustomerDTO []
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
    returnedAt: [null  as Date | null],
    returnNotes: [""],
    equipmentId: [0],
    status: [""],
    equipmentStatus: ["", [Validators.required]],
    equipmentCondition: ["", [Validators.required]],
    equipment: [null as Equipment | null],
    customer: [null as Customer | null],
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
    this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
    combineLatest([
      this.rentalService.GetRentalEntityById(this.rentalId),
      this.equipmentService.getAllEquipments(),
      this.customerService.getAllCustomers()
    ]).subscribe(([rental, equipments, customers]) => {
      this.equipments = equipments
      this.customers = customers

      const selectedEquipment = equipments.find(e => e.id === rental.equipmentId);
      const selectedCustomer = customers.find(c => c.id === rental.customerId);

      if (rental) {
        this.rental = rental; // <-- save original rental
        this.form.patchValue({
          equipmentName: selectedEquipment  ?? null,
          customerName: selectedCustomer ?? null,
          equipmentCondition: rental.equipmentCondition,
          equipmentStatus: rental.equipmentStatus,
          issuedAt: rental.issuedAt,
          dueDate: rental.dueDate,
          returnedAt: rental.returnedAt,
          returnNotes: rental.returnNotes,
          customerId: rental.customerId,
          equipmentId: rental.equipmentId,
          status: rental.status,
        });
        console.log(this.form.value)
        console.log('Patched equipment object:', selectedEquipment);
        console.log('Patched customer object:', selectedCustomer);
      }
    });

      // console.log('Rental received from API:', rental);
      // if (!rental) {
      //   console.warn('No rental returned from backend.');
      //   return;
      // }
      // this.rentalDTO = rental;
      // console.log('Equipment:', rental.equipment);

      // this.rental = rental;
      // console.log('Equipment:', rental.equipment);
      // console.log('Customer:', rental.customer);

      // if (rental) {
      //   this.rentalDTO = rental; // <-- save original rental
      //   this.form.patchValue({
      //     equipmentName: rental.equipment,
      //     // customerName: rental.customer,
      //     equipmentCondition: rental.equipmentCondition,
      //     equipmentStatus: rental.equipmentStatus,
      //     issuedAt: rental.issuedAt,
      //     dueDate: rental.dueDate,
      //     returnedAt: rental.returnedAt,
      //     returnNotes: rental.returnNotes,
      //     customerId: rental.customerId,
      //     equipmentId: rental.equipmentId,
      //     status: rental.status
      //   });
      //   console.log(this.form.value)
      // }

  }


  onSubmit() {
    // Check Validation
    if (this.form.valid) {
      console.log("modify rental form is valid")

      // Get data
      const equipment = this.form.value.equipmentName;
      const customer = this.form.value.customerName;

      const issuedAt = this.form.value.issuedAt!;
      const dueDate = this.form.value.dueDate!;
      const returnedAt = this.form.value.returnedAt!;
      const returnNotes = this.form.value.returnNotes!;
      const status = this.form.value.status!;
      const equipmentStatus = this.form.value.equipmentStatus!;
      const equipmentCondition = this.form.value.equipmentCondition!;
      const customerId = customer?.id;
      const equipmentId = equipment?.id;
      const customerName = customer?.name;
      const equipmentName = equipment?.name;



      // get existing entries
      const updatedRental = new Rental(
        issuedAt!,
        dueDate!,
        returnedAt!,
        returnNotes!,
        status!,
        equipmentCondition!,
        equipmentStatus!,
        customerId!,
        equipmentId!,
        this.form.value.equipment!,
        this.form.value.customer!
      )

      // const updatedRentalDTO = new RentalDTO(
      //   this.form.value.equipment?.name!,
      //   this.form.value.customer?.name!,
      //   this.form.value.issuedAt!,
      //   this.form.value.dueDate!,
      //   this.form.value.returnedAt!,
      //   this.form.value.returnNotes!,
      //   this.form.value.customerId!,
      //   this.form.value.equipmentId!,
      //   this.form.value.equipmentCondition!,
      //   this.form.value.equipmentStatus!,
      //   this.form.value.status!,
      //   this.form.value.equipment!
      // );

      // const updatedRentalDTO = new RentalDTO(
      //   customerName!,
      //   equipmentName!,
      //   issuedAt!,
      //   dueDate!,
      //   returnedAt!,
      //   returnNotes!,
      //   customerId!,
      //   equipmentId!,
      //   equipmentCondition!,
      //   equipmentStatus!,
      //   status!,
      //   equipment!
      // );

      updatedRental.id = this.rentalId;
      console.log(updatedRental);

      // updatedRentalDTO.id = this.rentalId;
      // console.log(updatedRentalDTO);

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
