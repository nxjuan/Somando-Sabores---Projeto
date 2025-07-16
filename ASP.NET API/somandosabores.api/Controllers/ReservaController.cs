using domain.IServices;
using domain.Models;
using domain.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class ReservaController(IReservaService reservaService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<ReservaDTO>>> GetById(Guid id)
    {
        var resposta = await reservaService.GetReservaDTO(id);
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
    public async Task<ActionResult<ServiceResponse<ReservaDTO>>> Post(ReservaDTO reservaDTO)
    {
        var resposta = await reservaService.CreateReservaDTO(reservaDTO);
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
    public async Task<ActionResult<ServiceResponse<ReservaDTO>>> Put(ReservaDTO reservaDTO)
    {
        var resposta = await reservaService.UpdateReserva(reservaDTO);
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
    public async Task<ActionResult<ServiceResponse<string>>> Delete(Guid id)
    {
        var resposta = await reservaService.DeleteReserva(id);
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
    public async Task<ActionResult<ICollection<ReservaDTO>>> GetReserva()
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