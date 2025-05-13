import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { RegistroPagamentoComponent } from '../../components/registro-pagamento/registro-pagamento.component'
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-pagamentos',
  imports: [SideBarComponent, RegistroPagamentoComponent],
  templateUrl: './adm-pagamentos.component.html',
  styleUrl: './adm-pagamentos.component.scss'
})
export class AdmPagamentosComponent {

}
