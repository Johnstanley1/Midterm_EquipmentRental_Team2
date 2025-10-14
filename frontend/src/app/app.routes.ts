import { Routes } from '@angular/router';
import {LoginScreen} from './login-screen/login-screen';
import {ErrorScreen} from './error-screen/error-screen';
import {HomeScreen} from './home-screen/home-screen';
import {EquipmentScreen} from './equipment-screen/equipment-screen';

export const routes: Routes = [
  {path: "login", component: LoginScreen},
  {path: "home", component: HomeScreen},
  {path: "manage-equipment", component: EquipmentScreen},
  {path: "error", component: ErrorScreen},
  {path: "", redirectTo: "/login", pathMatch: "full"}
];
