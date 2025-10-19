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

export interface CustomerDto {
  id: number;
  name: string;
  username: string;
  role: string;
  isActive: boolean;
  rentals?: Array<{
    id: number;
    equipmentId: number;
    equipmentName: string;
    equipmentStatus: string;
    issuedAt: string;
    dueDate: string;
    returnedAt?: string | null;
    status: string;
  }>; // optional in list view
}
