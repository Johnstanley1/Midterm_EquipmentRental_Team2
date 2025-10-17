import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Customer, CustomerDto } from '../../../services/model-services';
import { CustomerService } from '../../../services/customer-services';

@Component({
  selector: 'app-edit-customer-screen',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './edit-customer-screen.html',
  styleUrls: ['./edit-customer-screen.css'],
})
export class EditCustomerScreen {
  private fb = inject(FormBuilder);
  private service = inject(CustomerService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);

  id!: number;

  form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],
    username: [{ value: '', disabled: true }, [Validators.required, Validators.maxLength(50)]],
    password: ['', [Validators.maxLength(100)]],
    role: ['User', [Validators.required]],
    isActive: [true],
  });

  get refName() {
    return this.form.controls['name'];
  }

  ngOnInit() {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.service.getById(this.id).subscribe((c: CustomerDto) => {
      this.form.patchValue({
        name: c.name,
        username: c.username,
        role: c.role,
        isActive: c.isActive,
      });
    });
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const raw = this.form.getRawValue();
    const payload: Customer = {
      name: raw.name!,
      username: raw.username!,
      password: raw.password ?? '',
      role: raw.role!,
      isActive: !!raw.isActive,
    };
    this.service
      .update(this.id, payload)
      .subscribe(() => this.router.navigate(['/view-customers']));
  }
}
