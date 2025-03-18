import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';

@Component({
  standalone: true,
  selector: 'app-cardapio',
  imports: [HeaderBarComponent],
  templateUrl: './cardapio.component.html',
  styleUrl: './cardapio.component.scss'
})
export class CardapioComponent {

}
