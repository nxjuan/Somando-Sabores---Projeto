import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { CommonModule } from '@angular/common';
import { FilterResultsComponent } from '../../components/filter-results/filter-results.component'
import { RegistroReservaV2Component } from '../../components/registro-reserva-v2/registro-reserva-v2.component'
import { MatIconModule } from '@angular/material/icon';
import { Component, OnInit } from '@angular/core';
import { Reserva } from '../../../models/ReservaModel';
import { ReservaService } from '../../../services/reservas/reserva.service';
import { ServiceResponse } from '../../../models/ServiceResponseModel';

@Component({
  selector: 'app-adm-reservas',
  imports: [SideBarComponent, MatIconModule, FilterResultsComponent, RegistroReservaV2Component, CommonModule],
  templateUrl: './adm-reservas.component.html',
  styleUrl: './adm-reservas.component.scss'
})
export class AdmReservasComponent implements OnInit{
  reservas: Reserva[] = [];
    
  reservaParaExcluirId: string | null = null;
  mostrarConfirmacaoExclusao: boolean = false;

  constructor(private reservaService: ReservaService) {}

  ngOnInit(): void {
    this.carregarReservas();
  }

  carregarReservas(): void {
    this.reservaService.getAll().subscribe(
      (response: ServiceResponse<Reserva[]>) => {
        if (response.success && response.data){
          this.reservas = response.data;
          this.reservasFiltradas = this.reservas;
        } else {
          console.error(`Erro na resposta da API: ${response.message}`);
          this.reservas = [];
          this.reservasFiltradas = this.reservas;
        }
      },
      error => {
        console.error(`Erro ao carregar reservas: ${error}`);
        this.reservas = [];
        this.reservasFiltradas = this.reservas;
      }
    )
  }

  trackByReservaId(index: number, reserva: Reserva): string {
    return reserva.id ?? index.toString(); 
  }

  onReservaAtualizada(reservaAtualizada: Reserva): void {
    this.reservaService.update(reservaAtualizada).subscribe(
      response => {
        if (response.success){
          this.reservas = this.reservas.map(reserva =>
            reserva.id === reservaAtualizada.id ? reservaAtualizada : reserva
          );
        } else {
          console.error(`Erro ao atualizar reserva: ${response.message}`);
        }
      },
      error => console.error(error)
    );
  }

  onSolicitarExclusao(id: string): void {
    this.reservaParaExcluirId = id;
    this.mostrarConfirmacaoExclusao = true;
  }

  confirmarExclusao(): void {
    if (!this.reservaParaExcluirId) return;

    this.reservaService.delete(this.reservaParaExcluirId).subscribe(
      response => {
        this.reservas = this.reservas.filter(r => r.id !== this.reservaParaExcluirId);
        this.fecharConfirmacao();
        console.log('Reserva excluída com sucesso!')
      },
      error => {
        console.error(error);
        this.fecharConfirmacao();
      }
    );
  }

  fecharConfirmacao(): void {
    this.reservaParaExcluirId = null;
    this.mostrarConfirmacaoExclusao = false;
  }
  
  reservasFiltradas: Reserva[] = this.reservas;

  aplicarFiltro(filtro: { nome: string; data: string }) {
    this.reservasFiltradas = this.reservas.filter(p =>
      (!filtro.nome || p.nome?.toLowerCase().includes(filtro.nome.toLowerCase())) &&
      (!filtro.data || p.dataReserva.includes(filtro.data))
    );
  }

  foo(): void {
    const data_csv = this.reservasFiltradas.map(r => [r.nome, r.dataReserva, r.quantidade, r.nomesConvidados.toString().replaceAll(',', '; ')]);
    data_csv.splice(0, 0, ['Responsável', 'Data da Reserva', 'Quantidade', 'Nome dos Convidados']);
    const csvContent = data_csv.map(e => e.join(',')).join('\n');
    const blob = new Blob([csvContent], { type: 'text/csv;charset=utf-8;' });
    const url = window.URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.setAttribute('href', url);
    link.setAttribute('download', 'reservas.csv');
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

}
