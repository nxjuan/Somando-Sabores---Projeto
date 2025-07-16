import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pacote } from '../../models/PacoteModel';
import { ServiceResponse } from '../../models/ServiceResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PacoteService {

  private urlApi = 'http://localhost:5000/api/Pacote';

  constructor(private http: HttpClient) { }

  create(pacote: Pacote): Observable<Pacote>{
    return this.http.post<Pacote>(this.urlApi, pacote);
  }

  getAll(): Observable<ServiceResponse<Pacote[]>> {
    return this.http.get<ServiceResponse<Pacote[]>>(this.urlApi);
  }

  getById(id: string): Observable<ServiceResponse<Pacote>> {
    return this.http.get<ServiceResponse<Pacote>>(`${this.urlApi}/${id}`);
  }

  update(pacote: Pacote): Observable<ServiceResponse<Pacote>> {
    return this.http.put<ServiceResponse<Pacote>>(this.urlApi, pacote);
  }

  delete(id: string): Observable<ServiceResponse<string>> {
    console.log('Chamando delete com ID:', id);
    return this.http.delete<ServiceResponse<string>>(`${this.urlApi}/${id}`);
  }
}
