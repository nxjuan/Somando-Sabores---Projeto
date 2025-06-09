using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using infra.DbContext;

namespace somandosabores.api.Services;

public class PrecificacaoService(ApplicationDbContext context) : IPrecificacaoService
{
    public async Task<ServiceResponse<Precificacao>> GetPrecificacao(Guid id)
    {
        var serviceResponse = new ServiceResponse<Precificacao>();
        try
        {
            if (id == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Success = false;
                return serviceResponse;
            } 
            
            serviceResponse.Data = context.Precificacoes.FirstOrDefault(x => x.Id == id);
            serviceResponse.Message = "Precificacao Encontrada";
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

    public async Task<ServiceResponse<List<Precificacao>>> GetPrecificacoes()
    {
        var serviceResponse = new ServiceResponse<List<Precificacao>>();
        try
        {
            serviceResponse.Data = context.Precificacoes.ToList();
            serviceResponse.Message = "Precificações Encontradas";
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

    public async Task<ServiceResponse<Precificacao>> UpdatePrecificacao(Precificacao precificacao)
    {
        var  serviceResponse = new ServiceResponse<Precificacao>();
        try
        {
            var precificacaoExiste = await context.Precificacoes.FindAsync(precificacao.Id);
            if (precificacaoExiste == null)
            {
                serviceResponse.Message = "Precificação não existe";
                serviceResponse.Success = false;
                serviceResponse.Data = null;
                return serviceResponse;
            }

            precificacaoExiste.TipoServico = precificacao.TipoServico;
            precificacaoExiste.Quantidade = precificacao.Quantidade;
            precificacaoExiste.Total = precificacao.Total;
            precificacaoExiste.Status = precificacao.Status;
            precificacaoExiste.EmitirNF = precificacao.EmitirNF;

            context.SaveChangesAsync();
            serviceResponse.Data = precificacaoExiste;
            serviceResponse.Message = "Precificacao Updateada com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao atualizar: "  + e.Message;
            serviceResponse.Success = false;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<string>> DeletePrecificacao(Guid id)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var precificacaoExiste = await context.Precificacoes.FindAsync(id);
            if (precificacaoExiste == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Precificação não encontrada";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            context.Precificacoes.Remove(precificacaoExiste);
            context.SaveChanges();
            
            serviceResponse.Data = null;
            serviceResponse.Message = "Precificação deletada com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao deletar: "  + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Precificacao>> CreatePrecificacao(Precificacao precificacao)
    {
        var serviceResponse = new ServiceResponse<Precificacao>();
        try
        {
            if (precificacao == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Campos obrigatorios não preenchidos!";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            await context.Precificacoes.AddAsync(precificacao);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = precificacao;
            serviceResponse.Message = "Precificação criada com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao salvar: "  + e.InnerException;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }
}