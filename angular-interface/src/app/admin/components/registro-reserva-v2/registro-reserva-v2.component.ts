import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon'
import { MatExpansionModule } from '@angular/material/expansion'

@Component({
  selector: 'app-registro-reserva-v2',
  imports: [MatExpansionModule, MatIconModule],
  templateUrl: './registro-reserva-v2.component.html',
  styleUrl: './registro-reserva-v2.component.scss'
})
export class RegistroReservaV2Component {
  responsavel = 'João dos Santos';
  email = 'joao@email.com';
  qtd_convidados = 2;
  data_reserva = '26/04/2025';

  nome_convidado = 'Maria de Lurdes';
}
