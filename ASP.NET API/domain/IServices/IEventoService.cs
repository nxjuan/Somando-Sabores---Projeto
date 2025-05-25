using domain.Models;

namespace domain.IServices;

public interface IEventoService
{
    Task<ServiceResponse<Evento>> CreateEvento(Evento evento);
    Task<ServiceResponse<string>> DeleteEvento(int id);
    Task<ServiceResponse<Evento>> UpdateEvento(Evento evento);
    Task<ServiceResponse<Evento>> GetEvento(int id);
    Task<ServiceResponse<List<Evento>>> GetEventos();
}