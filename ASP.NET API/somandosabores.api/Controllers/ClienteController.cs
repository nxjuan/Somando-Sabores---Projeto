using domain.IServices;
using domain.Models;
using domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace somandosabores.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ClienteController(IClienteService service) : ControllerBase
{
    [HttpGet("{email}")]
    public async Task<ActionResult<ServiceResponse<Cliente>>> GetById(string email)
    {
        var retorno = await service.GetClienteByEmail(email);
        if (retorno.Success)
        {
            return Ok(retorno);
        }
        else
        {
            return BadRequest(retorno);
        }
    }
}