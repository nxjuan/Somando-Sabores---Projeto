import { Injectable } from '@angular/core';
import { Aluno } from '../../models/AlunoModel';
import { ServiceResponse } from '../../models/ServiceResponseModel';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AlunosService {
  private urlApi = 'http://localhost:5000/api/Aluno'

  constructor(private http: HttpClient) { }

  create(aluno: Aluno): Observable<ServiceResponse<Aluno>>{
    return this.http.post<ServiceResponse<Aluno>>(this.urlApi, aluno);
  }
}
