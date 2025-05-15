CREATE TYPE tipo_pagamento AS ENUM ('debito', 'credito', 'pix');
CREATE TYPE opcoes_servico AS ENUM ('pacote', 'reserva');
CREATE TYPE status_evento AS ENUM ('em andamento', 'cancelado', 'concluido');


CREATE TABLE TB_CLIENTES(
    id_cliente UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    nome_completo VARCHAR(255) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE TB_EVENTOS(
    id_evento UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    data_evento DATE NOT NULL,
    detalhes VARCHAR(1000) NOT NULL,
    evento_status status_evento NOT NULL,

    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente)
);

CREATE TABLE TB_ALUNOS(
    id_aluno UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    ra VARCHAR(8) UNIQUE NOT NULL,

    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente)
);

CREATE TABLE TB_PRECIFICACAO(
    id_precificacao UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    tipo_servico opcoes_servico NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario NUMERIC(10, 2) NOT NULL CHECK(preco_unitario > 0)
);

CREATE TABLE TB_PACOTES(
    id_pacote UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    aluno_id UUID NOT NULL,
    precificacao_id UUID NOT NULL,

    FOREIGN KEY (aluno_id) REFERENCES TB_ALUNOS(id_aluno),
    FOREIGN KEY (precificacao_id) REFERENCES TB_PRECIFICACAO(id_precificacao)
);

CREATE TABLE TB_RESERVAS(
    id_reserva UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    precificacao_id UUID NOT NULL,
    qtd_convidados INT,
    data_reserva DATE NOT NULL,

    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente),
    FOREIGN KEY (precificacao_id) REFERENCES TB_PRECIFICACAO(id_precificacao)
);

CREATE TABLE TB_CONVIDADOS(
    id_convidado UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    reserva_id UUID NOT NULL,
    nome_completo VARCHAR(255) NOT NULL,

    FOREIGN KEY (reserva_id) REFERENCES TB_RESERVAS(id_reserva)
);

CREATE TABLE TB_PAGAMENTOS(
    id_pagamento UUID DEFAULT gen_random_uuid() PRIMARY KEY,
    cliente_id UUID NOT NULL,
    reserva_id UUID,
    pacote_id UUID,
    forma_pagamento tipo_pagamento NOT NULL,
    valor_total NUMERIC(10, 2) NOT NULL CHECK(valor_total > 0), 
    data_pagamento DATE NOT NULL,
    asaas_id NOT NULL UNIQUE

    FOREIGN KEY (cliente_id) REFERENCES TB_CLIENTES(id_cliente),
    FOREIGN KEY (reserva_id) REFERENCES TB_RESERVAS(id_reserva),
    FOREIGN KEY (pacote_id) REFERENCES TB_PACOTES(id_pacote),
)