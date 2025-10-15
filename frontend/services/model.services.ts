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

export interface Customer {
  id?: number;
  name: string;
  username: string;
  password: string;
  role: string; // "Admin" or "User"
  isActive: boolean;
}
