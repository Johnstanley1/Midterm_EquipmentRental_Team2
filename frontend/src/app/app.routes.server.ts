import { RenderMode, ServerRoute } from '@angular/ssr';

export const serverRoutes: ServerRoute[] = [
  { path: 'edit-customer/:id', renderMode: RenderMode.Client },
  { path: 'customer-detail/:id', renderMode: RenderMode.Client },
  { path: 'edit-equipment/:id', renderMode: RenderMode.Client },
  { path: 'equipment-detail/:id', renderMode: RenderMode.Client },
  { path: 'overdue-rentals', renderMode: RenderMode.Client },
  { path: '**', renderMode: RenderMode.Prerender },
];
