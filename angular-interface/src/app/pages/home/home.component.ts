import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { CauroselComponent } from '../../components/caurosel/caurosel.component';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [HeaderBarComponent, CauroselComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

}
