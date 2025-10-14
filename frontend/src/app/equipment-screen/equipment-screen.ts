import {Component, OnInit} from '@angular/core';
import {Equipment, EquipmentService} from '../../../services/equipment.services';
import {Router} from '@angular/router';

@Component({
  selector: 'app-equipment-screen',
  imports: [],
  templateUrl: './equipment-screen.html',
  styleUrl: './equipment-screen.css'
})
export class EquipmentScreen implements OnInit{
  equipments: Equipment[] = []

  constructor(private router: Router, private equipmentService: EquipmentService) {}

  ngOnInit(): void {
    this.equipmentService.getAllEquipments().subscribe({
      next: (data)=>{
        this.equipments = data;
      },
      error: (err) => {
        console.log("Failed to load equipment", err);
      }
    })
  }
}
