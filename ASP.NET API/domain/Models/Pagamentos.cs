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
    public Guid Id { get; set; }
    public DateTime DataPagamento { get; set; } = DateTime.Now;
    public Double Valor { get; set; } = 0.0;
    public string? FormaPagamento { get; set; } = string.Empty;

    
    public Guid ReservaId { get; set; } 
    
    public Reserva? Reserva { get; set; }

    
    public Guid ClienteId { get; set; }
   
    public Cliente Cliente { get; set; } 

    public Guid AlunoId { get; set; } 

    public Aluno Aluno { get; set; } 
}
