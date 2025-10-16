import {Component, inject, Inject, PLATFORM_ID} from '@angular/core';
import {AsyncPipe, isPlatformBrowser} from "@angular/common";
import {FormBuilder, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Observable, of} from 'rxjs';
import {Equipment} from '../../../services/model.services';
import {ActivatedRoute, Router} from '@angular/router';
import {EquipmentService} from '../../../services/equipment.services';

@Component({
  selector: 'app-edit-equipment-screen',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    AsyncPipe
  ],
  templateUrl: './edit-equipment-screen.html',
  styleUrl: './edit-equipment-screen.css'
})
export class EditEquipmentScreen {
  id!: number
  min_Length = 4;
  max_length = 50;
  _statusOptions$: Observable<Equipment[]>;
  _conditionOptions$: Observable<Equipment[]>;
  _categoryOptions$: Observable<Equipment[]>;

  constructor(private router: Router, private equipmentService: EquipmentService,
              @Inject(PLATFORM_ID) platformId: Object, private route: ActivatedRoute) {
    const isBrowser = isPlatformBrowser(platformId);
    this._statusOptions$ = isBrowser ? this.equipmentService.getEquipmentStatus() : of([]);
    this._conditionOptions$ = isBrowser ? this.equipmentService.getEquipmentCondition() : of([]);
    this._categoryOptions$ = isBrowser ? this.equipmentService.getEquipmentCategory() : of([]);
  }

  // Inject builder
  builder = inject(FormBuilder)

  // Validation
  modifyForm = this.builder.group({
    equipment_name: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]
    ],

    description: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]

    ],

    isAvailable: [false],

    status: ["", [Validators.required]],
    category: ["", [Validators.required]],
    condition: ["", [Validators.required]],

  });

  // Get data:
  refName = this.modifyForm.controls['equipment_name']
  refDescription = this.modifyForm.controls['description']
  refAvailable = this.modifyForm.controls['isAvailable']
  refStatus = this.modifyForm.controls['status']
  refCategory = this.modifyForm.controls['category']
  refCondition = this.modifyForm.controls['condition']

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.id = +id;
        this.loadEquipment(this.id);
      }
    });
  }

  loadEquipment(id: number) {
    this.equipmentService.getEquipmentById(this.id).subscribe(equipment => {
      if (equipment) {
        this.modifyForm.patchValue({
          equipment_name: equipment.name,
          description: equipment.description,
          isAvailable: equipment.isAvailable,
          status: equipment.status,
          category: equipment.category,
          condition: equipment.condition,
        });
      }
    });
  }

  // On 'modify' Click:
  save() {
    // Check Validation
    if (this.modifyForm.valid) {
      console.log("modify equipment form valid")

      // get new entries
      const updatedEquipment = new Equipment(
        this.modifyForm.value.equipment_name!,
        this.modifyForm.value.description!,
        this.modifyForm.value.isAvailable!,
        this.modifyForm.value.status!,
        this.modifyForm.value.category!,
        this.modifyForm.value.condition!
      );

      console.log('Form Value:', this.modifyForm.value);
      console.log('Form Value:', updatedEquipment);

      this.equipmentService.updateEquipment(this.id, updatedEquipment).subscribe(() => {
        alert("Equipment modified successfully");
        this.router.navigate(["/manage-equipment"]);
      });
    }else {
      alert("Modify equipment form is invalid")
    }
  }
}


