using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using infra.DbContext;

namespace somandosabores.api.Services;


public class EventoService(ApplicationDbContext context) : IEventoService
{
    public async Task<ServiceResponse<Evento>> CreateEvento(Evento evento)
    {
        var serviceResponse = new ServiceResponse<Evento>();
        try
        {
            if (evento == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Informe os dados do evento";
                serviceResponse.Data = null;
                 
            }
            await context.Eventos.AddAsync(evento);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = evento;
            serviceResponse.Success = true;
            serviceResponse.Message = "Evento criado com sucesso";
            
            return serviceResponse;
        }
        catch(Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao cadastrar evento: : " + e.Message;
            serviceResponse.Success = false;
            
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<string>> DeleteEvento(Guid id)
    {
        var serviceResponse = new ServiceResponse<string>();

        try
        {
            var eventoExiste = context.Eventos.FirstOrDefault(x => x.Id == id);
            if (eventoExiste == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            
            context.Eventos.Remove(eventoExiste);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = null;
            serviceResponse.Message = "Evento removido com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
            
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao deletar evento: : " + e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Evento>> GetEvento(Guid id)
    {
        var serviceResponse = new ServiceResponse<Evento>();
        try
        {
            var eventoExiste = context.Eventos.FirstOrDefault(x => x.Id == id);
            if (eventoExiste == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Data = null;    
                return serviceResponse; 
            }
            serviceResponse.Data = eventoExiste;
            serviceResponse.Success = true;
            serviceResponse.Message = "Evento encontrado";
            return serviceResponse;
            
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "erro ao buscar evento: " + e.Message;
            serviceResponse.Data = null;    
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<List<Evento>>> GetEventos()
    {
        var serviceResponse = new ServiceResponse<List<Evento>>();
        try
        {
            serviceResponse.Data = context.Eventos.ToList();
            serviceResponse.Success = true;
            serviceResponse.Message = "Eventos encontrado";
            
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "erro ao buscar eventos: " + e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Evento>> UpdateEvento(Evento evento)
    {
       var serviceResponse = new ServiceResponse<Evento>();
       try
       {
            var eventoExiste = context.Eventos.FirstOrDefault(x => x.Id == evento.Id);
            if (eventoExiste == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            eventoExiste.Descricao = evento.Descricao ?? eventoExiste.Descricao;
            eventoExiste.Titulo = evento.Titulo ?? eventoExiste.Titulo;
            eventoExiste.Status = evento.Status ?? eventoExiste.Status; ;
            eventoExiste.DataFim = evento.DataFim ?? eventoExiste.DataFim;
            
            await context.SaveChangesAsync();
            serviceResponse.Data = eventoExiste;  
            serviceResponse.Success = true;
            serviceResponse.Message = "Evento atualizado com sucesso";
            
            return serviceResponse;
       }
       catch (Exception e)
       {
           serviceResponse.Success = false;
           serviceResponse.Message = "Erro ao atualizar evento: "  + e.Message;
           serviceResponse.Data = null;
           return serviceResponse;
       }
    }
}
