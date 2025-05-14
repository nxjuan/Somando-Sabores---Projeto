PREPARE consulta_clientes (TEXT) AS
SELECT * FROM TB_CLIENTES WHERE id_cliente = $1;

-- TO BE DONE --