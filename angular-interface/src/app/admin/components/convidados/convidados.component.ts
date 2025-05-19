import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-convidados',
  imports: [MatIconModule],
  templateUrl: './convidados.component.html',
  styleUrl: './convidados.component.scss'
})
export class ConvidadosComponent {
  nome_convidado = "Tânia dos Santos"
}
