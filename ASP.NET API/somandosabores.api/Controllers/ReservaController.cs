using domain.IServices;
using domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;
[ApiController]
[Route("/api/[controller]")]
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

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Reserva>>> Post(Reserva reserva)
    {
        var resposta = await reservaService.CreateReserva(reserva);
        if (resposta.Success)
        {
            return Ok(resposta);
        }
        else
        {
            return BadRequest(resposta);
        }
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Reserva>>> Put(Reserva reserva)
    {
        var resposta = await reservaService.UpdateReserva(reserva);
        if (resposta.Success)
        {
            return Ok(resposta);
        }
        else
        {
            return BadRequest(resposta);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
    {
        var  resposta = await reservaService.DeleteReserva(id);
        if (resposta.Success)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(resposta);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Reserva>>> GetReserva()
    {
        var resposta = await reservaService.ListReservas();
        if (resposta.Success)
        {
            return Ok(resposta);
        }
        else
        {
            return Problem(resposta.Message, statusCode: 500);
        }
    }
}