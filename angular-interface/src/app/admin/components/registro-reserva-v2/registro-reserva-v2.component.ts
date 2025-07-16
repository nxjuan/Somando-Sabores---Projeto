import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatIconModule } from '@angular/material/icon'
import { Reserva } from '../../../models/ReservaModel';
import { MatExpansionModule } from '@angular/material/expansion'
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registro-reserva-v2',
  imports: [MatExpansionModule, MatIconModule, CommonModule, FormsModule],
  templateUrl: './registro-reserva-v2.component.html',
  styleUrl: './registro-reserva-v2.component.scss'
})
export class RegistroReservaV2Component {
  @Input() reserva!: Reserva;
  localReserva!: Reserva; 

  modoEdicao: boolean = false;

  @Output() atualizarReserva = new EventEmitter<Reserva>();
  @Output() deletarReserva = new EventEmitter<string>();

  constructor() {}

  ngOnInit(): void {
    this.localReserva = { ...this.reserva };
  }

  onEditarClick(): void {
    this.modoEdicao = !this.modoEdicao;
  }

  onSalvarClick(): void {
    this.atualizarReserva.emit(this.localReserva);
    this.modoEdicao = false;
  }

  onDeletarClick(): void {
    if (this.localReserva?.id) {
      this.deletarReserva.emit(this.localReserva.id);
    } else {
      console.warn('ID da reserva não disponível para exclusão.');
    }
  }
}
