-- Definição dos tipos ENUM
CREATE TYPE opcoes_servico AS ENUM ('pacote', 'reserva');
CREATE TYPE status_pagamento AS ENUM ('pendente', 'confirmado', 'cancelado', 'concluida', 'atrasada', 'reembolsada');

-- Tabela de Clientes
CREATE TABLE TB_CLIENTES(
    id_cliente UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    nome_completo VARCHAR(255) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL
);

-- Tabela de Eventos
CREATE TABLE TB_EVENTOS(
    id_evento UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    data_evento DATE NOT NULL,
    detalhes VARCHAR(1000) NOT NULL,
    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente)
);

-- Tabela de Alunos
CREATE TABLE TB_ALUNOS(
    id_aluno UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    ra VARCHAR(8) UNIQUE NOT NULL,
    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente)
);

-- Tabela de Precificação
CREATE TABLE TB_PRECIFICACAO(
    id_precificacao UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    tipo_servico opcoes_servico NOT NULL,
    quantidade INT NOT NULL,
    status_precificacao status_pagamento NOT NULL,
    total NUMERIC(10, 2) NOT NULL CHECK(total > 0), 
    emitir_nf BOOLEAN NOT NULL
);

-- Tabela de Pacotes
CREATE TABLE TB_PACOTES(
    id_pacote UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    aluno_id UUID NOT NULL,
    precificacao_id UUID NOT NULL,
    data_inicio DATE NOT NULL,
    data_final DATE NOT NULL,
    FOREIGN KEY (aluno_id) REFERENCES TB_ALUNOS(id_aluno),
    FOREIGN KEY (precificacao_id) REFERENCES TB_PRECIFICACAO(id_precificacao)
);

-- Tabela de Reservas (deve ser criada antes de TB_CONVIDADOS)
CREATE TABLE TB_RESERVAS(
    id_reserva UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    precificacao_id UUID NOT NULL,
    qtd_convidados INT,
    data_reserva DATE NOT NULL,
    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente),
    FOREIGN KEY (precificacao_id) REFERENCES TB_PRECIFICACAO(id_precificacao)
);

-- Tabela de Convidados
CREATE TABLE TB_CONVIDADOS(
    id_convidado UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    reserva_id UUID NOT NULL,
    nome_completo VARCHAR(255) NOT NULL,
    FOREIGN KEY (reserva_id) REFERENCES TB_RESERVAS(id_reserva)
);

-- Tabela de Pagamentos
CREATE TABLE TB_PAGAMENTOS(
    id_pagamento UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    reserva_id UUID,
    pacote_id UUID,
    forma_pagamento VARCHAR(35) NOT NULL,
    valor_total NUMERIC(10, 2) NOT NULL CHECK(valor_total > 0), 
    data_pagamento DATE NOT NULL,
    asaas_id VARCHAR(40) NOT NULL UNIQUE,
    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente),
    FOREIGN KEY (reserva_id) REFERENCES TB_RESERVAS(id_reserva),
    FOREIGN KEY (pacote_id) REFERENCES TB_PACOTES(id_pacote)
);

-- Definição dos índices
CREATE INDEX idx_convidados_reserva_id ON tb_convidados (reserva_id);