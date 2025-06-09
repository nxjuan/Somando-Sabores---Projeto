import { Component } from '@angular/core';
import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-adm-relatorios',
  imports: [SideBarComponent, MatIconModule],
  templateUrl: './adm-relatorios.component.html',
  styleUrl: './adm-relatorios.component.scss'
})
export class AdmRelatoriosComponent {

}
