import {Component, inject, Inject, PLATFORM_ID} from '@angular/core';
import {CommonModule, isPlatformBrowser} from '@angular/common';
import {FormBuilder, ReactiveFormsModule, Validators} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalService } from '../../../services/rental-services';
import {Observable, of} from 'rxjs';
import {Equipment} from '../../../services/model-services';
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

  submit() {
    const v = this.form.value;
    this.rentalService.returnRental().subscribe(() =>{
      alert("Rental modified successfully");
      this.router.navigate(["/all-rentals"]);
    })
  }

  back() {
    this.router.navigate(['/rental-detail', this.rentalId]);
    console.log(this.rentalId);
  }
}
