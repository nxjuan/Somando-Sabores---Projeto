import { Component, OnInit } from '@angular/core';
import { Reserva } from '../../models/ReservaModel';
import { Guest } from '../../models/GuestModel';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { MatIcon } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, NgForm } from '@angular/forms';
import { ReservaService } from '../../services/reservas/reserva.service';
import { RouterModule, Router} from '@angular/router';
// import { environment } from '../../../environments/environment'; 
// import { NgxMaskDirective } from 'ngx-mask';
interface CustomerResponse {
  data: { id: string }[];
}

@Component({
  standalone: true,
  selector: 'app-reservas',
  imports: [HeaderBarComponent, CommonModule, FormsModule, MatIcon],//, NgxMaskDirective],
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.scss']
})
export class ReservasComponent implements OnInit {
  guestCount: number = 1;
  guests: Guest[] = [];

  // Dados da Reserva
  cpfOuCnpj: string = '';
  nome: string = '';
  email: string = '';
  convidados: number = 0;
  nomesConvidados: string[] = [];
  qtdConvidados: number = 1;
  quantidade: number = 1;
  dueDate: string = '';

  submitted: boolean = false;
  showModal: boolean = false;
  subtotal: number = 0;
  total: number = 0;
  wantInvoice: boolean = false;
  nextSaturdayDate: string = ''; // Nova propriedade para armazenar a data do próximo sábado
  nextSaturdayDateISO: string = ''; 

  // private apiUrl = '/api/v3';
  // private accessToken = environment.accessToken; 
  // private makeWebhookUrl = environment.urlWebhook;

  constructor(private http: HttpClient, private reservaService: ReservaService, private router: Router) {}

  ngOnInit(): void {
    this.updateGuests();
    this.setDueDateToToday();
    this.setNextSaturdayDate(); // Calcula a data do próximo sábado
  }

  private setDueDateToToday(): void {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    this.dueDate = `${year}-${month}-${day}`;
  }

  private setNextSaturdayDate(): void {
    const today = new Date();
    const currentDay = today.getDay(); // 0 = Domingo, 1 = Segunda, ..., 6 = Sábado
    const daysUntilSaturday = (6 - currentDay + 7) % 7 || 7; // Calcula dias até o próximo sábado

    const nextSaturday = new Date(today);
    nextSaturday.setDate(today.getDate() + daysUntilSaturday);
    this.nextSaturdayDateISO = nextSaturday.toISOString();

    const day = String(nextSaturday.getDate()).padStart(2, '0');
    const month = String(nextSaturday.getMonth() + 1).padStart(2, '0');
    const year = nextSaturday.getFullYear();
    this.nextSaturdayDate = `${day}/${month}/${year}`; // Formato DD/MM/YYYY
  }

  addGuest(): void {
    this.guestCount++;
    this.updateGuests();
  }

  removeGuest(): void {
    if (this.guestCount > 1) {
      this.guests.pop();
      this.guestCount--;
    }
  }

  updateGuests(): void {
    while (this.guests.length < this.guestCount - 1) {
      this.guests.push({ nome: '' });
    }
  }

  acessarResumo(): void {
    this.submitted = true;
    if (!this.cpfOuCnpj || !this.nome || !this.email || this.guests.some(guest => !guest.nome)) {
      return;
    }
    this.subtotal = this.guestCount * 39.9;
    this.total = this.subtotal;
    this.showModal = true;
  }

  fecharResumo(): void {
    this.showModal = false;
  }

  // Envia os dados do formulário para o back
  onSubmit(form: NgForm){
    if(form.invalid){
      Object.values(form.controls).forEach(control => control.markAsTouched());
    } else {
      const dadosReserva: Reserva = form.value;
      dadosReserva.nomesConvidados = [];
      dadosReserva.dataReserva = this.nextSaturdayDateISO;
      dadosReserva.qtdConvidados = this.guestCount - 1;
      dadosReserva.total = this.total;
      dadosReserva.quantidade = this.quantidade;
      this.guests.forEach(guest => {
        dadosReserva.nomesConvidados.push(guest.nome);
      });
      
      // console.log("Dados da reserva: ", dadosReserva);
      this.reservaService.create(dadosReserva).subscribe({
        next: (response) => {
          alert(` Reserva cadastrada com sucesso! `);
          //this.router.navigate(['/pagamentos']);
          //this.iniciarProcessoDePagamento(); 
        },
        error: (msgErro) => {
          alert(msgErro.error.message);
        }
      });
    }
  }

  // +-------------------------------------------------------------------------------------------------------+
  // |                                                                                                       |
  // | ATENÇÃO: A seção abaixo foi comentada para que o cadastro pudesse ser testado, pois estava dando erro |
  // |                                                                                                       |
  // +-------------------------------------------------------------------------------------------------------+
//   private getHeaders(): HttpHeaders {
//     return new HttpHeaders({
//       'accept': 'application/json',
//       'content-type': 'application/json',
//       'access_token': this.accessToken
//     });
//   }

//  private iniciarProcessoDePagamento(): void {
//     console.log("3. Iniciando processo de pagamento no Asaas...");

//     const searchUrl = `${this.apiUrl}/customers?cpfCnpj=${this.cpfOuCnpj}`;
//     console.log('Buscando cliente no Asaas:', searchUrl);

//     this.http.get<CustomerResponse>(searchUrl, { headers: this.getHeaders() }).subscribe({
//       next: (searchResponse) => {
//         if (searchResponse.data.length === 0) {
//           console.log("Cliente não encontrado, cadastrando novo cliente no Asaas...");
//           this.criarClienteEContinuarPagamento();
//         } else {
//           const customerId = searchResponse.data[0].id;
//           console.log("Cliente encontrado no Asaas com ID:", customerId);
//           this.criarCobrancaAsaas(customerId);
//         }
//       },
//       error: (err) => this.handleAsaasError('buscar cliente', err)
//     });
//   }

//   private criarClienteEContinuarPagamento(): void {
//     const registerUrl = `${this.apiUrl}/customers`;
//     const customerPayload = { name: this.nome, cpfCnpj: this.cpfOuCnpj, email: this.email, notificationDisabled: true };

//     this.http.post<any>(registerUrl, customerPayload, { headers: this.getHeaders() }).subscribe({
//       next: (customerResponse) => {
//         const customerId = customerResponse.id;
//         console.log("Novo cliente cadastrado no Asaas com ID:", customerId);
//         this.criarCobrancaAsaas(customerId);
//       },
//       error: (err) => this.handleAsaasError('cadastrar cliente', err)
//     });
//   }

//   // NOVO: Renomeado de createPayment para mais clareza
//   private criarCobrancaAsaas(customerId: string): void {
//     console.log("4. Criando cobrança no Asaas para o cliente ID:", customerId);
//     const paymentUrl = `${this.apiUrl}/payments`;
//     const paymentPayload = {
//       billingType: 'UNDEFINED',
//       customer: customerId,
//       value: this.total,
//       dueDate: this.dueDate,
//       description: 'Pagamento de reserva para buffet - ' + this.nextSaturdayDate,
//       // Se quiser que o Asaas notifique seu webhook
//       // webhookUrl: this.makeWebhookUrl 
//     };

//     this.http.post<any>(paymentUrl, paymentPayload, { headers: this.getHeaders() }).subscribe({
//       next: (paymentResponse) => {
//         console.log('5. Pagamento criado no Asaas:', paymentResponse);
//         const invoiceUrl = paymentResponse.invoiceUrl;
//         if (invoiceUrl) {
//           console.log("6. Redirecionando para:", invoiceUrl);
//           window.location.href = invoiceUrl; // <-- O REDIRECIONAMENTO FINAL!
//         } else {
//           alert('Erro: URL da fatura não foi retornada pelo Asaas.');
//         }
//       },
//       error: (err) => this.handleAsaasError('criar pagamento', err)
//     });
//   }
  
//   // NOVO: Um método central para lidar com erros do Asaas e dar mais detalhes.
//   private handleAsaasError(action: string, error: HttpErrorResponse): void {
//       console.error(`Erro ao ${action} no Asaas:`, error);
//       let errorDetails = '';
//       if(error.error && typeof error.error === 'object') {
//           // A API do Asaas geralmente retorna um objeto 'errors'
//           errorDetails = JSON.stringify(error.error.errors || error.error);
//       }
//       alert(`Ocorreu um erro durante a etapa de '${action}'. Detalhes: ${errorDetails}. Por favor, verifique os dados e tente novamente.`);
//   }
}