import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { RouterModule, Router } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-pacotes',
  imports: [HeaderBarComponent, RouterModule, FormsModule, CommonModule],
  templateUrl: './pacotes.component.html',
  styleUrl: './pacotes.component.scss'
})
export class PacotesComponent {
  constructor(private router: Router) { }
  nome: string = '';
  email: string = '';
  ra: string = '';
  onSubmit(form: NgForm) {
    if (form.invalid) {
      Object.values(form.controls).forEach(control => control.markAsTouched());
    } else {
      this.router.navigate(['/resumo-pacote']);
    }
  }
}

