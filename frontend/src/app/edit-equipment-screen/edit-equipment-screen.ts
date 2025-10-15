import { Component } from '@angular/core';
import {AsyncPipe} from "@angular/common";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";

@Component({
  selector: 'app-edit-equipment-screen',
    imports: [
        FormsModule,
        ReactiveFormsModule
    ],
  templateUrl: './edit-equipment-screen.html',
  styleUrl: './edit-equipment-screen.css'
})
export class EditEquipmentScreen {

}
