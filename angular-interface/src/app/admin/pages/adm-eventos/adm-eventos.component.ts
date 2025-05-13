import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { RegistroEventoComponent } from '../../components/registro-evento/registro-evento.component'
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-eventos',
  imports: [SideBarComponent, RegistroEventoComponent],
  templateUrl: './adm-eventos.component.html',
  styleUrl: './adm-eventos.component.scss'
})
export class AdmEventosComponent {

}
