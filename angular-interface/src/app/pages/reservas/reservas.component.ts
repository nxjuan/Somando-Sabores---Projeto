import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HeaderBarComponent } from '../../components/header-bar/header-bar.component';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  selector: 'app-reservas',
  imports: [HeaderBarComponent, CommonModule, FormsModule],
  templateUrl: './reservas.component.html',
  styleUrl: './reservas.component.scss'
})
export class ReservasComponent implements OnInit {
  cpfCnpj: string = '';
  name: string = '';
  email: string = '';
  value: number = 0; // Valor padrão (pode ser ajustado pelo usuário)
  dueDate: string = ''; // Data padrão (pode ser ajustada pelo usuário)
  message: string = '';

  private apiUrl = '/api/v3';
  private accessToken = '$aact_hmlg_000MzkwODA2MWY2OGM3MWRlMDU2NWM3MzJlNzZmNGZhZGY6OjllMGEyMjVjLTdkNjMtNDU4MC05Y2Y4LTQwOTJmMzYyYTk0ZDo6JGFhY2hfMDEzOTVlN2ItMGM3NS00OWVjLTg5NGMtNTcxYTJmMTQzMjFh';

  constructor(private http: HttpClient) {}

  ngOnInit(): void {}

  private getHeaders(): HttpHeaders {
    return new HttpHeaders({
      'accept': 'application/json',
      'content-type': 'application/json',
      'access_token': this.accessToken
    });
  }

  handleSubmit(): void {
    // Validação básica
    if (!this.cpfCnpj || !this.name || !this.email || !this.value || !this.dueDate) {
      this.message = 'Por favor, preencha todos os campos.';
      return;
    }

    this.message = 'Criando pagamento...';

    // Primeiro, cadastrar o cliente
    const registerUrl = `${this.apiUrl}/customers`;
    const customerPayload = {
      name: this.name,
      cpfCnpj: this.cpfCnpj,
      email: this.email
    };

    this.http.post<any>(registerUrl, customerPayload, { headers: this.getHeaders() }).subscribe({
      next: (customerResponse) => {
        const customerId = customerResponse.id;

        // Em seguida, criar o pagamento
        const paymentUrl = `${this.apiUrl}/payments`;
        const paymentPayload = {
          billingType: 'UNDEFINED',
          customer: customerId,
          value: this.value,
          dueDate: this.dueDate
        };

        this.http.post<any>(paymentUrl, paymentPayload, { headers: this.getHeaders() }).subscribe({
          next: (paymentResponse) => {
            this.message = 'Pagamento criado com sucesso! Redirecionando...';
            const invoiceUrl = paymentResponse.invoiceUrl;
            if (invoiceUrl) {
              // Redirecionar para a invoiceUrl
              window.location.href = invoiceUrl;
            } else {
              this.message = 'Erro: URL da fatura não encontrada.';
            }
          },
          error: (err) => {
            this.message = 'Erro ao criar pagamento: ' + err.message;
          }
        });
      },
      error: (err) => {
        this.message = 'Erro ao cadastrar cliente: ' + err.message;
      }
    });
  }
}