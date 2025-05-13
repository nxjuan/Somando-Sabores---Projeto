import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { RegistroReservaComponent } from '../../components/registro-reserva/registro-reserva.component'
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-reservas',
  imports: [SideBarComponent, RegistroReservaComponent],
  templateUrl: './adm-reservas.component.html',
  styleUrl: './adm-reservas.component.scss'
})
export class AdmReservasComponent {

}
