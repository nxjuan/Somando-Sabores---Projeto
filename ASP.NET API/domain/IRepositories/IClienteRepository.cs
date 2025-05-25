using domain.Models;

namespace domain.IRepositories;

public interface IClienteRepository
{
    Task<Cliente> AddAsync(Cliente cliente);
    Task<string> Delete(int id);
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente> GetByIdAsync(int id);
    Task<Cliente> Update(Cliente cliente);
}