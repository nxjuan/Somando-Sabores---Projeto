import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

interface Guest {
  name: string;
}

interface CustomerResponse {
  data: { id: string }[];
}

@Component({
  standalone: true,
  selector: 'app-reservas',
  imports: [HeaderBarComponent, CommonModule, FormsModule],
  templateUrl: './reservas.component.html',
  styleUrls: ['./reservas.component.scss']
})
export class ReservasComponent implements OnInit {
  guestCount: number = 1;
  guests: Guest[] = [];
  cpfCnpj: string = '';
  name: string = '';
  email: string = '';
  submitted: boolean = false;
  showModal: boolean = false;
  subtotal: number = 0;
  total: number = 0;
  dueDate: string = '';
  wantInvoice: boolean = false;
  nextSaturdayDate: string = ''; // Nova propriedade para armazenar a data do próximo sábado

  private apiUrl = '/api/v3';
  private accessToken = '$aact_hmlg_000MzkwODA2MWY2OGM3MWRlMDU2NWM3MzJlNzZmNGZhZGY6OjllMGEyMjVjLTdkNjMtNDU4MC05Y2Y4LTQwOTJmMzYyYTk0ZDo6JGFhY2hfMDEzOTVlN2ItMGM3NS00OWVjLTg5NGMtNTcxYTJmMTQzMjFh';
  private makeWebhookUrl = 'https://hook.make.com/<seu-webhook-id>';

  constructor(private http: HttpClient) {}

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

    const day = String(nextSaturday.getDate()).padStart(2, '0');
    const month = String(nextSaturday.getMonth() + 1).padStart(2, '0');
    const year = nextSaturday.getFullYear();
    this.nextSaturdayDate = `${day}/${month}/${year}`; // Formato DD/MM/YYYY
  }

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'accept': 'application/json',
      'content-type': 'application/json',
      'access_token': this.accessToken
    });
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
      this.guests.push({ name: '' });
    }
  }

  acessarResumo(): void {
    this.submitted = true;
    if (!this.cpfCnpj || !this.name || !this.email || this.guests.some(guest => !guest.name)) {
      return;
    }
    this.subtotal = this.guestCount * 39.9;
    this.total = this.subtotal;
    this.showModal = true;
  }

  fecharResumo(): void {
    this.showModal = false;
  }

  prosseguirPagamento(): void {
    this.handleSubmit();
  }

  handleSubmit(): void {
    if (!this.cpfCnpj || !this.name || !this.email || !this.total || !this.dueDate) {
      alert('Por favor, preencha todos os campos.');
      return;
    }

    const searchUrl = `${this.apiUrl}/customers?cpfCnpj=${this.cpfCnpj}`;
    console.log('Buscando cliente:', searchUrl);

    this.http.get<CustomerResponse>(searchUrl, { headers: this.getHeaders() }).subscribe({
      next: (searchResponse) => {
        let customerId: string;

        if (searchResponse.data.length === 0) {
          const registerUrl = `${this.apiUrl}/customers`;
          const customerPayload = { name: this.name, cpfCnpj: this.cpfCnpj, email: this.email };

          this.http.post<any>(registerUrl, customerPayload, { headers: this.getHeaders() }).subscribe({
            next: (customerResponse) => {
              customerId = customerResponse.id;
              this.createPayment(customerId);
            },
            error: (err) => {
              console.error('Erro ao cadastrar cliente:', err);
              alert('Erro ao cadastrar cliente: ' + err.message + ' - Detalhes: ' + JSON.stringify(err.error || err));
            }
          });
        } else {
          customerId = searchResponse.data[0].id;
          this.createPayment(customerId);
        }
      },
      error: (err) => {
        console.error('Erro ao buscar cliente:', err);
        alert('Erro ao buscar cliente: ' + err.message + ' - Detalhes: ' + JSON.stringify(err.error || err));
      }
    });
  }

  private createPayment(customerId: string): void {
    const paymentUrl = `${this.apiUrl}/payments`;
    const paymentPayload = {
      billingType: 'BOLETO',
      customer: customerId,
      value: this.total,
      dueDate: this.dueDate,
      description: 'Pagamento de reserva para buffet',
      successUrl: 'https://webhook.site/#!/<seu-id>'
    };

    this.http.post<any>(paymentUrl, paymentPayload, { headers: this.getHeaders() }).subscribe({
      next: (paymentResponse) => {
        console.log('Pagamento criado:', paymentResponse);
        const invoiceUrl = paymentResponse.invoiceUrl;
        if (invoiceUrl) {
          const notificationPayload = {
            customerId,
            paymentId: paymentResponse.id,
            wantInvoice: this.wantInvoice,
            email: this.email,
            total: this.total
          };
          this.http.post(this.makeWebhookUrl, notificationPayload).subscribe(
            () => console.log('Notificação enviada para Make'),
            err => console.error('Erro ao notificar Make:', err)
          );
          window.location.href = invoiceUrl;
        } else {
          alert('Erro: URL da fatura não encontrada.');
        }
      },
      error: (err) => {
        console.error('Erro ao criar pagamento:', err);
        alert('Erro ao criar pagamento: ' + err.message + ' - Detalhes: ' + JSON.stringify(err.error || err));
      }
    });
  }
}