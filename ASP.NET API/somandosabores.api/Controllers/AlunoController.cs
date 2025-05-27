using domain.IServices;
using domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AlunoController(IAlunoService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Aluno>>>> GetAll()
    {
        var retorno = await service.GetAlunos();
        if (retorno.Success)
        {
            return Ok(retorno);
        }
        else
        {
            return BadRequest(retorno);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Reserva>>> GetById(int id)
    {
        var retorno = await service.GetAlunoById(id);
        if (retorno.Success)
        {
            return Ok(retorno);
        }
        else
        {
            return BadRequest(retorno);
        }
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Reserva>>> Post(Aluno aluno)
    {
        var retorno = await service.CreateAluno(aluno);
        if (retorno.Success)
        {
            return Ok(retorno);
        }
        else
        {
            return BadRequest(retorno);
        }
    }

    [HttpPut]
    public async Task<ActionResult<ServiceResponse<Reserva>>> Put(Aluno aluno)
    {
        var retorno = await service.UpdateAluno(aluno);
        if (retorno.Success)
        {
            return Ok(retorno);
        }
        else
        {
            return BadRequest(retorno);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<string>>> Delete(int id)
    {
        var retorno = await service.DeleteAluno(id);
        if (retorno.Success)
        {
            return NotFound(retorno);
        }
        else
        {
            return BadRequest(retorno);
        }
    }
}