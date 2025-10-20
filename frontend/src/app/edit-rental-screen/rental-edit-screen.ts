import { Component, Inject, PLATFORM_ID, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Rental, RentalService } from '../../../services/rental-services';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-rental-edit-screen',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './rental-edit-screen.html',
  styleUrls: ['./rental-edit-screen.css'],
})
export class RentalEditScreen {
  form!: FormGroup;
  rental!: Rental;
  rentalId = 0;
  loading = true;
  errorMsg: string | null = null;
  private isBrowser = true;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private rentals: RentalService,
    private cdr: ChangeDetectorRef,
    @Inject(PLATFORM_ID) platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(platformId);
  }

  ngOnInit() {
    this.form = this.fb.group({
      dueDate: ['', Validators.required],
      reason: [''],
    });
    this.route.paramMap.subscribe((params) => {
      const id = Number(params.get('id'));
      this.rentalId = id;
      if (!this.isBrowser) {
        // Do not fetch on server; rely on client load
        this.loading = false;
        this.errorMsg = 'This page requires a browser environment.';
        this.cdr.markForCheck();
        return;
      }
      if (!isNaN(id) && id > 0) {
        this.rentals.getById(id).subscribe({
          next: (r) => {
            this.rental = r;
            const suggested = this.toLocalInputValue(r.dueDate || r.issuedAt);
            this.form.patchValue({ dueDate: suggested, reason: '' });
            this.loading = false;
            this.cdr.markForCheck();
          },
          error: (err) => {
            this.loading = false;
            this.errorMsg = typeof err?.error === 'string' ? err.error : 'Failed to load rental.';
            this.cdr.markForCheck();
          },
        });
      } else {
        this.loading = false;
        this.errorMsg = 'Invalid rental id.';
        this.cdr.markForCheck();
      }
    });
  }

  private toLocalInputValue(iso?: string | null): string {
    try {
      const d = iso ? new Date(iso) : new Date();
      const pad = (n: number) => String(n).padStart(2, '0');
      const yyyy = d.getFullYear();
      const MM = pad(d.getMonth() + 1);
      const dd = pad(d.getDate());
      const hh = pad(d.getHours());
      const mm = pad(d.getMinutes());
      return `${yyyy}-${MM}-${dd}T${hh}:${mm}`; // for input[type=datetime-local]
    } catch {
      return '';
    }
  }

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const v = this.form.value;
    const iso = new Date(v.dueDate).toISOString();
    this.rentals.extendRental(this.rentalId, iso, v.reason || '').subscribe({
      next: () => this.router.navigate(['/rental-detail', this.rentalId]),
      error: (err) => alert('Failed to update: ' + (err?.error || err?.message || err)),
    });
  }

  back() {
    this.router.navigate(['/rental-detail', this.rentalId]);
  }
}
