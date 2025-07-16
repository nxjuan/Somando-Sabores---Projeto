import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { CommonModule } from '@angular/common';
import { RegistroPacoteComponent } from '../../components/registro-pacote/registro-pacote.component'
import { FilterResultsComponent } from '../../components/filter-results/filter-results.component';
import { Component, OnInit } from '@angular/core';
import { PacoteService } from '../../../services/pacote/pacote.service';
import { Pacote } from '../../../models/PacoteModel';
import { ServiceResponse } from '../../../models/ServiceResponseModel';

@Component({
  selector: 'app-adm-pacotes',
  imports: [SideBarComponent, RegistroPacoteComponent, FilterResultsComponent, CommonModule],
  templateUrl: './adm-pacotes.component.html',
  styleUrl: './adm-pacotes.component.scss'
})
export class AdmPacotesComponent implements OnInit{
  pacotes: Pacote[] = [];
  
  pacoteParaExcluirId: string | null = null;
  mostrarConfirmacaoExclusao: boolean = false;

  constructor(private pacoteService: PacoteService) {}

  ngOnInit(): void {
    this.carregarPacotes();
  }

  carregarPacotes(): void {
    this.pacoteService.getAll().subscribe(
      (response: ServiceResponse<Pacote[]>) => {
        if (response.success && response.data){
          this.pacotes = response.data;
        } else {
          console.error(`Erro na resposta da API: ${response.message}`);
          this.pacotes = [];
        }
      },
      error => {
        console.error(`Erro ao carregar pacotes: ${error}`);
        this.pacotes = [];
      }
    )
  }

  trackByPacoteId(index: number, pacote: Pacote): string {
    return pacote.idPacote ?? index.toString(); 
  }

  onPacoteAtualizado(pacoteAtualizado: Pacote): void {
    this.pacoteService.update(pacoteAtualizado).subscribe(
      response => {
        if (response.success){
          this.pacotes = this.pacotes.map(pacote =>
            pacote.idPacote === pacoteAtualizado.idPacote ? pacoteAtualizado : pacote
          );
        } else {
          console.error(`Erro ao atualizar pacote: ${response.message}`);
        }
      },
      error => console.error(error)
    );
  }

  onSolicitarExclusao(id: string): void {
    this.pacoteParaExcluirId = id;
    this.mostrarConfirmacaoExclusao = true;
  }

  confirmarExclusao(): void {
    if (!this.pacoteParaExcluirId) return;

    this.pacoteService.delete(this.pacoteParaExcluirId).subscribe(
      response => {
        this.pacotes = this.pacotes.filter(p => p.idPacote !== this.pacoteParaExcluirId);
        this.fecharConfirmacao();
        console.log('Pacote excluído com sucesso!')
      },
      error => {
        console.error(`Falha ao excluir pacote: ${error}`);
        this.fecharConfirmacao();
      }
    );
  }

  fecharConfirmacao(): void {
    this.pacoteParaExcluirId = null;
    this.mostrarConfirmacaoExclusao = false;
  }

}
