import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { FilterResultsComponent } from '../../components/filter-results/filter-results.component'
import { RegistroReservaV2Component } from '../../components/registro-reserva-v2/registro-reserva-v2.component'
import { MatIconModule } from '@angular/material/icon';
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-reservas',
  imports: [SideBarComponent, MatIconModule, FilterResultsComponent, RegistroReservaV2Component],
  templateUrl: './adm-reservas.component.html',
  styleUrl: './adm-reservas.component.scss'
})
export class AdmReservasComponent {

}
