***Projeto Integrador***
> **Link do Figma**
> [Prototipo Restaurante](https://www.figma.com/file/2A9eoztwnfrPBTw6hseSd1/Untitled?type=design&node-id=0-1&mode=design&t=u4yjprx9qSJ5NwjY-0)

> **Visualizar Prototipo Interativo**
> [Modo Apresentação](https://www.figma.com/proto/2A9eoztwnfrPBTw6hseSd1/Untitled?type=design&node-id=224-2&t=eHOcuu5HRKBhWcSA-0&scaling=contain&page-id=0%3A1&starting-point-node-id=10%3A6&show-proto-sidebar=1)


> **Redes sociais do restaurante**
> `@somandosaboreslr`
`https://www.instagram.com/somandosaboreslr/`

# Projeto Somando Sabores

Bem-vindo ao repositório do projeto **Somando Sabores**! Aqui estão as instruções para rodar o projeto localmente.

---

## Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas na sua máquina:

- **Java JDK** (versão 21 ou superior)
- **Maven**
- **Docker Desktop**

---

## Como rodar o projeto na sua máquina

### 1. Criar o container Docker

Antes de tudo, precisamos criar o container Docker. Execute o seguinte comando no terminal da raiz do projeto:

```bash
docker-compose up -d
```
certifique-se de estar com o programa docker-desktop aberto

## 2. Rodar o Back end

```bash
mvn spring-boot:run
```

## 3. Acessando Banco de dados Postgree

No navegador, insira:
```bash
http://localhost:15432/
```
senha: admin

user: admin@admin.com

clicando com o botão direito em 'Servers'

Register -> Server

Na aba inicial Generals: 

  No campo nome, insira 'db'

na aba conection:

  no campo 'Host name/address': insira 'db'
  
  na 'port', insira: 5432
  
  no campo Maintenace database: postgres
  
  no campo username: admin
  
  no campo senha: admin

