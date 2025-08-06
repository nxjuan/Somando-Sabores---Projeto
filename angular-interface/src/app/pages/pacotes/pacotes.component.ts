import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { RouterModule, Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, NgForm, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Aluno } from '../../models/AlunoModel';
import { AlunosService } from '../../services/alunos/alunos.service';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';

@Component({
  standalone: true,
  selector: 'app-pacotes',
  imports: [
    // Imports que você já tinha
    HeaderBarComponent,
    RouterModule,
    FormsModule,
    CommonModule,
    // NOVO: Adicione os módulos aqui
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatCheckboxModule,
    MatButtonModule
  ],
  templateUrl: './pacotes.component.html',
  styleUrl: './pacotes.component.scss'
})
export class PacotesComponent {

  id: string | undefined;
  nome: string = "";
  email: string = "";
  ra: string = "";

  range = new FormGroup({
    start: new FormControl<Date | null>(null),
    end: new FormControl<Date | null>(null),
  });

  incluirJantar: boolean = false;
  precoTotal: number = 0;
  diasUteisContados: number = 0;

  constructor(
    private router: Router,
    private alunosService: AlunosService
    // private pagamentoService: PagamentoService // Você vai precisar de um serviço para o Asaas
  ) { }

  // NOVO: Função para o filtro de datas do calendário
  dateFilter = (d: Date | null): boolean => {
    const day = (d || new Date()).getDay();
    // Bloqueia apenas os domingos (onde o dia da semana é 0)
    return day !== 0;
  }

  // NOVO: Função principal para calcular o preço
  calcularPreco(): void {
    const start = this.range.value.start;
    const end = this.range.value.end;

    if (!start || !end) {
      this.precoTotal = 0;
      this.diasUteisContados = 0;
      return;
    }

    let diasValidos = 0;
    let dataCorrente = new Date(start);

    while (dataCorrente <= end) {
      if (dataCorrente.getDay() !== 0) { // Se não for domingo
        diasValidos++;
      }
      dataCorrente.setDate(dataCorrente.getDate() + 1);
    }

    this.diasUteisContados = diasValidos;

    const precoAlmoco = 21.50;
    const precoJantar = 18.90;

    let subtotalAlmoco = diasValidos * precoAlmoco;
    let subtotalJantar = 0;

    if (this.incluirJantar) {
      subtotalJantar = diasValidos * precoJantar;
    }

    this.precoTotal = subtotalAlmoco + subtotalJantar;
  }

  onSubmit(form: NgForm) {

    if (form.invalid || !this.range.valid || this.precoTotal <= 0) {
      alert("Por favor, preencha todos os dados e selecione um período válido.");
      Object.values(form.controls).forEach(control => control.markAsTouched());
      return;
    }

    // 1. Salva os dados do aluno/pacote no seu backend
    const dadosPacote = {
      nome: this.nome,
      email: this.email,
      ra: this.ra,
      dataInicio: this.range.value.start,
      dataFim: this.range.value.end,
      incluiJantar: this.incluirJantar,
      diasContratados: this.diasUteisContados,
      valorTotal: this.precoTotal,
    };

    if (form.invalid) {
      Object.values(form.controls).forEach(control => control.markAsTouched());
    } else {
      const dadosAluno: Aluno = form.value;
      //console.log("Dados do aluno a serem enviados: ", dadosAluno);
      this.alunosService.create(dadosAluno).subscribe({
        next: (response) => {

          this.alunosService.setIdAluno(response.data?.id);

          if (response.message == "Aluno já cadastrado") {
            alert(`Bem-vindo(a) de volta, ${dadosAluno.nome}! `);
          } else {
            alert(` Aluno(a) '${dadosAluno.nome}' cadastrado(a) com sucesso! `);
          }
          //console.log(response);
          this.router.navigate(['/resumo-pacote']);
        },
        error: (msgErro) => {
          alert(`Erro no cadastro de aluno: ${msgErro.error.message}`)
        }
      });
    }
  }

}

