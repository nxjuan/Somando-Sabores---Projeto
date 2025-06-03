using domain.Models;

namespace domain.IServices;

public interface IReservaService
{
    Task<ServiceResponse<Reserva>> CreateReserva(Reserva reserva);
    Task<ServiceResponse<Reserva>> GetReserva(Guid id);
    Task<ServiceResponse<Reserva>> UpdateReserva(Reserva reserva);
    Task<ServiceResponse<string>> DeleteReserva(Guid id);
    Task<ServiceResponse<List<Reserva>>> ListReservas();
    
}