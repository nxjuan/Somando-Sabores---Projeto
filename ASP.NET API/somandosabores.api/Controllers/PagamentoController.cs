using domain.IServices;
using domain.Models;
using domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PagamentoController(IPagamentoService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<PagamentoDTO>>>> GetAll()
    {
        var retorno = await service.GetPagamentos();
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
    public async Task<ActionResult<ServiceResponse<PagamentoDTO>>> GetById(Guid id)
    {
        var retorno = await service.GetPagamentoById(id);
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
    public async Task<ActionResult<ServiceResponse<PagamentoDTO>>> Post(PagamentoDTO pagamento)
    {
        var retorno = await service.CreatePagamento(pagamento);
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
    public async Task<ActionResult<ServiceResponse<PagamentoDTO>>> Put(PagamentoDTO pagamentoDTO)
    {
        var retorno = await service.UpdatePagamento(pagamentoDTO);
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
        var retorno = await service.DeletePagamento(id);
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