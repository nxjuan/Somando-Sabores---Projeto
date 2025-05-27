using domain.IServices;
using domain.Models;
using infra.DbContext;

namespace somandosabores.api.Services;

public class PacoteService(ApplicationDbContext context) : IPacoteService
{
    public async Task<ServiceResponse<Pacotes>> GetPacoteById(int id)
    {
        var serviceResponse = new ServiceResponse<Pacotes>();
        try
        {
            if (id < 1 || id == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id inváido";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            serviceResponse.Data = await context.Pacotes.FindAsync(id);
            serviceResponse.Success = true;
            serviceResponse.Message = "Encontrado com sucesso";
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao busscar: " + e.Message;
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<List<Pacotes>>> ListPacotes()
    {
        var serviceResponse = new ServiceResponse<List<Pacotes>>();
        try
        {
            serviceResponse.Data = context.Pacotes.ToList();
            serviceResponse.Success = true;
            serviceResponse.Message = "Encontrado com sucesso";
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao busscar: " + e.Message;
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Pacotes>> CreatePacote(Pacotes pacote)
    {
        var serviceResponse = new ServiceResponse<Pacotes>();
        try
        {
            if (pacote.DataInicio == null || pacote.DataFim == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Adicione as datas de inicio e fim";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            pacote.QtdDias = (pacote.DataFim.DayNumber - pacote.DataInicio.DayNumber);

            await context.Pacotes.AddAsync(pacote);
            await context.SaveChangesAsync();

            serviceResponse.Data = pacote;
            serviceResponse.Message = "criado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao salvar: " + e.Message;
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<string>> DeletePacote(int id)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var pacoteExiste = await context.Pacotes.FindAsync(id);
            if (pacoteExiste == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Pacote no encontrado";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            context.Pacotes.Remove(pacoteExiste);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = null;
            serviceResponse.Message = "Removido com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao deletar: " + e.Message;
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Pacotes>> UpdatePacote(Pacotes pacote)
    {
        var serviceResponse = new ServiceResponse<Pacotes>();
        try
        {
            var pacoteExiste = await context.Pacotes.FindAsync(pacote.Id);
            if (pacoteExiste == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Pacote no encontrado";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            pacoteExiste.DataInicio = pacote.DataInicio;
            pacoteExiste.DataFim =  pacote.DataFim;
            
            serviceResponse.Data = pacote;
            serviceResponse.Success = true;
            serviceResponse.Message = "Atualizado com sucesso";
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao atualizar: "  + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }
}