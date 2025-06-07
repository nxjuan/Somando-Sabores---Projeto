using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models;

public class Aluno
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid ClienteId { get; set; }
    
    public Cliente Cliente { get; set; }
    
    public string RA { get; set; }
}
