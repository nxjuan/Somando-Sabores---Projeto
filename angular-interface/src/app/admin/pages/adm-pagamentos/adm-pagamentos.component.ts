import { SideBarComponent } from '../../components/side-bar/side-bar.component';
import { CommonModule } from '@angular/common';
import { RegistroPagamentoComponent } from '../../components/registro-pagamento/registro-pagamento.component';
import { FilterResultsComponent } from '../../components/filter-results/filter-results.component';
import { Component, OnInit } from '@angular/core';
import { ServiceResponse } from '../../../models/ServiceResponseModel';
import { Pagamento } from '../../../models/PagamentoModel';
import { PagamentosService } from '../../../services/pagamentos/pagamentos.service';

@Component({
  selector: 'app-adm-pagamentos',
  imports: [SideBarComponent, RegistroPagamentoComponent, FilterResultsComponent, CommonModule],
  templateUrl: './adm-pagamentos.component.html',
  styleUrl: './adm-pagamentos.component.scss'
})
export class AdmPagamentosComponent implements OnInit{
  pagamentos: Pagamento[] = [];

  constructor(private pagamentoService: PagamentosService) {}

  ngOnInit(): void {
    this.carregarPagamentos();
  }

  carregarPagamentos(): void {
    this.pagamentoService.getAll().subscribe(
      (response: ServiceResponse<Pagamento[]>) => {
        if (response.success && response.data){
          this.pagamentos = response.data;
          this.pagamentosFiltrados = this.pagamentos;
        } else {
          console.error(`Erro na resposta da API: ${response.message}`);
          this.pagamentos = [];
          this.pagamentosFiltrados = this.pagamentos;
        }
      },
      error => {
        console.error(`Erro ao carregar pagamentos: ${error}`)
        this.pagamentos = [];
        this.pagamentosFiltrados = this.pagamentos;
      }
    )
  }

  trackByPagamentoId(index: number, pagamento: Pagamento): string {
    return pagamento.id ?? index.toString(); 
  }

  pagamentosFiltrados: Pagamento[] = this.pagamentos;

  aplicarFiltro(filtro: { nome: string; data: string }) {
    this.pagamentosFiltrados = this.pagamentos.filter(p =>
      (!filtro.nome || p.nome?.toLowerCase().includes(filtro.nome.toLowerCase())) &&
      (!filtro.data || p.dataPagamento.includes(filtro.data))
    );
  }
}
