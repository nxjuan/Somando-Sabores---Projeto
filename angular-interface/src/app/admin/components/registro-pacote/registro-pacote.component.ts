import { Component, Input, Output, EventEmitter } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { Pacote } from '../../../models/PacoteModel';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-registro-pacote',
  imports: [MatIconModule, CommonModule, FormsModule],
  templateUrl: './registro-pacote.component.html',
  styleUrl: './registro-pacote.component.scss'
})
export class RegistroPacoteComponent {
  @Input() pacote!: Pacote;
  localPacote!: Pacote; // Evita efeito colateral

  modoEdicao: boolean = false;

  @Output() atualizarPacote = new EventEmitter<Pacote>();
  @Output() deletarPacote = new EventEmitter<string>();

  constructor() {}

  ngOnInit(): void {
    this.localPacote = { ...this.pacote };
  }

  onEditarClick(): void {
    this.modoEdicao = !this.modoEdicao;
  }

  onSalvarClick(): void {
    this.atualizarPacote.emit(this.localPacote);
    this.modoEdicao = false;
  }

  onDeletarClick(): void {
    if (this.localPacote?.idPacote) {
      this.deletarPacote.emit(this.localPacote.idPacote);
    } else {
      console.warn('ID do pacote não disponível para exclusão.');
    }
  }
}
