# Documentação para Rodar a API com Docker

Esta documentação orienta como configurar e rodar a API utilizando o Docker.

## 1. Instalar o Docker Desktop
- Windows: Baixe o Docker Desktop [aqui](https://www.docker.com/products/docker-desktop/) e siga as instruções de instalação.
  
## 2. Rodar a API
- Abra o terminal no doretório 'API
- Cole o seguinte comando no terminal: `docker-compose up --build`
## 3. Acessar a API via Swagger
- Após o container estar em execução, você pode acessar a interface Swagger da API no seguinte endereço:
- http://localhost:8080/swagger-ui/index.html
- Essa interface permitirá que você visualize e interaja com os endpoints da API sem a necessidade de postman.

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

