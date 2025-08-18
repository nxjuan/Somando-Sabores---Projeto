using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models;

public class Pacote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid? Id { get; set; }
    public Guid AlunoId { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime DataFinal { get; set; }
    public Aluno? Aluno { get; set; }
    public Guid PrecificacaoId { get; set; }
    public Precificacao? Precificacao { get; set; }
}
