using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models;

public class Pagamentos
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; } = 0;
    public DateTime DataPagamento { get; set; } = DateTime.Now;
    public Double Valor { get; set; } = 0.0;
    public string? FormaPagamento { get; set; } = string.Empty;

    
    public int ReservaId { get; set; } = 0;
    [ForeignKey("reserva_id")]
    public Reserva? Reserva { get; set; } = new Reserva();

    
    public int ClienteId { get; set; } = 0;
    [ForeignKey("cliente_id")]
    public Cliente Cliente { get; set; } = new Cliente();

    public int AlunoId { get; set; } = 0;
    [ForeignKey("cliente_id")]
    public Aluno Aluno { get; set; } = new Aluno();
}
