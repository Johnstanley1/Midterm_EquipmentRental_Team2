import {Component, Inject, PLATFORM_ID} from '@angular/core';
import {EquipmentService} from '../../../services/equipment.services';
import {ActivatedRoute, RouterLink} from '@angular/router';
import {Equipment} from '../../../services/model.services';
import {of} from 'rxjs';
import {AsyncPipe, isPlatformBrowser, NgOptimizedImage} from '@angular/common';
import {catchError} from 'rxjs/operators';

@Component({
  selector: 'app-equipment-detail-screen',
  imports: [
    AsyncPipe,
    NgOptimizedImage,
    RouterLink,
  ],
  templateUrl: './equipment-detail-screen.html',
  styleUrl: './equipment-detail-screen.css'
})
export class EquipmentDetailScreen {
  equipmentId!: number;
  equipment$ = of<Equipment | null>(null);
  errorMessage: string | null = null;

  constructor( private equipmentService: EquipmentService,
               private route: ActivatedRoute,
               @Inject(PLATFORM_ID) private platformId: Object) {
  }

  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      this.equipmentId = Number(this.route.snapshot.paramMap.get('id'));
      this.equipment$ = this.equipmentService.getEquipmentById(this.equipmentId).pipe(
        catchError(err => {
          this.errorMessage = 'Failed to load equipment details';
          console.error(err);
          return of(null);
        })
      );
    }
  }
}
