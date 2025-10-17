import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { EquipmentService } from '../../../services/equipment.services';
import { RentalService } from '../../../services/rental.services';
import { CustomerService } from '../../../services/customer.services';

@Component({
  selector: 'app-rental-issue-screen',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './rental-issue-screen.html',
  styleUrls: ['./rental-issue-screen.css'],
})
export class RentalIssueScreen {
  equipments: Array<{ id: number; name: string }> = [];
  customers: Array<{ id: number; name: string }> = [];
  isAdmin = false;
  form!: FormGroup;
  private fb: FormBuilder;

  constructor(
    fb: FormBuilder,
    private equipmentService: EquipmentService,
    private rentalService: RentalService,
    private customerService: CustomerService,
    private router: Router,
    private http: HttpClient,
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
      issuedAt: [''],
      dueDate: [''],
    });
    // Avoid HTTP when running on server during prerender
    if (typeof window !== 'undefined') {
      this.equipmentService.getAllEquipments().subscribe((list) => {
        this.equipments = (list || [])
          .filter(
            (e: any) => e.isAvailable === true && (e.status === 0 || e.status === 'Available')
          )
          .map((e: any) => ({ id: e.id, name: e.name }));
      });
      this.customerService.getAll().subscribe((list) => {
        this.customers = (list || []).map((c: any) => ({ id: c.id, name: c.name }));
      });
    }
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const v = this.form.value;
    let issuedIso: string | null = null;
    if (v.issuedAt) {
      const d = new Date(v.issuedAt);
      if (!isNaN(d.getTime())) {
        issuedIso = d.toISOString();
      } else {
        // If parsing fails, omit issuedAt so backend uses UtcNow
        issuedIso = null;
      }
    }
    const proceedIssue = () => {
      this.rentalService
        .issueRental({
          equipmentId: Number(v.equipmentId),
          customerId: Number(v.customerId) || undefined,
          dueDate: v.dueDate || null,
          issuedAt: issuedIso,
        })
        .subscribe({
          next: () => this.router.navigate(['/all-rentals']),
          error: (err) => {
            const raw = err?.error;
            let msg = 'Failed to issue rental';
            if (typeof raw === 'string') {
              msg = raw;
            } else if (raw && typeof raw === 'object') {
              const title = raw.title || raw.error || '';
              const detail = raw.detail || raw.message || '';
              const errors = raw.errors ? Object.values(raw.errors).flat().join('\n') : '';
              msg = [title, detail, errors].filter(Boolean).join('\n');
            } else if (err?.message) {
              msg = err.message;
            }
            alert(msg);
          },
        });
    };

    // Multiple active rentals allowed; no pre-check needed
    proceedIssue();
  }
}
