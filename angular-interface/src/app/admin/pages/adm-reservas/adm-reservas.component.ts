import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { RegistroReservaComponent } from '../../components/registro-reserva/registro-reserva.component'
import { FilterButtonComponent } from '../../components/filter-button/filter-button.component'
import { FilterComponent } from '../../components/filter/filter.component'
import { ConvidadosComponent } from '../../components/convidados/convidados.component'
import { MatIconModule } from '@angular/material/icon';
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-reservas',
  imports: [SideBarComponent, RegistroReservaComponent, MatIconModule, FilterButtonComponent, FilterComponent, ConvidadosComponent],
  templateUrl: './adm-reservas.component.html',
  styleUrl: './adm-reservas.component.scss'
})
export class AdmReservasComponent {

}
