import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';// ajuste o caminho se necessário

@Component({
  selector: 'app-confirmacao',
  standalone: true,
  imports: [HeaderBarComponent], // Adicione aqui
  templateUrl: './confirmacao.component.html',
  styleUrls: ['./confirmacao.component.scss']
})
export class ConfirmacaoComponent {

}
 