import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ModelService {
  constructor() {}
}

export class Equipment {
  id: number | undefined;
  name: string;
  description: string;
  isAvailable: boolean;
  status: string;
  category: string;
  condition: string;

  constructor(
    name: string,
    description: string,
    isAvailable: boolean,
    status: string,
    category: string,
    condition: string
  ) {
    this.name = name;
    this.description = description;
    this.isAvailable = isAvailable;
    this.status = status;
    this.category = category;
    this.condition = condition;
  }
}

export class Customer {
  id?: number;
  name: string;
  username: string;
  password: string;
  role: string; // "Admin" or "User"
  isActive: boolean;

  constructor(name: string, username: string, password: string, role: string, isActive: boolean) {
    this.name = name;
    this.username = username;
    this.password = password;
    this.role = role;
    this.isActive = isActive;
  }
}


export class CustomerDTO {
  id?: number;
  name: string;
  username: string;
  role: string;
  isActive: boolean;
  rentals: []

  constructor(name: string, username: string, role: string, isActive: boolean, rentals: []){
    this.name = name;
    this.username = username;
    this.role = role;
    this.isActive = isActive;
    this.rentals = rentals
  }
}


export class RentalDTO{
  id?: number;
  issuedAt: Date;
  dueDate: Date;
  returnedAt: Date
  returnNotes: string
  equipmentId: number;
  status: string;
  equipmentName: string;
  equipmentStatus: string;
  equipmentCondition: string
  equipment: Equipment
  customerId: number
  customerName: string;

  constructor(issuedAt: Date, dueDate: Date, returnedAt: Date, returnNotes: string, equipmentId: number, status: string, equipmentName: string, equipmentStatus: string, equipmentCondition: string, equipment: Equipment, customerId: number, customerName: string) {
    this.issuedAt = issuedAt;
    this.dueDate = dueDate;
    this.returnedAt = returnedAt;
    this.returnNotes = returnNotes;
    this.equipmentId = equipmentId;
    this.status = status;
    this.equipmentName = equipmentName;
    this.equipmentStatus = equipmentStatus;
    this.equipmentCondition = equipmentCondition;
    this.equipment = equipment;
    this.customerId = customerId;
    this.customerName = customerName;
  }

}
