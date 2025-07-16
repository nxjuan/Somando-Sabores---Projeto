import { Component, Input, NgModule, OnInit } from '@angular/core';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { FormControl, FormGroup, FormsModule, NgForm } from '@angular/forms';
import { MatIcon } from '@angular/material/icon';
import { RouterModule} from '@angular/router'; 
import { Pacote } from '../../models/PacoteModel';
import { AlunosService } from '../../services/alunos/alunos.service';
import { PacoteService } from '../../services/pacote/pacote.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-resumo-pacote',
  imports: [HeaderBarComponent, RouterModule, MatIcon, FormsModule],
  standalone: true,
  templateUrl: './resumo-pacote.component.html',
  styleUrl: './resumo-pacote.component.scss'
})
export class ResumoPacoteComponent{

  constructor(private alunosService: AlunosService, private pacotesService: PacoteService) { }

  id: string | undefined = undefined;
  nome: string | undefined = undefined;
  email: string | undefined = undefined;
  ra: string | undefined = undefined;
  precoFixo: number = 516.00;
  quantidade: number = 1;
  subtotal: number = this.precoFixo;
  total: number = this.subtotal;
  data_inicio: string =  '';
  data_fim: string = '';
  idAluno: string | undefined = undefined;
  private idAlunoSubscription: Subscription | undefined;

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

  private geraDataAtual(): void {
    const dataAtual = new Date();
    this.data_inicio = dataAtual.toISOString();
  }

  private calculaDataFinal(): void {
   const data = new Date(this.data_inicio);
   data.setDate(data.getDate() + 30 * this.quantidade);
   const dia = data.getDay();

   // Checa se o dia final do pacote é Domingo, se for, "joga" o dia final pra Segunda
   if(dia == 0){
    data.setDate(data.getDate() + 1)
   }

   this.data_fim = data.toISOString();

  }

  ngOnInit(): void {
    this.idAlunoSubscription = this.alunosService.currentIdAluno.subscribe(id => {
      this.idAluno = id;
      if (this.idAluno) {
        console.log('ID do Aluno recebido no ResumoPacote:', this.alunosService);
        this.criaPacoteModel();
      } else {
        console.log('Aguardando ID do Aluno...');
      }
    });
  }

  ngOnDestroy(): void {
    if (this.idAlunoSubscription) {
      this.idAlunoSubscription.unsubscribe();
    }
  }

  private criaPacoteModel(): any {
    this.geraDataAtual();
    this.calculaDataFinal();

    if (this.idAluno){    
      const pacote: Pacote = {
        idPacote: this.id,
        nome: this.nome,
        email: this.email,
        ra: this.ra,
        dataInicio: this.data_inicio,
        dataFim: this.data_fim,
        idAluno: this.idAluno,
        quantidade: this.quantidade,
        total: this.total
      }
      return pacote;
    }
    return "Id do Aluno inválido!";
  }

  prosseguirParaPagamento(): any {
    const dadosPacote: Pacote = this.criaPacoteModel();

    if(dadosPacote.idAluno == undefined){
      console.error("Id do aluno não encontado");
      return;
    }
    
    console.log("Dados do pacote: ", dadosPacote);
    this.pacotesService.create(dadosPacote).subscribe({
      next: (response) => {
        alert(`Pacote cadastrado com sucesso! `);
      },
      error: (msgErro) => {
        alert(`Erro no cadastro de aluno: ${msgErro}`);
      }
    });

    //Colocar o bagulho do Asas!!
  }
}