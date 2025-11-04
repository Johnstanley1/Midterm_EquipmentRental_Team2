import {Component, inject, Inject, PLATFORM_ID} from '@angular/core';
import {CommonModule, isPlatformBrowser} from '@angular/common';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalService } from '../../../services/rental-services';
import {Observable, of} from 'rxjs';
import {Equipment, RentalDTO} from '../../../services/model-services';
import {EquipmentService} from '../../../services/equipment-services';

@Component({
  selector: 'app-rental-return-screen',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './rental-return-screen.html',
  styleUrls: ['./rental-return-screen.css'],
})
export class RentalReturnScreen {
  rentalId: number;
  equipmentCondition$: Observable<Equipment[]>;
  rental!: RentalDTO;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private rentalService: RentalService,
    private equipmentService: EquipmentService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    const isBrowser = isPlatformBrowser(platformId);
    this.equipmentCondition$ = isBrowser ? this.equipmentService.getEquipmentCondition() : of([]);
    this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
  }

  // Inject builder
  builder = inject(FormBuilder)

  // Validation
  form = this.builder.group({
    equipmentCondition: ["", [Validators.required]],
    notes: ["", [Validators.required]],
  })

  // Get data:
  refCondition = this.form.controls['equipmentCondition']
  refNotes = this.form.controls['notes']

  ngOnInit(): void {
    this.rentalId = this.route.snapshot.params['id'];
    this.rentalService.getRentalsById(this.rentalId).subscribe(rental => {
      if (rental) {
        this.rental = rental; // <-- save original rental
        this.form.patchValue({
          equipmentCondition: rental.equipmentCondition,
          notes: rental.returnNotes,
        });
      }
    });
  }

  submit() {
    if (this.form.valid) {
      console.log("Return rental form is valid")

      this.rentalService.returnRental(this.rental).subscribe(() =>{
        alert("Rental returned successfully");
        this.router.navigate(["/all-rentals"]);
      })
    }else {
      alert("Return rental form is invalid")
    }
  }

  back() {
    this.router.navigate(['/rental-detail', this.rentalId]);
  }
}
