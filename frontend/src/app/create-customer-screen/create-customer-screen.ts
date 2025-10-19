import {Component, Inject, inject, PLATFORM_ID} from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {CommonModule, isPlatformBrowser, NgOptimizedImage} from '@angular/common';
import {Router, RouterLink} from '@angular/router';
import {Customer, CustomerDTO} from '../../../services/model-services';
import { CustomerService } from '../../../services/customer-services';
import {Observable, of} from 'rxjs';

@Component({
  selector: 'app-create-customer-screen',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, RouterLink, NgOptimizedImage],
  templateUrl: './create-customer-screen.html',
  styleUrls: ['./create-customer-screen.css'],
})
export class CreateCustomerScreen {
  private fb = inject(FormBuilder);
  roleOptions$: Observable<CustomerDTO[]>

  constructor(private router: Router, private customerService: CustomerService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.roleOptions$ = isBrowser ? this.customerService.getCustomerRoles() : of([]);
  }



  min_Length = 3;
  max_length = 50;

  createForm = this.fb.group({
    _name: [
    "",
      [
        Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length),
      ],
    ],
    _username: ["", [Validators.required, Validators.minLength(this.min_Length), Validators.maxLength(this.max_length)]],
    _password: ["", [Validators.required, Validators.minLength(this.min_Length), Validators.maxLength(this.max_length)]],
    _role: ["", [Validators.required]],
    _isActive: [false],
  });

  get refName() {
    return this.createForm.controls['_name'];
  }

  get refUserName() {
    return this.createForm.controls['_username'];
  }

  get refPassword() {
    return this.createForm.controls['_password'];
  }

  get refRole() {
    return this.createForm.controls['_role'];
  }

  get refIsActive() {
    return this.createForm.controls['_isActive'];
  }

  onSubmit() {
    if (this.createForm.valid) {

      console.log("Add customer form valid")

      // Get data:
      const customer_name = this.createForm.value._name!
      const username = this.createForm.value._username!;
      const password = this.createForm.value._password!;
      const role = this.createForm.value._role!;
      const isActive = this.createForm.value._isActive!;

      // Create new customer object, pass data:
      const customer = new Customer(customer_name, username, password, role, isActive)


      this.customerService.createCustomer(customer).subscribe(() => {
        alert("Customer added successfully");
        // Route:
        this.router.navigate(["/all-customers"]);
      })

    }else{
      alert("Add customer form is invalid")
    }
  }
}
