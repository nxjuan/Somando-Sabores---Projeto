import { MatIconModule } from '@angular/material/icon';
import { Component } from '@angular/core';

@Component({
  selector: 'app-registro-reserva',
  imports: [MatIconModule],
  templateUrl: './registro-reserva.component.html',
  styleUrl: './registro-reserva.component.scss'
})
export class RegistroReservaComponent {
  responsavel = "Michele Melo";
  email = "michele@email.com";
  qtd_convidados = 2;
  data_reserva = "22/05/2025";
}
