import {Component, inject} from '@angular/core';
import {FormBuilder, ReactiveFormsModule, Validators} from "@angular/forms";
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';

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
  max_length = 20;

  constructor(private http: HttpClient, private router: Router) {}


  // Inject builder
  builder = inject(FormBuilder)

  // Validation
  createForm = this.builder.group({
    _username: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]
    ],

    _password: ["",
      [Validators.required,
        Validators.minLength(this.min_Length),
        Validators.maxLength(this.max_length)]

    ]
  })

  // Get data:
  refName = this.createForm.controls['_username']
  refPassword = this.createForm.controls['_password']

  add(){}
}
