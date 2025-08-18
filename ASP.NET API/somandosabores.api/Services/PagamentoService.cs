using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using domain.Enums;
using domain.Models.DTO;
using domain.Models.Common;
using infra.DbContext;

namespace somandosabores.api.Services;

public class PagamentoService (ApplicationDbContext context,
                                IClienteService clienteService,
                                IReservaService reservaService,
                                IPacoteService pacoteService) : IPagamentoService
{
    public async Task<ServiceResponse<PagamentoDTO>> GetPagamentoById(Guid id)
    {
        var serviceResponse = new ServiceResponse<PagamentoDTO>();
        try
        {
            if (id == Guid.Empty)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Data = null;
                return serviceResponse;
            }

            var pagamento = await context.Pagamentos
                                    .Include(p => p.Cliente)
                                    .Include(p => p.Reserva)
                                        .ThenInclude(r => r.Precificacao)
                                    .Include(p => p.Pacote)
                                        .ThenInclude(p => p.Precificacao)
                                    .FirstOrDefaultAsync(p => p.Id == id);

            serviceResponse.Success = true;
            serviceResponse.Message = "Pagamento encontrado";

            OpcoesServico tipoServico = OpcoesServico.Reserva;
            StatusPrecificacao status = StatusPrecificacao.Pendente;
            bool emitirNF = false;

            if (pagamento.Reserva != null && pagamento.ReservaId != Guid.Empty)
            {
                tipoServico = pagamento.Reserva.Precificacao.TipoServico;
                status = pagamento.Reserva.Precificacao.Status;
                emitirNF = pagamento.Reserva.Precificacao.EmitirNF;
            }
            else if (pagamento.Pacote != null && pagamento.PacoteId != Guid.Empty)
            {
                tipoServico = pagamento.Pacote.Precificacao.TipoServico;
                status = pagamento.Pacote.Precificacao.Status;
                emitirNF = pagamento.Pacote.Precificacao.EmitirNF;
            }

            serviceResponse.Data = new PagamentoDTO
            {
                Id = pagamento.Id,
                Nome = pagamento.Cliente?.Nome,
                ValorTotal = pagamento.ValorTotal,
                DataPagamento = pagamento.DataPagamento,
                FormaPagamento = pagamento.FormaPagamento,
                AsaasId = pagamento.AsaasId,
                TipoServico = tipoServico,
                Status = status,
                EmitirNF = emitirNF,
                ReservaId = pagamento.ReservaId,
                PacoteId = pagamento.PacoteId,
                ClienteId = pagamento.ClienteId
            };

            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }
    
    public async Task<ServiceResponse<List<PagamentoDTO>>> GetPagamentos()
    {
        var serviceResponse = new ServiceResponse<List<PagamentoDTO>>();
        try
        {
            var pagamentos = await context.Pagamentos
                                    .Include(p => p.Cliente)
                                    .Include(p => p.Reserva)
                                        .ThenInclude(r => r.Precificacao)
                                    .Include(p => p.Pacote)
                                        .ThenInclude(p => p.Precificacao)
                                    .ToListAsync();

            serviceResponse.Data = pagamentos.Select(pagamento =>
            {
                OpcoesServico tipoServico = OpcoesServico.Reserva;
                StatusPrecificacao status = StatusPrecificacao.Pendente;
                bool emitirNF = false;

                if (pagamento.ReservaId != Guid.Empty)
                {
                    tipoServico = pagamento.Reserva.Precificacao.TipoServico;
                    status = pagamento.Reserva.Precificacao.Status;
                    emitirNF = pagamento.Reserva.Precificacao.EmitirNF;
                }
                else if (pagamento.PacoteId != Guid.Empty)
                {
                    tipoServico = pagamento.Pacote.Precificacao.TipoServico;
                    status = pagamento.Pacote.Precificacao.Status;
                    emitirNF = pagamento.Pacote.Precificacao.EmitirNF;
                }

                return new PagamentoDTO
                {
                    Id = pagamento.Id,
                    Nome = pagamento.Cliente?.Nome,
                    ValorTotal = pagamento.ValorTotal,
                    DataPagamento = pagamento.DataPagamento,
                    FormaPagamento = pagamento.FormaPagamento,
                    AsaasId = pagamento.AsaasId,
                    TipoServico = tipoServico,
                    Status = status,
                    EmitirNF = emitirNF,
                    ReservaId = pagamento.ReservaId,
                    PacoteId = pagamento.PacoteId,
                    ClienteId = pagamento.ClienteId
                };
            }).ToList();

            serviceResponse.Success = true;
            serviceResponse.Message = "Pagamentos encontrados";
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao buscar: " + e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<PagamentoDTO>> CreatePagamento(PagamentoDTO pagamento)
    {
        var serviceResponse = new ServiceResponse<PagamentoDTO>();

        try
        {
            if (pagamento == null ||
                pagamento.ValorTotal <= 39 ||
                pagamento.DataPagamento == DateTime.MinValue ||
                pagamento.FormaPagamento == null ||
                string.IsNullOrEmpty(pagamento.AsaasId)
                )
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Campos obrigatórios não preenchidos!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var clienteExiste = await clienteService.GetCliente(pagamento.ClienteId);
            var reservaExiste = await reservaService.GetReservaDTO(pagamento.ReservaId);
            var pacoteExiste = await pacoteService.GetPacoteById(pagamento.PacoteId);

            // Propriedades vindas de pacote ou reserva
            OpcoesServico tipoServico = OpcoesServico.Reserva;
            StatusPrecificacao status = StatusPrecificacao.Pendente;
            bool emitirNF = false;

            if (pagamento.ReservaId != Guid.Empty && reservaExiste.Success)
            {
                tipoServico = reservaExiste.Data.TipoServico;
                status = reservaExiste.Data.Status;
                emitirNF = reservaExiste.Data.EmitirNF;
                pagamento.PacoteId = null;
            }
            else if (pagamento.PacoteId != Guid.Empty && pacoteExiste.Success)
            {
                tipoServico = OpcoesServico.Pacote;
                status = pacoteExiste.Data.Status;
                emitirNF = true;
                pagamento.ReservaId = null;
            }
            else
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "O pagamento não tem reserva ou pacote associado!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var newPagamento = new Pagamento
            {
                DataPagamento = pagamento.DataPagamento,
                ValorTotal = pagamento.ValorTotal,
                FormaPagamento = pagamento.FormaPagamento,
                AsaasId = pagamento.AsaasId,
                ReservaId = pagamento.ReservaId,
                PacoteId = pagamento.PacoteId,
                ClienteId = pagamento.ClienteId
            };

            await context.Pagamentos.AddAsync(newPagamento);
            await context.SaveChangesAsync();

            serviceResponse.Data = new PagamentoDTO
            {
                Id = newPagamento.Id,
                Nome = clienteExiste.Data.Nome,
                ValorTotal = pagamento.ValorTotal,
                DataPagamento = pagamento.DataPagamento,
                FormaPagamento = pagamento.FormaPagamento,
                AsaasId = pagamento.AsaasId,
                TipoServico = tipoServico,
                Status = status,
                EmitirNF = emitirNF,
                ReservaId = pagamento.ReservaId,
                PacoteId = pagamento.PacoteId,
                ClienteId = pagamento.ClienteId
            };
            serviceResponse.Message = "Pagamento criado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao salvar: " + e.InnerException;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<PagamentoDTO>> UpdatePagamento(PagamentoDTO pagamentoDTO)
    {
        var serviceResponse = new ServiceResponse<PagamentoDTO>();
        try
        {
            if (pagamentoDTO == null ||
                pagamentoDTO.ValorTotal <= 39 ||
                pagamentoDTO.DataPagamento == DateTime.MinValue ||
                pagamentoDTO.FormaPagamento == null ||
                string.IsNullOrEmpty(pagamentoDTO.AsaasId)
                )
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Campos obrigatórios não preenchidos!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var pagamentoExists = await context.Pagamentos
                                        .Include(p => p.Cliente)
                                        .Include(p => p.Reserva)
                                            .ThenInclude(r => r.Precificacao)
                                        .Include(p => p.Pacote)
                                            .ThenInclude(p => p.Precificacao)
                                        .FirstOrDefaultAsync(p => p.Id == pagamentoDTO.Id);

            if (pagamentoExists == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Pagamento não encontrado";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            // Atualização de nome do cliente
            pagamentoExists.Cliente.Nome = pagamentoDTO.Nome;
            var clienteUpdate = await clienteService.UpdateCliente(pagamentoExists.Cliente);
            if (!clienteUpdate.Success)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Erro ao atualizar nome do cliente";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            // Atualização de dados do próprio pagamento
            pagamentoExists.ValorTotal = pagamentoDTO.ValorTotal;
            pagamentoExists.DataPagamento = pagamentoDTO.DataPagamento;
            pagamentoExists.FormaPagamento = pagamentoDTO.FormaPagamento;
            pagamentoExists.AsaasId = pagamentoDTO.AsaasId;

            context.Update(pagamentoExists);

            // Atualização do status da precificação
            if (pagamentoDTO.ReservaId != Guid.Empty)
            {
                var reservaDTOAtualizada = new ReservaDTO
                {
                    Id = pagamentoExists.Reserva.Id,
                    DataReserva = pagamentoExists.Reserva.DataReserva,
                    QtdConvidados = pagamentoExists.Reserva.QtdConvidados,
                    Nome = pagamentoExists.Reserva.Cliente.Nome,
                    Email = pagamentoExists.Reserva.Cliente.Email,
                    IdsConvidados = pagamentoExists.Reserva.Convidados?.Select(c => c.Id).ToList(),
                    NomesConvidados = pagamentoExists.Reserva.Convidados?.Select(c => c.Nome).ToList(),
                    Quantidade = pagamentoExists.Reserva.Precificacao.Quantidade,
                    Total = pagamentoExists.Reserva.Precificacao.Total,
                    Status = pagamentoExists.Reserva.Precificacao.Status,
                    TipoServico = pagamentoExists.Reserva.Precificacao.TipoServico,
                    EmitirNF = pagamentoExists.Reserva.Precificacao.EmitirNF
                };

                var reservaUpdate = await reservaService.UpdateReserva(reservaDTOAtualizada);

                if (!reservaUpdate.Success)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Erro ao atualizar status da reserva";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }

            }
            else if (pagamentoDTO.PacoteId != Guid.Empty)
            {
                var pacoteDTOAtualizado = new PacoteDTO
                {
                    IdPacote = pagamentoExists.Pacote.Id,
                    DataInicio = pagamentoExists.Pacote.DataInicio,
                    DataFinal = pagamentoExists.Pacote.DataFinal,
                    IdAluno = pagamentoExists.Pacote.AlunoId,
                    Nome = pagamentoExists.Pacote.Aluno.Cliente.Nome,
                    Email = pagamentoExists.Pacote.Aluno.Cliente.Email,
                    RA = pagamentoExists.Pacote.Aluno.RA,
                    Quantidade = pagamentoExists.Pacote.Precificacao.Quantidade,
                    Total = pagamentoExists.Pacote.Precificacao.Total,
                    Status = pagamentoExists.Pacote.Precificacao.Status
                };

                var pacoteUpdate = await pacoteService.UpdatePacote(pacoteDTOAtualizado);

                if (!pacoteUpdate.Success)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "Erro ao atualizar status do pacote";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
            }
            else
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "O pagamento não tem reserva ou pacote associado!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            serviceResponse.Data = pagamentoDTO;
            serviceResponse.Success = true;
            serviceResponse.Message = "Pagamento atualizado com sucesso";
            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao atualizar: " + e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }


    public async Task<ServiceResponse<string>> DeletePagamento(Guid id)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var pagamento = await context.Pagamentos.FindAsync(id);

            if (id == Guid.Empty || pagamento != null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Success = false;
            }
            
            context.Pagamentos.Remove(pagamento);
            await context.SaveChangesAsync();

            serviceResponse.Success = true;
            serviceResponse.Message = "Pagamento deletado com sucesso";
            serviceResponse.Data = null;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao deletar pagamento: " + e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }
}