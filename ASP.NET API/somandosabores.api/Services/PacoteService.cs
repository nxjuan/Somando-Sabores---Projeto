using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using infra.DbContext;

namespace somandosabores.api.Services;

public class PacoteService(ApplicationDbContext context) : IPacoteService
{
    public async Task<ServiceResponse<Pacote>> GetPacoteById(Guid id)
    {
        var serviceResponse = new ServiceResponse<Pacote>();
        try
        {
            if (id == null)
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

    public async Task<ServiceResponse<List<Pacote>>> ListPacotes()
    {
        var serviceResponse = new ServiceResponse<List<Pacote>>();
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

    public async Task<ServiceResponse<Pacote>> CreatePacote(Pacote pacote)
    {
        var serviceResponse = new ServiceResponse<Pacote>();
        try
        {
            if (pacote == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Preencha os dados do pacote";
                serviceResponse.Success = false;
                return serviceResponse;
            }

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

    public async Task<ServiceResponse<string>> DeletePacote(Guid id)
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

    public async Task<ServiceResponse<Pacote>> UpdatePacote(Pacote pacote)
    {
        var serviceResponse = new ServiceResponse<Pacote>();
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