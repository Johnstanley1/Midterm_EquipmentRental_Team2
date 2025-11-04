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
  rentals: RentalDTO[]

  constructor(name: string, username: string, role: string, isActive: boolean, rentals: RentalDTO[]){
    this.name = name;
    this.username = username;
    this.role = role;
    this.isActive = isActive;
    this.rentals = rentals
  }
}


export class RentalDTO{
  id!: number;
  issuedAt: Date;
  dueDate: Date;
  returnedAt: Date;
  returnNotes: string;
  customerId: number = 0;
  equipmentId: number = 0;
  equipmentCondition: string
  equipmentStatus: string;
  status: string;
  equipmentName?: string;
  equipment?: Equipment
  customerName?: string;

  constructor(equipmentName: string, customerName: string,  issuedAt: Date, dueDate: Date, returnedAt: Date, returnNotes: string, customerId: number,
              equipmentId: number, equipmentCondition: string, equipmentStatus: string, status: string, equipment: Equipment) {
    this.equipmentName = equipmentName;
    this.customerName = customerName;
    this.issuedAt = issuedAt;
    this.dueDate = dueDate;
    this.returnedAt = returnedAt;
    this.returnNotes = returnNotes;
    this.customerId = customerId;
    this.equipmentId = equipmentId;
    this.equipmentCondition = equipmentCondition;
    this.equipmentStatus = equipmentStatus;
    this.status = status;
    this.equipment = equipment;
  }
}
