export interface Pacote {
    idAluno: string,
    idPacote: string | undefined;
    nome: string | undefined;
    email: string | undefined;
    ra: string | undefined;
    dataInicio: string;
    dataFim: string;
    quantidade: number;
    total: number;
}