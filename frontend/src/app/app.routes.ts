import { Routes } from '@angular/router';
import { LoginScreen } from './login-screen/login-screen';
import { ErrorScreen } from './error-screen/error-screen';
import { HomeScreen } from './home-screen/home-screen';
import { EquipmentScreen } from './equipment-screen/equipment-screen';
import { CreateEquipmentScreen } from './create-equipment-screen/create-equipment-screen';
import { CustomerScreen } from './customer-screen/customer-screen';
import { RentalsScreen } from './rentals-screen/rentals-screen';
import { RentalDetailScreen } from './rental-detail-screen/rental-detail-screen';
import { RentalIssueScreen } from './rental-issue-screen/rental-issue-screen';
import { RentalReturnScreen } from './rental-return-screen/rental-return-screen';
import { OverdueScreen } from './overdue-screen/overdue-screen';
import { AvailableScreen } from './available-screen/available-screen';
import { CreateCustomerScreen } from './create-customer-screen/create-customer-screen';
import { EditCustomerScreen } from './edit-customer-screen/edit-customer-screen';
import { CustomerDetailScreen } from './customer-detail-screen/customer-detail-screen';

export const routes: Routes = [
  { path: 'login', component: LoginScreen },
  { path: 'home', component: HomeScreen },
  { path: 'manage-equipment', component: EquipmentScreen },
  { path: 'add-equipment', component: CreateEquipmentScreen },
  { path: 'view-customers', component: CustomerScreen },
  { path: 'add-customer', component: CreateCustomerScreen },
  { path: 'edit-customer/:id', component: EditCustomerScreen },
  { path: 'customer-detail/:id', component: CustomerDetailScreen },
  { path: 'all-rentals', component: RentalsScreen },
  { path: 'rental-detail/:id', component: RentalDetailScreen },
  { path: 'rental-issue', component: RentalIssueScreen },
  { path: 'rental-return/:id', component: RentalReturnScreen },
  { path: 'over-due', component: OverdueScreen },
  { path: 'available', component: AvailableScreen },
  { path: 'error', component: ErrorScreen },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];
