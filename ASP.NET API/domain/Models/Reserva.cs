using domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models;

public class Reserva
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }
    public DateTime DataReserva { get; set; } = DateTime.UtcNow;
    public int? EventoId { get; set; } = null;
    public Evento? Evento { get; set; }= null;
    public Double Valor { get; set; } = 0.0;
    public ReservaStatus ReservaStatus { get; set; } = ReservaStatus.Pendente;
}
