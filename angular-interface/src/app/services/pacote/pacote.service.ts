import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Pacote } from '../../models/PacoteModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PacoteService {

  private urlApi = 'https://somando-sabores-pi.onrender.com/api/Pacote';

  constructor(private http: HttpClient) { }

  create(pacote: Pacote): Observable<Pacote>{
    return this.http.post<Pacote>(this.urlApi, pacote);
  }

}
