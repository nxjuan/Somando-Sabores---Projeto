using domain.IServices;
using domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReservaController(IReservaService reservaService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Reserva>>> GetById(int id)
    {
        var resposta = await reservaService.GetReserva(id);
        if (resposta.Success)
        {
            return Ok(resposta);
        }
        else
        {
            return BadRequest(resposta);
        }
    }
}