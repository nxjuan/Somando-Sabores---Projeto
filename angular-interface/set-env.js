const fs = require('fs');
const path = require('path');
require('dotenv').config(); // Carrega as variáveis do arquivo .env

// Caminho para o arquivo environment.ts que será gerado
const envFilePath = path.join(__dirname, 'src', 'environments', 'environment.ts');

// Conteúdo que será escrito no arquivo
const envFileContent = `
// Este arquivo é gerado automaticamente pelo script set-env.js. NÃO EDITE MANUALMENTE.
export const environment = {
  accessToken: '${process.env.NG_APP_ACCESS_TOKEN}',
  urlWebhook: '${process.env.NG_APP_URL_WEBHOOK}',
};
`;

// Escreve o conteúdo no arquivo
fs.writeFile(envFilePath, envFileContent, (err) => {
  if (err) {
    console.error(err);
    throw err;
  }
  console.log(`Arquivo de ambiente gerado com sucesso em ${envFilePath}`);
});