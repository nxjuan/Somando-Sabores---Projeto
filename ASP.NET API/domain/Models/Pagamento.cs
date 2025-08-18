using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models;

public class Pagamento
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public DateTime DataPagamento { get; set; } = DateTime.Now;
    public decimal ValorTotal { get; set; }
    public string? FormaPagamento { get; set; }
    public string? AsaasId { get; set; }
    
    public Guid? ReservaId { get; set; } 
    public Reserva? Reserva { get; set; }

    public Guid? PacoteId { get; set; } 
    public Pacote Pacote { get; set; }

    public Guid ClienteId { get; set; }
    public Cliente Cliente { get; set; } 
}
