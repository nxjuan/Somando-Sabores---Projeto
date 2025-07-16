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
        } else {
          console.error(`Erro na resposta da API: ${response.message}`);
          this.reservas = [];
        }
      },
      error => {
        console.error(`Erro ao carregar reservas: ${error}`);
        this.reservas = [];
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
        console.error(`Falha ao excluir reserva: ${error}`);
        this.fecharConfirmacao();
      }
    );
  }

  fecharConfirmacao(): void {
    this.reservaParaExcluirId = null;
    this.mostrarConfirmacaoExclusao = false;
  }
  
}
