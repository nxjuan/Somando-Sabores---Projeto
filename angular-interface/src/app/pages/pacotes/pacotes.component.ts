import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-pacotes',
  imports: [HeaderBarComponent, RouterModule],
  templateUrl: './pacotes.component.html',
  styleUrl: './pacotes.component.scss'
})
export class PacotesComponent {

}
