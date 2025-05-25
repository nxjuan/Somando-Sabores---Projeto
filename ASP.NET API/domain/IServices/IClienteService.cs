using domain.Models;

namespace domain.IServices;

public interface IClienteService
{
    Task<ServiceResponse<Cliente>> GetCliente(int id);
    Task<ServiceResponse<List<Cliente>>> GetClientes();
    Task<ServiceResponse<Cliente>> UpdateCliente(Cliente cliente);
    Task<ServiceResponse<string>> DeleteCliente(int id);
    Task<ServiceResponse<Cliente>> CreateCliente(Cliente cliente);
}