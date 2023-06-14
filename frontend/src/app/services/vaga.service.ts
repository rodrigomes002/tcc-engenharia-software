import { Candidatura } from './../models/candidatura';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Filtros } from '../models/filter/filtros';
import { CadastrarVagaDTO, Vaga } from '../models/vaga';
import { environment } from '../../environments/environment'

@Injectable({
  providedIn: 'root',
})
export class VagaService {
  private url = environment.baseUrl +'/api/vaga';
  constructor(private http: HttpClient) {}

  cadastrar(usuario: CadastrarVagaDTO) {
    return this.http.post(`${this.url}/cadastrar`, usuario);
  }

  listarPorFiltro(filtro: any) {
    return this.http.post(`${this.url}/vagas`, filtro);
  }

  listarFiltros(): Observable<Filtros> {
    return this.http.get<Filtros>(`${this.url}/filtros`);
  }

  candidatarSe(candidatura: Candidatura) {
    return this.http.post(`${this.url}/candidatar`, candidatura);
  }
}
