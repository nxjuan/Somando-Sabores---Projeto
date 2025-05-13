import { Component } from '@angular/core';
import { SideBarComponent } from '../../components/side-bar/side-bar.component'
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-admin-panel',
  imports: [SideBarComponent, MatIconModule],
  templateUrl: './admin-panel.component.html',
  styleUrl: './admin-panel.component.scss'
})
export class AdminPanelComponent {

}
