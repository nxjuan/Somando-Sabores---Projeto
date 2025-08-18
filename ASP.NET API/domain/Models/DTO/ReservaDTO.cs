using domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models.DTO;

public class ReservaDTO
{
    // Reserva
    public Guid? Id { get; set; }
    public string? CpfOuCnpj { get; set; }
    public DateTime DataReserva { get; set; }
    public int QtdConvidados { get; set; }

    // Cliente
    public string? Nome { get; set; }
    public string? Email { get; set; }

    // Convidado
    public List<Guid>? IdsConvidados { get; set; }
    public List<string>? NomesConvidados { get; set; }

    // Precificação
    public int Quantidade { get; set; }
    public decimal Total { get; set; }
    public StatusPrecificacao Status { get; set; }
    public OpcoesServico TipoServico { get; set; }
    public bool EmitirNF { get; set; } = false;
}
