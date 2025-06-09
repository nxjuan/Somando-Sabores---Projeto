import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { RegistroPagamentoComponent } from '../../components/registro-pagamento/registro-pagamento.component'
import { FilterResultsComponent } from '../../components/filter-results/filter-results.component';
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-pagamentos',
  imports: [SideBarComponent, RegistroPagamentoComponent, FilterResultsComponent],
  templateUrl: './adm-pagamentos.component.html',
  styleUrl: './adm-pagamentos.component.scss'
})
export class AdmPagamentosComponent {

}
