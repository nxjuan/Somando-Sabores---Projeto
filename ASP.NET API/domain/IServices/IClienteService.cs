using domain.Models;

namespace domain.IServices;

public interface IClienteService
{
    Task<ServiceResponse<Cliente>> GetCliente(Guid id);
    Task<ServiceResponse<List<Cliente>>> GetClientes();
    Task<ServiceResponse<Cliente>> UpdateCliente(Cliente cliente);
    Task<ServiceResponse<string>> DeleteCliente(Guid id);
    Task<ServiceResponse<Cliente>> CreateCliente(Cliente cliente);
}