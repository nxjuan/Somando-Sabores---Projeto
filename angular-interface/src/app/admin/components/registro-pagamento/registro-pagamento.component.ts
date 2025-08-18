import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Pagamento } from '../../../models/PagamentoModel';

@Component({
  selector: 'app-registro-pagamento',
  imports: [CommonModule],
  templateUrl: './registro-pagamento.component.html',
  styleUrl: './registro-pagamento.component.scss'
})
export class RegistroPagamentoComponent {
  @Input() pagamento!: Pagamento;
  localPagamento!: Pagamento;

  constructor() {}

  ngOnInit(): void {
    this.localPagamento = { ...this.pagamento };
  }
}
