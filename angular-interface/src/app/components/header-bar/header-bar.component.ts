import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar'
import { MatButtonModule } from '@angular/material/button'
import { MatIconModule } from '@angular/material/icon';
import { Router, RouterLink } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-header-bar',
  imports: [MatButtonModule, MatToolbarModule, MatIconModule, RouterLink],
  templateUrl: './header-bar.component.html',
  styleUrl: './header-bar.component.scss'
})
export class HeaderBarComponent {
  imageUrl:string = "src/assets/images/Logo-3.png";

  constructor(private route: Router) { }

  mudaPagina(rota: any){
    this.route.navigate([`/${rota}`])
  }
}
