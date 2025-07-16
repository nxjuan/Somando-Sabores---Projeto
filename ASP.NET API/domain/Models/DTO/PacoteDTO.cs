using System;
using domain.Enums;

namespace domain.Models.DTO;

public class PacoteDTO
{
    public Guid IdPacote { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public Guid IdAluno { get; set; }

    // Cliente
    public string Nome { get; set; }
    public string Email { get; set; }

    // Aluno
    public string RA { get; set; }

    // Precificação
    public int Quantidade { get; set; }
    public decimal Total { get; set; }
    public StatusPrecificacao Status { get; set; }
}
