using domain.Models;

namespace domain.IServices;

public interface IReservaService
{
    Task<ServiceResponse<Reserva>> CreateReserva(Reserva reserva);
    Task<ServiceResponse<Reserva>> GetReserva(int id);
    Task<ServiceResponse<Reserva>> UpdateReserva(Reserva reserva);
    Task<ServiceResponse<string>> DeleteReserva(int id);
    Task<ServiceResponse<List<Reserva>>> ListReservas();
    
}