import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Reserva } from '../../models/ReservaModel';
import { ServiceResponse } from '../../models/ServiceResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservaService {

  private urlApi = 'http://localhost:5000/api/Reserva';

  constructor(private http: HttpClient) { }

  create(reserva: Reserva): Observable<Reserva>{
    return this.http.post<Reserva>(this.urlApi, reserva);
  }

  getAll(): Observable<ServiceResponse<Reserva[]>> {
    return this.http.get<ServiceResponse<Reserva[]>>(this.urlApi);
  }

  getById(id: string): Observable<ServiceResponse<Reserva>> {
    return this.http.get<ServiceResponse<Reserva>>(`${this.urlApi}/${id}`);
  }

  update(reserva: Reserva): Observable<ServiceResponse<Reserva>> {
    return this.http.put<ServiceResponse<Reserva>>(this.urlApi, reserva);
  }

  delete(id: string): Observable<ServiceResponse<string>> {
    return this.http.delete<ServiceResponse<string>>(`${this.urlApi}/${id}`);
  }
}
