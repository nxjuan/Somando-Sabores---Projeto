-- Prevenção de SQLi --

-- GET BY ID --
PREPARE consulta_clientes (TEXT) AS
SELECT * FROM TB_CLIENTES WHERE id_cliente = $1;

-- POST --


-- PUT --


-- DELETE --

