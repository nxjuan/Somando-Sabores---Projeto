using domain.Models;

namespace domain.IServices;

public interface IPacoteService
{
    Task<ServiceResponse<Pacote>> GetPacoteById(Guid id);
    Task<ServiceResponse<List<Pacote>>> ListPacotes();
    Task<ServiceResponse<Pacote>> CreatePacote(Pacote pacote);
    Task<ServiceResponse<string>> DeletePacote(Guid id);
    Task<ServiceResponse<Pacote>> UpdatePacote(Pacote pacote);
}