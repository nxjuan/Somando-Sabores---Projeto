# Documentação para Rodar a API com Docker

Esta documentação orienta como configurar e rodar a API utilizando o Docker.

## 1. Instalar o Docker Desktop
- Windows: Baixe o Docker Desktop [aqui](https://www.docker.com/products/docker-desktop/) e siga as instruções de instalação.
  
## 2. Rodar a API
- Abra o terminal no diretório 'ASP.NET API/somandosabores.api'
- Cole o seguinte comando no terminal: `docker-compose up --build`

## 3. Acessar a API via Swagger
- Após o container estar em execução, você pode acessar a interface Swagger da API no seguinte endereço:
- http://localhost:5000/
- Essa interface permitirá que você visualize e interaja com os endpoints da API sem a necessidade de postman.
- *OBS:* Para que os endpoints funcionem direito, é necessário configurar o banco de dados.



# 💾 Como configurar o banco de dados para testar os endpoints

## 1. Acesse o PgAdmin
- Abra a seguinte URL no navegador: http://localhost:15432/
- Em "Email Address / Username" digite:
```
admin@admin.com
```
- Em "Password", digite:
```
admin
```

## 2. Registrando o BD
- No menu esquerdo, em "Servers", clique com o botão direito do mouse

- Vá em "Register" e clique em "Server..."

- No campo "Name" da primeira aba (General), digite:
```
ssdb
```

- Na aba "Connection", em "Host name / address", digite:
```
db
```

- Em "Username", digite:
```
restaurantuser
```

- Em "Password", digite:
```
s0M@ND0
```

- Clique em "Save"


# 3. Criando as tabelas (provisório)

- No menu esquerdo, expanda os conteúdos de "ssdb"

- Siga a ordem: "Databases" > "ssdb" > "Schemas" > "Public"

- Em "Tables", clique com o botão direito e vá em "Query Tool"

- No campo de "Query", cole o script "db.sql" e clique no símbolo de play (ou F5).



# 🚀 Como Rodar a Aplicação Angular Localmente

Este guia explica como configurar e executar uma aplicação Angular local em seu ambiente de desenvolvimento.

---

## ✅ Pré-requisitos

Antes de iniciar, verifique se você possui os seguintes softwares instalados:

1. **Node.js** (versão LTS recomendada)  
   [https://nodejs.org/](https://nodejs.org/)

2. **Angular CLI**  
   Instale com o comando:
   ```bash
   npm install -g @angular/cli
  
---
## Rodando Localmente
1. apos finalizar as configurações de ambiente, instale os pacotes:
- Execute no diretorio 'angular-interface':
```bash
npm install
```
2. Builde a aplicação:
```bash
ng serve
```
3. EXTRA: Caso receba um erro ao rodar o ng serve, considere rodar no terminal, já que o powershell do vscode pode precisar de permissões adicionais para executar certos comandos. Fique à vontade para executar o codigo que libera o powershell no vscode, fica até mais facil.
   ```bash
   Set-ExecutionPolicy RemoteSigned
   ```

