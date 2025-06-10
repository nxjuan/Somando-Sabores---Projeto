using domain.IServices;
using domain.Models;
using domain.Models.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;
[ApiController]
[Route("/api/[controller]")]
public class PacoteController(IPacoteService pacoteService) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<PacoteDTO>>> GetById(Guid id)
    {
        var resposta = await pacoteService.GetPacoteById(id);
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
    public async Task<ActionResult<ServiceResponse<PacoteDTO>>> Post(PacoteDTO pacoteDTO)
    {
        var resposta = await pacoteService.CreatePacoteDTO(pacoteDTO);
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
    public async Task<ActionResult<ServiceResponse<PacoteDTO>>> Put(PacoteDTO pacoteDTO)
    {
        var resposta = await pacoteService.UpdatePacote(pacoteDTO);
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
        var  resposta = await pacoteService.DeletePacote(id);
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
    public async Task<ActionResult<ICollection<PacoteDTO>>> GetPacotes()
    {
        var resposta = await pacoteService.ListPacotes();
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