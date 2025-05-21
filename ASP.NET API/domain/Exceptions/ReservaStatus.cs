using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Exceptions;

public enum ReservaStatus
{
    Pendente = 0,
    Confirmada = 1,
    Cancelada = 2,
    Concluida = 3,
    Atrasada = 4,
    Reembolsada = 5
}
