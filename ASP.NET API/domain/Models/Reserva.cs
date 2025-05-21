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
    public DateTime DataReserva { get; set; } = DateTime.Now;
    public int? EventoId { get; set; }
    public Evento? Evento { get; set; } = new Evento();
    public Double Valor { get; set; } = 0.0;
    public ReservaStatus ReservaStatus { get; set; } = ReservaStatus.Pendente;
}
