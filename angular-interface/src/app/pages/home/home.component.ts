import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { CauroselComponent } from '../../components/caurosel/caurosel.component';
import { RouterModule } from '@angular/router';
@Component({
  standalone: true,
  selector: 'app-home',
  imports: [HeaderBarComponent, CauroselComponent, RouterModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  anoAtual: number = new Date().getFullYear();
}
 