using domain.Models;

namespace domain.IServices;

public interface IEventoService
{
    Task<ServiceResponse<Evento>> CreateEvento(Evento evento);
    Task<ServiceResponse<string>> DeleteEvento(Guid id);
    Task<ServiceResponse<Evento>> UpdateEvento(Evento evento);
    Task<ServiceResponse<Evento>> GetEvento(Guid id);
    Task<ServiceResponse<List<Evento>>> GetEventos();
}