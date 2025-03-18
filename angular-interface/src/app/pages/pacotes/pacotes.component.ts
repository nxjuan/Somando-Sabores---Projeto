import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';

@Component({
  standalone: true,
  selector: 'app-pacotes',
  imports: [HeaderBarComponent],
  templateUrl: './pacotes.component.html',
  styleUrl: './pacotes.component.scss'
})
export class PacotesComponent {

}
