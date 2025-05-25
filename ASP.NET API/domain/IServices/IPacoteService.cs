using domain.Models;

namespace domain.IServices;

public interface IPacoteService
{
    Task<ServiceResponse<Pacotes>> GetPacoteById(int id);
    Task<ServiceResponse<List<Pacotes>>> ListPacotes();
    Task<ServiceResponse<Pacotes>> CreatePacote(Pacotes pacote);
    Task<ServiceResponse<string>> DeletePacote(int id);
    Task<ServiceResponse<Pacotes>> UpdatePacote(Pacotes pacote);
}