using System;
using System.Text.RegularExpressions;

namespace domain.Models.Common;

public class Validations
{
    bool ValidaCPF(string CPF)
    {
        try
        {
            string cpf_sem_pontuacao = CPF.Replace(".", "").Replace("-", "");
            string digitos_verificadores = cpf_sem_pontuacao.Substring(9);
            int soma_validacao_1 = 0;
            int soma_validacao_2 = 0;
            int controle = 10;

            // Verifica se o cpf fornecido só tem dígitos iguais e retorna falso se tiver
            bool digitosIguais = cpf_sem_pontuacao.Skip(1).All(c => c == cpf_sem_pontuacao[0]);
            if (digitosIguais) return false;

            // Soma dos dois dígitos verificadores
            for (int c = 0; c <= 8; c++)
            {
                bool sucesso = int.TryParse(cpf_sem_pontuacao[c].ToString(), out int digito);
                if (sucesso)
                {
                    soma_validacao_1 += digito * controle;
                    soma_validacao_2 += digito * (controle + 1);
                    controle--;
                }
            }

            // Cálculo do dígito 1
            int resto = soma_validacao_1 % 11;
            int digito_v1 = (resto < 2) ? 0 : 11 - resto;

            // Cálculo do dígito 2
            soma_validacao_2 += digito_v1 * 2;
            resto = soma_validacao_2 % 11;
            int digito_v2 = (resto < 2) ? 0 : 11 - resto;

            int verificador1 = (int)Char.GetNumericValue(digitos_verificadores[0]);
            int verificador2 = (int)Char.GetNumericValue(digitos_verificadores[1]);

            // Verificação dos dígitos fornecidos com os calculados
            if (verificador1 == digito_v1 && verificador2 == digito_v2)
            {
                return true;
            }

            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    bool ValidaEmail(string email)
    {
        try
        {
            string padraoRegex = @"^[a-zA-Z0-9]+([._%+-][a-zA-Z0-9]+)*@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(padraoRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            return regex.IsMatch(email);
        }
        catch
        {
            return false;
        }
    }

    bool ValidaEmailEstudantil(string email)
    {
        try
        {
            bool emailValido = ValidaEmail(email);
            string padraoEstudantil = @"^[a-zA-Z0-9]+([._][a-zA-Z0-9]+)*[@]educadventista.org$";
            Regex regex = new Regex(padraoEstudantil, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            bool ehAluno = regex.IsMatch(email);

            if (emailValido && ehAluno)
            {
                return true;
            }
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }
}