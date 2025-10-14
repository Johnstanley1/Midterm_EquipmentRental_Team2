import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class EquipmentServices {
  constructor() {}
}

export class Equipment {
  id: number;
  name: string
  description: string
  isAvailable: boolean
  status: string
  category: string
  condition: string

  constructor(id: number, name: string, description: string, isAvailable: boolean,
              status: string, category: string, condition: string, private http: HttpClient) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.isAvailable = isAvailable;
    this.status = status;
    this.category = category;
    this.condition = condition;
  }

  private baseUrl = 'https://localhost:7024/api/Equipment'

  // get all equipments
  getAllEquipments(): Observable<Equipment[]>{
    return this.http.get<Equipment[]>(this.baseUrl);
  }


  // get equipment by id
  getEquipmentById(id: number): Observable<Equipment>{
    return this.http.get<Equipment>(`${this.baseUrl}/${id}`)
  }


  // get all available equipment
  GetAvailableEquipment(): Observable<Equipment[]>{
    return this.http.get<Equipment[]>(`${this.baseUrl}/available`)
  }


  // get all rented equipment
  getRentedEquipments(): Observable<Equipment[]>{
    return this.http.get<Equipment[]>(`${this.baseUrl}/rented`)
  }


  createEquipment(equipment: Equipment ): Observable<Equipment>{
    return this.http.post<Equipment>(this.baseUrl, equipment)
  }


  updateEquipment(id: number, equipment: Equipment): Observable<void>{
    return this.http.put<void>(`${this.baseUrl}/${id}`, equipment)
  }


  deleteEquipment(id: number): Observable<void>{
    return this.http.delete<void>(`${this.baseUrl}/${id}`)
  }
}
