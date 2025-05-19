import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-registro-pacote',
  imports: [MatIconModule],
  templateUrl: './registro-pacote.component.html',
  styleUrl: './registro-pacote.component.scss'
})
export class RegistroPacoteComponent {
  aluno="José Silva";
  email="jose_silva@email.com";
  ra="171717";
  qtd_pacotes="1";
}
