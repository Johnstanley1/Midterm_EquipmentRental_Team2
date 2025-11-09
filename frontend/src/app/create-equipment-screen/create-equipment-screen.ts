import {Component, Inject, inject, PLATFORM_ID} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient} from '@angular/common/http';
import {Router, RouterLink} from '@angular/router';
import {EquipmentService} from '../../../services/equipment-services';
import {Equipment} from '../../../services/model-services';
import {Observable, of} from 'rxjs';
import {AsyncPipe, isPlatformBrowser, NgOptimizedImage} from '@angular/common';

@Component({
  selector: 'app-create-equipment-screen',
  imports: [
    ReactiveFormsModule,
    AsyncPipe,
    RouterLink
  ],
  templateUrl: './create-equipment-screen.html',
  styleUrl: './create-equipment-screen.css'
})
export class CreateEquipmentScreen {
  min_Length = 4;
  max_length = 50;
  statusOptions$: Observable<Equipment[]>;
  conditionOptions$: Observable<Equipment[]>;
  categoryOptions$: Observable<Equipment[]>;

  constructor(private router: Router, private equipmentService: EquipmentService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.statusOptions$ = isBrowser ? this.equipmentService.getEquipmentStatus() : of([]);
    this.conditionOptions$ = isBrowser ? this.equipmentService.getEquipmentCondition() : of([]);
    this.categoryOptions$ = isBrowser ? this.equipmentService.getEquipmentCategory() : of([]);
  }

  // Inject builder
  builder = inject(FormBuilder)

  // Validation
  createForm = this.builder.group({
    _equipment_name: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]
    ],

    _description: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]

    ],

    _isAvailable: [false],

    _status: ["", [Validators.required]],
    _category: ["", [Validators.required]],
    _condition: ["", [Validators.required]],

  })

  // Get data:
  refName = this.createForm.controls['_equipment_name']
  refDescription = this.createForm.controls['_description']
  refAvailable = this.createForm.controls['_isAvailable']
  refStatus = this.createForm.controls['_status']
  refCategory = this.createForm.controls['_category']
  refCondition = this.createForm.controls['_condition']

  // On 'Add' Click:
  add() {
    // Check Validation
    if (this.createForm.valid) {
      console.log("Add equipment form valid")
      const role = localStorage.getItem('role')

      // Get data:
      const equipment_name = this.createForm.value._equipment_name!
      const description = this.createForm.value._description!;
      const isAvailable = this.createForm.value._isAvailable!;
      const status = this.createForm.value._status!;
      const category = this.createForm.value._category!;
      const condition = this.createForm.value._condition!;

      // Create new equipment object, pass data:
      const equipment = new Equipment(equipment_name, description, isAvailable, status, category, condition)

      if (role === "Admin"){
        this.equipmentService.createEquipment(equipment).subscribe(() => {
          alert("Equipment added successfully");
          // Route:
          this.router.navigate(["/all-equipments"]);
        })
      }else{
        alert("You are not authorized to add equipments")
      }
    }else {
      alert("Add equipment form is invalid")
    }
  }
}
