import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { RegistroPagamentoComponent } from '../../components/registro-pagamento/registro-pagamento.component'
import { FilterButtonComponent } from '../../components/filter-button/filter-button.component';
import { FilterComponent } from '../../components/filter/filter.component';
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-pagamentos',
  imports: [SideBarComponent, RegistroPagamentoComponent, FilterButtonComponent, FilterComponent],
  templateUrl: './adm-pagamentos.component.html',
  styleUrl: './adm-pagamentos.component.scss'
})
export class AdmPagamentosComponent {

}
