using domain.Enums;
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
    public Guid Id { get; init; }
    public DateTime DataReserva { get; set; }
    public int QtdConvidados { get; set; }

    public Guid ClienteId { get; set; }
    public Cliente? Cliente { get; set; }

    public List<Convidado>? Convidados { get; set; }

    public Guid PrecificacaoId { get; set; }
    public Precificacao? Precificacao { get; set; }
}
