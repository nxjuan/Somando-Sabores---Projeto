using domain.IServices;
using domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AlunoController(IAlunoService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<AlunoDTO>>>> GetAll()
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
    public async Task<ActionResult<ServiceResponse<AlunoDTO>>> GetById(Guid id)
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
    public async Task<ActionResult<ServiceResponse<AlunoDTO>>> Post(AlunoDTO alunoDTO)
    {
        var retorno = await service.CreateAluno(alunoDTO);
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
    public async Task<ActionResult<ServiceResponse<AlunoDTO>>> Put(AlunoDTO alunoDTO)
    {
        var retorno = await service.UpdateAluno(alunoDTO);
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
    public async Task<ActionResult<ServiceResponse<string>>> Delete(Guid id)
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