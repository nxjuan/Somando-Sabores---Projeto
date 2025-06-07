using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Models.DTO;

public class AlunoDTO
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }
    
    public string RA { get; set; } 
}
