import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Customer } from '../../../services/model.services';
import { CustomerService } from '../../../services/customer.services';

@Component({
  selector: 'app-create-customer-screen',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './create-customer-screen.html',
  styleUrls: ['./create-customer-screen.css'],
})
export class CreateCustomerScreen {
  private fb = inject(FormBuilder);
  private service = inject(CustomerService);
  private router = inject(Router);

  min_Length = 3;
  max_length = 100;

  form = this.fb.group({
    name: [
      '',
      [
        Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length),
      ],
    ],
    username: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
    password: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
    role: ['User', [Validators.required]],
    isActive: [true],
  });

  get refName() {
    return this.form.controls['name'];
  }
  get refUsername() {
    return this.form.controls['username'];
  }
  get refPassword() {
    return this.form.controls['password'];
  }

  onSubmit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.value as Customer;
    this.service.create(value).subscribe(() => this.router.navigate(['/view-customers']));
  }
}
