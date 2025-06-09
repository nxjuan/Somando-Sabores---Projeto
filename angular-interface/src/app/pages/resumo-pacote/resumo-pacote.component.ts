import { Component, NgModule } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { MatIcon } from '@angular/material/icon';
import { RouterModule} from '@angular/router'; 
@Component({
  selector: 'app-resumo-pacote',
  imports: [HeaderBarComponent, RouterModule, MatIcon],
  standalone: true,
  templateUrl: './resumo-pacote.component.html',
  styleUrl: './resumo-pacote.component.scss'
})
export class ResumoPacoteComponent {
  precoFixo: number = 516.00;
  quantidade: number = 1;
  subtotal: number = this.precoFixo;
  total: number = this.subtotal;
  atualizarValores() {
    this.subtotal = this.quantidade * this.precoFixo;
    this.total = this.subtotal;
  }
  aumentarQuantidade() {
    this.quantidade++;
    this.atualizarValores();
  }
  diminuirQuantidade() {
    if (this.quantidade > 1) {
      this.quantidade--;
      this.atualizarValores();
    }
  }
  resetarQuantidade() {
    this.quantidade = 1;
    this.atualizarValores();
  }
  prosseguirParaPagamento() {
    //Colocar o bagulho do Asas!!
  }
}