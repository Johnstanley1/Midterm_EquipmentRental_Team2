import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {CommonModule} from '@angular/common';
import {ActivatedRoute, Router, RouterLink} from '@angular/router';
import {Customer, CustomerDTO, Equipment} from '../../../services/model-services';
import { CustomerService } from '../../../services/customer-services';

@Component({
  selector: 'app-edit-customer-screen',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './edit-customer-screen.html',
  styleUrls: ['./edit-customer-screen.css'],
})
export class EditCustomerScreen {
  private fb = inject(FormBuilder);
  private service = inject(CustomerService);
  private router = inject(Router);
  private route = inject(ActivatedRoute);
  min_Length = 3;
  max_length = 50;
  id!: number;

  form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(100)]],

    username: ['', [Validators.required, Validators.maxLength(50)]],

    password: ['', [Validators.maxLength(100)]],

    role: ['User', [Validators.required]],

    isActive: [true],
  });


  get refName() {
    return this.form.controls['name'];
  }

  get refUserName() {
    return this.form.controls['username'];
  }

  get refPassword() {
    return this.form.controls['password'];
  }

  get refRole() {
    return this.form.controls['role'];
  }

  get refIsActive() {
    return this.form.controls['isActive'];
  }


  ngOnInit(): void {
    this.id = Number(this.route.snapshot.paramMap.get('id'));
    this.service.getCustomerById(this.id).subscribe(customer => {
      if (customer) {
        this.form.patchValue(customer);
      }
      console.log(customer);
    });
  }

  // On 'modify' Click:
  onSubmit() {
    // Check Validation
    if (this.form.valid) {
      console.log("modify customer form valid")

      // get new entries
      const updatedCustomer = new Customer(
        this.form.value.name!,
        this.form.value.username!,
        this.form.value.password ?? '',
        this.form.value.role!,
        this.form.value.isActive!,
      );

      updatedCustomer.id = this.id;

      this.service.updateCustomer(this.id, updatedCustomer).subscribe(() => {
        alert("Customer modified successfully");
        this.router.navigate(["/all-customers"]);
      });
    }else {
      alert("Modify customer form is invalid")
    }
  }
}
