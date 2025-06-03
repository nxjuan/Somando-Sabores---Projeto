using domain.Models;

namespace domain.IServices;

public interface IPacoteService
{
    Task<ServiceResponse<Pacotes>> GetPacoteById(Guid id);
    Task<ServiceResponse<List<Pacotes>>> ListPacotes();
    Task<ServiceResponse<Pacotes>> CreatePacote(Pacotes pacote);
    Task<ServiceResponse<string>> DeletePacote(Guid id);
    Task<ServiceResponse<Pacotes>> UpdatePacote(Pacotes pacote);
}