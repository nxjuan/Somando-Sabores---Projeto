import { MatSidenavModule } from '@angular/material/sidenav'
import { MatToolbarModule } from '@angular/material/toolbar'
import { Router, RouterLink } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'app-side-bar',
  imports: [MatSidenavModule, MatToolbarModule, RouterLink],
  templateUrl: './side-bar.component.html',
  styleUrl: './side-bar.component.scss'
})
export class SideBarComponent {
  constructor(private route: Router) { }

  mudaPagina(rota: any){
    this.route.navigate([`/${rota}`])
  }
}
