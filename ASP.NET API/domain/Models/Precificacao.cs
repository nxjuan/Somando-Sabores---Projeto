using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain.Enums;

namespace domain.Models;

public class Precificacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public OpcoesServico TipoServico { get; set; }
    public int Quantidade { get; set; }
    public decimal Total { get; set; }
    public StatusPrecificacao Status { get; set; }
    public bool EmitirNF { get; set; }

}
