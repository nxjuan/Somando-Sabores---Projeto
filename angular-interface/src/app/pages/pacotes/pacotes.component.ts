import { Component } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { RouterModule, Router } from '@angular/router';
import { FormControl, FormGroup, FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Aluno } from '../../models/AlunoModel';
import { AlunosService } from '../../services/alunos/alunos.service';

@Component({
  standalone: true,
  selector: 'app-pacotes',
  imports: [HeaderBarComponent, RouterModule, FormsModule, CommonModule],
  templateUrl: './pacotes.component.html',
  styleUrl: './pacotes.component.scss'
})
export class PacotesComponent {
  constructor(private router: Router, private alunosService: AlunosService) { }

  id: number = 0;
  nome: string = "";
  email: string = "";
  ra: string = "";

  onSubmit(form: NgForm) {
    if (form.invalid) {
      Object.values(form.controls).forEach(control => control.markAsTouched());
    } else {
      const dadosAluno: Aluno = form.value;
      //console.log("Dados do aluno a serem enviados: ", dadosAluno);
      this.alunosService.create(dadosAluno).subscribe({
        next: (response) => {
          if(response.message == "Aluno já cadastrado"){
            alert(`Bem-vindo(a) de volta, ${dadosAluno.nome}! `);
          } else {
            alert(` Aluno(a) '${dadosAluno.nome}' cadastrado(a) com sucesso! `);
          }
          //console.log(response);
          this.router.navigate(['/resumo-pacote']);
        },
        error: (msgErro) => {
          alert(`Erro no cadastro de aluno: ${msgErro}`)
        }
      });
    }
  }

}

