using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using infra.DbContext;

namespace somandosabores.api.Services;

public class ConvidadoService(ApplicationDbContext context) : IConvidadoService
{
    public async Task<ServiceResponse<Convidado>> GetConvidado(Guid id)
    {
        var serviceResponse = new ServiceResponse<Convidado>();
        try
        {
            if (id == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            serviceResponse.Data = context.Convidados.FirstOrDefault(x => x.Id == id);
            serviceResponse.Message = "Convidado Encontrado";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<List<Convidado>>> GetConvidados()
    {
        var serviceResponse = new ServiceResponse<List<Convidado>>();
        try
        {
            serviceResponse.Data = context.Convidados.ToList();
            serviceResponse.Message = "Convidados Encontrados";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Convidado>> UpdateConvidado(Convidado convidado)
    {
        var serviceResponse = new ServiceResponse<Convidado>();
        try
        {
            var convidadoExiste = await context.Convidados.FindAsync(convidado.Id);
            if (convidadoExiste == null)
            {
                serviceResponse.Message = "Convidado não existe";
                serviceResponse.Success = false;
                serviceResponse.Data = null;
                return serviceResponse;
            }
            convidadoExiste.Nome = convidado.Nome ?? convidadoExiste.Nome;

            context.SaveChangesAsync();
            serviceResponse.Data = convidadoExiste;
            serviceResponse.Message = "Convidado Updateado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao atualizar: " + e.Message;
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<string>> DeleteConvidado(Guid id)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var convidadoExiste = await context.Convidados.FindAsync(id);
            if (convidadoExiste == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Convidado não encontrado";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            context.Convidados.Remove(convidadoExiste);
            context.SaveChanges();

            serviceResponse.Data = null;
            serviceResponse.Message = "Convidado Deletado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao deletar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Convidado>> CreateConvidado(Convidado convidado)
    {
        var serviceResponse = new ServiceResponse<Convidado>();
        try
        {
            if (convidado == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Campos obrigatorios não preenchidos!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            await context.Convidados.AddAsync(convidado);
            await context.SaveChangesAsync();

            serviceResponse.Data = convidado;
            serviceResponse.Message = "Convidado criado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao salvar: " + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<List<Convidado>>> CreateConvidados(IEnumerable<Convidado> convidados)
    {
        var serviceResponse = new ServiceResponse<List<Convidado>>();

        try
        {
            context.Convidados.AddRange(convidados);
            await context.SaveChangesAsync();

            serviceResponse.Data = convidados.ToList();
            serviceResponse.Success = true;
            serviceResponse.Message = "Convidados cadastrados com sucesso";
        }
        catch (Exception ex)
        {
            serviceResponse.Message = $"Erro ao cadastrar convidados: {ex.Message}";
            serviceResponse.Success = false;
            serviceResponse.Data = null;
        }
        return serviceResponse;
    }
}