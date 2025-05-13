import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { RegistroPacoteComponent } from '../../components/registro-pacote/registro-pacote.component'
import { Component } from '@angular/core';

@Component({
  selector: 'app-adm-pacotes',
  imports: [SideBarComponent, RegistroPacoteComponent],
  templateUrl: './adm-pacotes.component.html',
  styleUrl: './adm-pacotes.component.scss'
})
export class AdmPacotesComponent {

}
