import {Component, Inject, PLATFORM_ID} from '@angular/core';
import { EquipmentService } from '../../../services/equipment-services';
import {Equipment} from '../../../services/model-services';
import { RouterLink } from '@angular/router';
import {AsyncPipe, isPlatformBrowser, NgOptimizedImage} from '@angular/common';
import {map, Observable, of, throwError} from 'rxjs';
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
  rentedEquipments$: Observable<Equipment[]>;
  errorMessage: string | null = null;

  constructor(private equipmentService: EquipmentService, @Inject(PLATFORM_ID) platformId: Object) {
    const isBrowser = isPlatformBrowser(platformId);
    this.rentedEquipments$ = isBrowser ? this.equipmentService.getRentedEquipment() : of([]); // get rented equipments

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

      const role = localStorage.getItem('role')

      if (role == "Admin") {
        this.equipments$ = this.equipments$.pipe(
          map((data) => data.filter((d) => d.id != id))
        )
        this.equipmentService.deleteEquipment(id).subscribe({
          error: (err) => {
            console.error('Delete failed:', err);
          }
        })
      }else{
        alert("You don't have permission to delete the equipment");
      }
    }
  }
}
