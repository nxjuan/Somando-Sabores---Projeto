import { Injectable } from '@angular/core';
import { Aluno } from '../../models/AlunoModel';
import { ServiceResponse } from '../../models/ServiceResponseModel';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AlunosService {
  private urlApi = 'https://somando-sabores-pi.onrender.com/api/Aluno';

  private idAluno = new BehaviorSubject<string | undefined>(undefined);
  currentIdAluno: Observable<string | undefined> = this.idAluno.asObservable();

  constructor(private http: HttpClient) { }

  create(aluno: Aluno): Observable<ServiceResponse<Aluno>>{
    return this.http.post<ServiceResponse<Aluno>>(this.urlApi, aluno);
  }

  setIdAluno(id: string | undefined): void {
    this.idAluno.next(id);
  }

}
