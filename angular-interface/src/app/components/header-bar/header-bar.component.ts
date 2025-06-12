import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Router,} from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-header-bar',

  imports: [CommonModule, MatButtonModule, MatToolbarModule, MatIconModule],
  templateUrl: './header-bar.component.html',
  styleUrl: './header-bar.component.scss'
})
export class HeaderBarComponent {
  isMenuOpen = false;

  constructor(private route: Router) { }

  
  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
  }

  closeMenuAndNavigate(rota: any): void {
    this.isMenuOpen = false;
    this.route.navigate([`/${rota}`]);
  }
}
