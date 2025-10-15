import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { EquipmentService } from '../../../services/equipment.services';
import { RentalService } from '../../../services/rental.services';

@Component({
  selector: 'app-rental-issue-screen',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './rental-issue-screen.html',
  styleUrls: ['./rental-issue-screen.css'],
})
export class RentalIssueScreen {
  equipments: Array<{ id: number; name: string }> = [];
  isAdmin = false;
  form!: FormGroup;
  private fb: FormBuilder;

  constructor(
    fb: FormBuilder,
    private equipmentService: EquipmentService,
    private rentalService: RentalService,
    private router: Router,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.fb = fb;
    const isBrowser = isPlatformBrowser(platformId);
    if (isBrowser) {
      this.isAdmin = (localStorage.getItem('role') || '').toLowerCase() === 'admin';
    }
  }

  ngOnInit() {
    this.form = this.fb.group({
      equipmentId: [null, Validators.required],
      customerId: [null],
      dueDate: [''],
    });
    // Avoid HTTP when running on server during prerender
    if (typeof window !== 'undefined') {
      this.equipmentService.getAllEquipments().subscribe((list) => {
        this.equipments = (list || []).map((e) => ({ id: (e as any).id, name: (e as any).name }));
      });
    }
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const v = this.form.value;
    this.rentalService
      .issueRental({
        equipmentId: Number(v.equipmentId),
        customerId: this.isAdmin ? Number(v.customerId) || undefined : undefined,
        dueDate: v.dueDate || null,
      })
      .subscribe(() => this.router.navigate(['/all-rentals']));
  }
}
