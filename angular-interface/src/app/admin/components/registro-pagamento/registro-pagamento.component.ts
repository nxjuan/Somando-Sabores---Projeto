import { Component } from '@angular/core';

@Component({
  selector: 'app-registro-pagamento',
  imports: [],
  templateUrl: './registro-pagamento.component.html',
  styleUrl: './registro-pagamento.component.scss'
})
export class RegistroPagamentoComponent {
  id_pagamento = "1234-12345-123456-12345-1234";
  nome_pagamento = "Antônio dos Santos";
  valor_pagamento = "119,90";
  data_pagamento = "24/04/2025";
}
