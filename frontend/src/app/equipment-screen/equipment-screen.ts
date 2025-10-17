import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { EquipmentService } from '../../../services/equipment-services';
import {CustomerDto, Equipment} from '../../../services/model-services';
import { RouterLink } from '@angular/router';
import { AsyncPipe, isPlatformBrowser } from '@angular/common';
import {map, Observable, of} from 'rxjs';
import {catchError} from 'rxjs/operators';

@Component({
  selector: 'app-equipment-screen',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './equipment-screen.html',
  styleUrl: './equipment-screen.css',
})
export class EquipmentScreen {
  // Use observable + async pipe to ensure updates render in zoneless mode
  equipments$: Observable<Equipment[]>;
  errorMessage: string | null = null;

  constructor(private equipmentService: EquipmentService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.equipments$ = isBrowser ? this.equipmentService.getAllEquipments() : of([]);

    this.equipments$ = isBrowser
      ? this.equipmentService.getAllEquipments().pipe(
        catchError((err) => {
          // Friendly message for permission issues or others
          if (err?.status === 403) {
            this.errorMessage = 'You do not have permission to view customers.';
          } else if (err?.status === 401) {
            this.errorMessage = 'Your session has expired. Please log in again.';
          } else {
            this.errorMessage = 'Failed to load customers. Please try again.';
          }
          return of([] as Equipment[]);
        })
      )
      : of([] as Equipment[]);
  }


  btnDelete_click(id: number){
    if (confirm('Are you sure you want to delete this equipment?')){

      this.equipments$ = this.equipments$.pipe(
        map((equipments) => equipments.filter((e) => e.id !== id))
      );

      this.equipmentService.deleteEquipment(id).subscribe({
        // Refresh the list after deletion
        next:(data) =>{
          this.equipments$ = this.equipments$.pipe(
            map((data) => data.filter((d) => d.id != id))
          )
        },
        error:(err) => {
          if (err.status === 404) {
            this.errorMessage = 'Equipment not found.';
          } else if (err.status === 403) {
            this.errorMessage = 'You do not have permission to delete equipment.';
          } else {
            this.errorMessage = 'Failed to delete equipment. Please try again.';
          }
        }
      })
    }
  }
}
