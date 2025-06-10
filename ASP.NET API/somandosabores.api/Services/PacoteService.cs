using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using infra.DbContext;
using domain.Models.DTO;
using domain.Enums;
using Microsoft.VisualBasic;

namespace somandosabores.api.Services;

public class PacoteService(ApplicationDbContext context, IPrecificacaoService precificacaoService) : IPacoteService
{
    public async Task<ServiceResponse<PacoteDTO>> GetPacoteById(Guid id)
    {
        var serviceResponse = new ServiceResponse<PacoteDTO>();
        try
        {
            if (id == Guid.Empty)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id inváido";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var pacoteDTO = await context.Pacotes
                            .Include(p => p.Aluno)
                            .Include(p => p.Precificacao)
                            .FirstOrDefaultAsync(p => p.Id == id);

            serviceResponse.Data = new PacoteDTO
            {
                IdPacote = pacoteDTO.Id,
                DataInicio = pacoteDTO.DataInicio,
                DataFinal = pacoteDTO.DataFinal,
                IdAluno = pacoteDTO.AlunoId,
                Quantidade = pacoteDTO.Precificacao.Quantidade,
                Total = pacoteDTO.Precificacao.Total,
                Status = pacoteDTO.Precificacao.Status
            };       

            serviceResponse.Success = true;
            serviceResponse.Message = "Pacote encontrado com sucesso";
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

    public async Task<ServiceResponse<List<PacoteDTO>>> ListPacotes()
    {
        var serviceResponse = new ServiceResponse<List<PacoteDTO>>();
        try
        {
            var pacotesDTO = await context.Pacotes
                                    .Include(p => p.Aluno)
                                    .Include(p => p.Precificacao)
                                    .ToListAsync();

            serviceResponse.Data = pacotesDTO.Select(pacoteDTO => new PacoteDTO
            {
                IdPacote = pacoteDTO.Id,
                DataInicio = pacoteDTO.DataInicio,
                DataFinal = pacoteDTO.DataFinal,
                IdAluno = pacoteDTO.AlunoId,
                Quantidade = pacoteDTO.Precificacao.Quantidade,
                Total = pacoteDTO.Precificacao.Total,
                Status = pacoteDTO.Precificacao.Status
            }).ToList();

            serviceResponse.Success = true;
            serviceResponse.Message = "Pacotes encontrados com sucesso";
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
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
    
    public async Task<ServiceResponse<PacoteDTO>> CreatePacoteDTO(PacoteDTO pacoteDTO)
    {
        var serviceResponse = new ServiceResponse<PacoteDTO>();
        try
        {
            if (pacoteDTO == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Preencha os dados do pacote";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var precificacao = new Precificacao
            {
                TipoServico = OpcoesServico.Pacote,
                Quantidade = pacoteDTO.Quantidade,
                Total = pacoteDTO.Total,
                Status = StatusPrecificacao.Pendente
            };

            var precificacaoResponse = await precificacaoService.CreatePrecificacao(precificacao);
            if (!precificacaoResponse.Success)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = $"Erro no cadastro de precificação: {precificacaoResponse.Message}";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var pacote = new Pacote
            {
                AlunoId = pacoteDTO.IdAluno,
                DataInicio = pacoteDTO.DataInicio,
                DataFinal = pacoteDTO.DataFinal,
                PrecificacaoId = precificacaoResponse.Data.Id  
            };

            var pacoteResponse = await CreatePacote(pacote);
            if (!pacoteResponse.Success)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Erro no cadastro do pacote";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            serviceResponse.Data = new PacoteDTO
            {
                IdPacote = pacoteResponse.Data.Id,
                DataInicio = pacoteResponse.Data.DataInicio,
                DataFinal = pacoteResponse.Data.DataFinal,
                IdAluno = pacoteResponse.Data.AlunoId,
                Quantidade = precificacaoResponse.Data.Quantidade,
                Total = precificacaoResponse.Data.Total
            };

            serviceResponse.Message = "Pacote criado com sucesso";
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
                serviceResponse.Message = "Pacote não encontrado";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var precificacaoId = pacoteExiste.PrecificacaoId;

            context.Pacotes.Remove(pacoteExiste);
            await context.SaveChangesAsync();

            var precificacaoResponse = await precificacaoService.DeletePrecificacao(precificacaoId);
            if (!precificacaoResponse.Success)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Pacote removido, mas falha ao remover Precificação associada: {precificacaoResponse.Message}";
                serviceResponse.Data = null;
                return serviceResponse;
            };

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

    public async Task<ServiceResponse<PacoteDTO>> UpdatePacote(PacoteDTO pacoteDTO)
    {
        var serviceResponse = new ServiceResponse<PacoteDTO>();
        try
        {
            var pacoteExiste = await context.Pacotes.FindAsync(pacoteDTO.IdPacote);
            if (pacoteExiste == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Pacote não encontrado";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            pacoteExiste.DataInicio = pacoteDTO.DataInicio;
            pacoteExiste.DataFinal = pacoteDTO.DataFinal;
            pacoteExiste.AlunoId = pacoteDTO.IdAluno;
            
            var precificacaoAtualizada = new Precificacao
            {
                TipoServico = OpcoesServico.Pacote,
                Quantidade = pacoteDTO.Quantidade,
                Total = pacoteDTO.Total,
                Status = pacoteDTO.Status
            };

            var precificacaoResponse = await precificacaoService.UpdatePrecificacao(precificacaoAtualizada);
            if (!precificacaoResponse.Success || precificacaoResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Precificacao não encontrada";
                serviceResponse.Data = null;
                return serviceResponse;
            };
            
            serviceResponse.Data = new PacoteDTO
            {
                IdPacote = pacoteExiste.Id,
                DataInicio = pacoteExiste.DataInicio,
                DataFinal = pacoteExiste.DataFinal,
                IdAluno = pacoteExiste.AlunoId,
                Quantidade = precificacaoResponse.Data.Quantidade,
                Total = precificacaoResponse.Data.Total,
                Status = precificacaoResponse.Data.Status
            };

            serviceResponse.Success = true;
            serviceResponse.Message = "Pacote atualizado com sucesso";
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