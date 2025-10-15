import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {EquipmentService} from '../../../services/equipment.services';
import {Equipment} from '../../../services/model.services';

@Component({
  selector: 'app-create-equipment-screen',
    imports: [
        ReactiveFormsModule
    ],
  templateUrl: './create-equipment-screen.html',
  styleUrl: './create-equipment-screen.css'
})
export class CreateEquipmentScreen {
  min_Length = 4;
  max_length = 50;

  constructor(private router: Router, private equipmentService: EquipmentService) {}

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

    _status: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]
    ],

    _category: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]
    ],

    _condition: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]
    ]
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
      console.log("Add photo form valid")

      // Get data:
      const equipment_name = this.createForm.value._equipment_name!
      const description = this.createForm.value._description!;
      const isAvailable = this.createForm.value._isAvailable!;
      const status = this.createForm.value._status!;
      const category = this.createForm.value._category!;
      const condition = this.createForm.value._condition!;

      // Create new equipment object, pass data:
      const equipment = new Equipment(equipment_name, description, isAvailable, status, category, condition)


      this.equipmentService.createEquipment(equipment).subscribe(() => {
        alert("Equipment added successfully");
        // Route:
        this.router.navigate(["/manage-equipments"]);
      })
    }else {
      alert("Add equipment form is invalid")
    }
  }
}
