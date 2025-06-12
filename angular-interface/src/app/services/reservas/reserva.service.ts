import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Reserva } from '../../models/ReservaModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReservaService {

  private urlApi = 'https://somando-sabores-pi.onrender.com/api/Reserva';
  // private urlGetByEmail = this.urlApi + "/mail/";

  constructor(private http: HttpClient) { }

  create(reserva: Reserva): Observable<Reserva>{
    return this.http.post<Reserva>(this.urlApi, reserva);
  }

  // checkIfExistsByEmail(email: string): Observable<Reserva>{
  //   let params = new HttpParams();

  //   params = params.set('email', email);
  //   return this.http.get<Reserva>(this.urlGetByEmail, {params: params});
  // }
}
