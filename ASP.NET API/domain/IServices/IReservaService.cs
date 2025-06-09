using domain.Models;
using domain.Models.DTO;

namespace domain.IServices;

public interface IReservaService
{
    Task<ServiceResponse<Reserva>> CreateReserva(Reserva reserva);
    Task<ServiceResponse<ReservaDTO>> CreateReservaDTO(ReservaDTO reservaDTO);
    Task<ServiceResponse<ReservaDTO>> GetReservaDTO(Guid id);
    Task<ServiceResponse<ReservaDTO>> GetReservaDTOByEmail(string email);
    Task<ServiceResponse<ReservaDTO>> UpdateReserva(ReservaDTO reserva);
    Task<ServiceResponse<string>> DeleteReserva(Guid id);
    Task<ServiceResponse<List<ReservaDTO>>> ListReservas();
    
}