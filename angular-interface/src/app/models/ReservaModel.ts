export interface Reserva {
    id: string;
    cpfOuCnpj: string;
    dataReserva: string;
    qtdConvidados: number;
    nome: string;
    email: string;
    quantidade: number;
    nomesConvidados: string[];
    total: number;
}