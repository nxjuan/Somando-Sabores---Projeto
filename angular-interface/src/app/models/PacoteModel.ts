export interface Pacote {
    idPacote: string | undefined;
    idAluno: string,
    nome: string | undefined;
    email: string | undefined;
    ra: string | undefined;
    dataInicio: string;
    dataFim: string;
    quantidade: number;
    total: number;
}