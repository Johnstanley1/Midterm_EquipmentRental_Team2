import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';

export class Equipment {
  id: number | undefined
  name: string
  description: string
  isAvailable: boolean
  status: string
  category: string
  condition: string

  constructor(name: string, description: string, isAvailable: boolean,
              status: string, category: string, condition: string) {
    this.name = name;
    this.description = description;
    this.isAvailable = isAvailable;
    this.status = status;
    this.category = category;
    this.condition = condition;
  }
}


@Injectable({
  providedIn: 'root'
})

export class EquipmentService {

  constructor(private http: HttpClient) {}

  private baseUrl = 'https://localhost:7024/api/Equipment'
  // private baseUrl = '/api/Equipment';


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


  // create new equipment
  createEquipment(equipment: Equipment ): Observable<Equipment>{
    return this.http.post<Equipment>(this.baseUrl, equipment)
  }


  // modify existing equipment
  updateEquipment(id: number, equipment: Equipment): Observable<void>{
    return this.http.put<void>(`${this.baseUrl}/${id}`, equipment)
  }


  // delete existing equipment
  deleteEquipment(id: number): Observable<void>{
    return this.http.delete<void>(`${this.baseUrl}/${id}`)
  }
}
