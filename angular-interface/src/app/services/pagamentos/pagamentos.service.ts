import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pagamento } from '../../models/PagamentoModel';
import { ServiceResponse } from '../../models/ServiceResponseModel';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PagamentosService {
  private urlApi = 'http://localhost:5000/api/Pagamento';

  constructor(private http: HttpClient) { }

  getAll(): Observable<ServiceResponse<Pagamento[]>> {
    return this.http.get<ServiceResponse<Pagamento[]>>(this.urlApi);
  }

}
