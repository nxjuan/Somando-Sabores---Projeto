using domain.IServices;
using domain.Models;
using infra.DbContext;

namespace somandosabores.api.Services;

public class ReserveService(ApplicationDbContext context) : IReservaService
{
    public async Task<ServiceResponse<Reserva>> CreateReserva(Reserva reserva)
    {
        var serviceResponse = new ServiceResponse<Reserva>();
        try
        {
            if (reserva == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Informar Dados da reserva";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            context.AddAsync<Reserva>(reserva);
            await context.SaveChangesAsync(); 
            
            serviceResponse.Data = reserva;
            serviceResponse.Message = "Reserva cadastrada com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
            
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao cadastrar reserva: " + e.Message;
            serviceResponse.Success = false;
            
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Reserva>> GetReserva(Guid id)
    {
        var serviceResponse = new ServiceResponse<Reserva>();
        try
        {
            if (id == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            serviceResponse.Data = context.Reservas.FirstOrDefault(x => x.Id == id);
            serviceResponse.Message = "Reservas encontradas com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro na busca: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Reserva>> UpdateReserva(Reserva reserva)
    {
        var serviceResponse = new ServiceResponse<Reserva>();
        try
        {
            var reservaAntiga = context.Reservas.FirstOrDefault(x => x.Id == reserva.Id); 
            if (reservaAntiga.Id == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Informar Dados da reserva";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            
            var novaReserva = new Reserva();
            novaReserva.ReservaStatus = reserva.ReservaStatus;
            novaReserva.DataReserva = reserva.DataReserva;
            novaReserva.Evento = reserva.Evento;
            novaReserva.EventoId = reserva.EventoId;
            novaReserva.Valor = reserva.Valor;
            
            context.Update(novaReserva);
            context.SaveChangesAsync();
            
            serviceResponse.Data = novaReserva;
            serviceResponse.Message = "Reserva atualizada com sucesso";
            serviceResponse.Success = true;
            
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao atualizar reserva: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<string>> DeleteReserva(Guid id)
    {
        var serviceRespose = new ServiceResponse<string>();
        try
        {
            if(id == null){
                serviceRespose.Data = null;
                serviceRespose.Message = "Id invalido";
                serviceRespose.Success = false;
            }

            var reserva = await context.Reservas.FindAsync(id);
            if(reserva == null){
                serviceRespose.Data = null;
                serviceRespose.Message = "Id invalido";
                serviceRespose.Success = false;
            }

            context.Remove(reserva);
            context.SaveChangesAsync();

            serviceRespose.Success = true;
            serviceRespose.Message = "deletado com sucesso";
            serviceRespose.Data = null;
            return serviceRespose;
        }
        catch(Exception e)
        {
            serviceRespose.Data = null;
            serviceRespose.Message = "Erro durante deleção: " + e.Message;
            serviceRespose.Success = false;
            return serviceRespose;
        }
    }

    public async Task<ServiceResponse<List<Reserva>>> ListReservas()
    {
        var serviceResponse = new ServiceResponse<List<Reserva>>();
        try
        {
            serviceResponse.Data = context.Reservas.ToList();
            serviceResponse.Message = "Reservas encontradas";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao listar reservas: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }
}