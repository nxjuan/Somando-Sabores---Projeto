import { Component, Output, EventEmitter } from '@angular/core';
import { MatIcon } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-filter-results',
  imports: [MatIcon, MatExpansionModule, FormsModule],
  templateUrl: './filter-results.component.html',
  styleUrl: './filter-results.component.scss'
})
export class FilterResultsComponent {
  nome: string = '';
  data: string = '';

  @Output() filtroChange = new EventEmitter<{ nome: string; data: string }>();

  emitirFiltro() {
    this.filtroChange.emit({ nome: this.nome, data: this.data });
  }
}
