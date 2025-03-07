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
