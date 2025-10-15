import {Component, OnInit} from '@angular/core';
import {EquipmentService} from '../../../services/equipment.services';
import {Equipment} from '../../../services/model.services';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-equipment-screen',
  imports: [
    RouterLink
  ],
  templateUrl: './equipment-screen.html',
  styleUrl: './equipment-screen.css'
})
export class EquipmentScreen implements OnInit{
  equipments: Equipment[] = []

  constructor(private equipmentService: EquipmentService) {}

  ngOnInit(): void {
    this.equipmentService.getAllEquipments().subscribe(data => {
      this.equipments.push(...data); // bind to the table
      console.log(this.equipments)
    });
  }
}
