using domain.IServices;
using domain.Models;
using Microsoft.AspNetCore.Mvc;
using somandosabores.api.Services;

namespace somandosabores.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class EventoController(IEventoService service) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Evento>>> Post(Evento evento)
    {
        var resposta = await service.CreateEvento(evento);
        if (resposta.Success)
        {
            return Ok(resposta);
        }
        else
        {
            return BadRequest(resposta);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<ICollection<Evento>>>> GetAll()
    {
        var resposta = await service.GetEventos();
        if (resposta.Success)
        {
            return Ok(resposta);
        }
        else
        {
            return Problem(resposta.Message, statusCode: 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Evento>>> GetById(int id)
    {
        var resposta = await service.GetEvento(id);
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
    public async Task<ActionResult<ServiceResponse<Evento>>> Put(Evento evento)
    {
        var resposta = await service.UpdateEvento(evento);
        if (resposta.Success)
        {
            return Ok(resposta);
        }
        else
        {
            return BadRequest(resposta);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
    {
        var resposta = await service.DeleteEvento(id);
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