using domain.Models;
using domain.Models.DTO;

namespace domain.IServices;

public interface IPacoteService
{
    Task<ServiceResponse<PacoteDTO>> GetPacoteById(Guid id);
    Task<ServiceResponse<List<PacoteDTO>>> ListPacotes();
    Task<ServiceResponse<Pacote>> CreatePacote(Pacote pacote);
    Task<ServiceResponse<PacoteDTO>> CreatePacoteDTO(PacoteDTO pacoteDTO);
    Task<ServiceResponse<string>> DeletePacote(Guid id);
    Task<ServiceResponse<PacoteDTO>> UpdatePacote(PacoteDTO pacoteDTO);
}