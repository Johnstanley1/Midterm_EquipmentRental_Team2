import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RentalService } from '../../../services/rental.services';

@Component({
  selector: 'app-rental-return-screen',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './rental-return-screen.html',
  styleUrls: ['./rental-return-screen.css'],
})
export class RentalReturnScreen {
  form;

  rentalId: number;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private rentals: RentalService,
    private router: Router
  ) {
    this.rentalId = Number(this.route.snapshot.paramMap.get('id'));
    this.form = this.fb.group({
      condition: ['Good'],
      notes: [''],
    });
  }

  submit() {
    const v = this.form.value;
    this.rentals
      .returnRental(this.rentalId, v.notes || '', v.condition || 'Good')
      .subscribe(() => this.router.navigate(['/all-rentals']));
  }
}
