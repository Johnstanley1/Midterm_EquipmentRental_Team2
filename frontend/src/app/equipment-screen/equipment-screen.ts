import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { EquipmentService } from '../../../services/equipment.services';
import { Equipment } from '../../../services/model.services';
import { RouterLink } from '@angular/router';
import { AsyncPipe, isPlatformBrowser } from '@angular/common';
import { Observable, of } from 'rxjs';

@Component({
  selector: 'app-equipment-screen',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './equipment-screen.html',
  styleUrl: './equipment-screen.css',
})
export class EquipmentScreen {
  // Use observable + async pipe to ensure updates render in zoneless mode
  equipments$: Observable<Equipment[]>;

  constructor(private equipmentService: EquipmentService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.equipments$ = isBrowser ? this.equipmentService.getAllEquipments() : of([]);
  }
}
