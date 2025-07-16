using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using domain.Models.DTO;
using domain.Enums;
using infra.DbContext;

namespace somandosabores.api.Services;

public class ReservaService(ApplicationDbContext context,
                            IClienteService clienteService,
                            IPrecificacaoService precificacaoService,
                            IConvidadoService convidadoService) : IReservaService
{
    public async Task<ServiceResponse<Reserva>> CreateReserva(Reserva reserva)
    {
        var serviceResponse = new ServiceResponse<Reserva>();
        try
        {
            if (reserva == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Campos obrigatórios não preenchidos!";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            await context.Reservas.AddAsync(reserva);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = reserva;
            serviceResponse.Message = "Reserva criada com sucesso";
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

    public async Task<ServiceResponse<ReservaDTO>> CreateReservaDTO(ReservaDTO reservaDTO)
    {
        var serviceResponse = new ServiceResponse<ReservaDTO>();
        
        try
        {
            if (reservaDTO == null ||
                string.IsNullOrWhiteSpace(reservaDTO.CpfOuCnpj) ||
                string.IsNullOrWhiteSpace(reservaDTO.Nome) ||
                string.IsNullOrWhiteSpace(reservaDTO.Email)
                )
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Informar Dados da reserva";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            // Checa se o cliente já existe no BD
            var clienteExiste = await clienteService.GetClienteByEmail(reservaDTO.Email);
            Guid idDoCliente;
            string nomeCliente;
            string emailCliente;

            if (clienteExiste.Success && clienteExiste.Data != null)
            {
                idDoCliente = clienteExiste.Data.Id;
                nomeCliente = clienteExiste.Data.Nome;
                emailCliente = clienteExiste.Data.Email;
            }
            else
            {
                // Envio de dados do cliente
                var cliente = new Cliente
                {
                    Nome = reservaDTO.Nome,
                    Email = reservaDTO.Email
                };

                var clienteResponse = await clienteService.CreateCliente(cliente);

                if (clienteResponse.Success && clienteResponse.Data != null)
                {
                    idDoCliente = clienteResponse.Data.Id;
                    nomeCliente = clienteResponse.Data.Nome;
                    emailCliente = clienteResponse.Data.Email;
                }
                else
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = $"Erro no cadastro do cliente: {clienteResponse.Message}";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
            }

            // Envio de dados da Precificacao
            var precificacao = new Precificacao
            {
                TipoServico = OpcoesServico.Reserva,
                Quantidade = reservaDTO.Quantidade,
                Total = reservaDTO.Total,
                Status = StatusPrecificacao.Pendente,
                EmitirNF = reservaDTO.EmitirNF
            };

            var precificacaoResponse = await precificacaoService.CreatePrecificacao(precificacao);

            if (!precificacaoResponse.Success)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = $"Erro no cadastro da precificação: {precificacaoResponse.Message}`";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            // Envia Dados de Reserva
            var reserva = new Reserva
            {
                DataReserva = reservaDTO.DataReserva,
                QtdConvidados = reservaDTO.QtdConvidados,
                ClienteId = idDoCliente,
                PrecificacaoId = precificacaoResponse.Data.Id,
            };

            var reservaResponse = await CreateReserva(reserva);

            var convidadosParaInserir = new List<Convidado>();

            if (reservaDTO.QtdConvidados > 0)
            {
                for (int i = 0; i < reservaDTO.QtdConvidados; i++)
                {
                    var convidado = new Convidado
                    {
                        Nome = reservaDTO.NomesConvidados[i],
                        ReservaId = reservaResponse.Data.Id
                    };
                    convidadosParaInserir.Add(convidado);
                }

                // Inserção em Lote
                var convidadosResponse = await convidadoService.CreateConvidados(convidadosParaInserir);

                if (!convidadosResponse.Success)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = $"Erro no cadastro dos convidados: {convidadosResponse.Message}";
                    serviceResponse.Success = false;
                    return serviceResponse;
                }
            }

            serviceResponse.Data = new ReservaDTO
            {
                Id = reservaResponse.Data.Id,
                CpfOuCnpj = reservaDTO.CpfOuCnpj,
                DataReserva = reservaResponse.Data.DataReserva,
                QtdConvidados = reservaResponse.Data.QtdConvidados,

                Nome = nomeCliente,
                Email = emailCliente,

                NomesConvidados = reservaDTO.NomesConvidados,

                Quantidade = precificacaoResponse.Data.Quantidade,
                Total = precificacaoResponse.Data.Total,
                Status = precificacaoResponse.Data.Status,
                TipoServico = precificacaoResponse.Data.TipoServico,
                EmitirNF = precificacaoResponse.Data.EmitirNF
            };

            serviceResponse.Message = "Reserva cadastrada com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = $"Erro ao cadastrar reserva: {e.Message}";
            serviceResponse.Success = false;

            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<ReservaDTO>> GetReservaDTO(Guid id)
    {
        var serviceResponse = new ServiceResponse<ReservaDTO>();
        try
        {
            if (id == Guid.Empty)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var reserva = await context.Reservas
                                    .Include(r => r.Cliente)
                                    .Include(r => r.Precificacao)
                                    .Include(r => r.Convidados)
                                    .FirstOrDefaultAsync(a => a.Id == id);

            serviceResponse.Data = new ReservaDTO
            {
                Id = reserva.Id,
                //CpfOuCnpj = reserva.CpfOuCnpj,
                DataReserva = reserva.DataReserva,
                QtdConvidados = reserva.QtdConvidados,

                Nome = reserva.Cliente.Nome,
                Email = reserva.Cliente.Email,

                NomesConvidados = reserva.Convidados?.Select(c => c.Nome).ToList(),

                Quantidade = reserva.Precificacao.Quantidade,
                Total = reserva.Precificacao.Total,
                Status = reserva.Precificacao.Status,
                TipoServico = reserva.Precificacao.TipoServico,
                EmitirNF = reserva.Precificacao.EmitirNF
            };

            serviceResponse.Message = "Reserva encontrada com sucesso";
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

    public async Task<ServiceResponse<ReservaDTO>> UpdateReserva(ReservaDTO reservaDTO)
    {
        var serviceResponse = new ServiceResponse<ReservaDTO>();
        try
        {
            if (reservaDTO == null ||
                reservaDTO.Id == Guid.Empty ||
                string.IsNullOrWhiteSpace(reservaDTO.CpfOuCnpj) ||
                string.IsNullOrWhiteSpace(reservaDTO.Nome) ||
                string.IsNullOrWhiteSpace(reservaDTO.Email))
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Informar Dados da reserva";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            var reservaExistente = await context.Reservas
                                        .Include(r => r.Cliente)
                                        .Include(r => r.Precificacao)
                                        .Include(r => r.Convidados)
                                        .FirstOrDefaultAsync(r => r.Id == reservaDTO.Id);

            if (reservaExistente == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Reserva não encontrada";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            // Cliente
            reservaExistente.Cliente.Nome = reservaDTO.Nome;
            reservaExistente.Cliente.Email = reservaDTO.Email;

            var clienteUpdate = await clienteService.UpdateCliente(reservaExistente.Cliente);

            // Precificação
            reservaExistente.Precificacao.Quantidade = reservaDTO.Quantidade;
            reservaExistente.Precificacao.Total = reservaDTO.Total;
            reservaExistente.Precificacao.Status = reservaDTO.Status;
            reservaExistente.Precificacao.TipoServico = reservaDTO.TipoServico;
            reservaExistente.Precificacao.EmitirNF = reservaDTO.EmitirNF;

            var precificacaoUpdate = await precificacaoService.UpdatePrecificacao(reservaExistente.Precificacao);

            if (!clienteUpdate.Success || !precificacaoUpdate.Success)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Erro na atualização dos dados. Verifique as informações novamente.";
                serviceResponse.Success = false;
                return serviceResponse;
            }

            // Reserva
                reservaExistente.DataReserva = reservaDTO.DataReserva;
            reservaExistente.QtdConvidados = reservaDTO.QtdConvidados;

            if (reservaDTO.QtdConvidados > 0)
            {
                var nomesNoDto = reservaDTO.NomesConvidados ?? new List<string>();
                var convidadosParaRemover = reservaExistente.Convidados
                                                        .Where(c => !nomesNoDto.Contains(c.Nome))
                                                        .ToList();

                foreach (var convidado in convidadosParaRemover)
                {
                    await convidadoService.DeleteConvidado(convidado.Id);
                }

                var nomesAtualmenteNoBanco = reservaExistente.Convidados.Select(c => c.Nome).ToList();
                var nomesParaAdicionar = nomesNoDto.Where(nome => !nomesAtualmenteNoBanco.Contains(nome)).ToList();

                foreach (var nomeNovo in nomesParaAdicionar)
                {
                    var novoConvidado = new Convidado
                    {
                        Nome = nomeNovo,
                        ReservaId = reservaExistente.Id
                    };
                    await convidadoService.CreateConvidado(novoConvidado);
                }
            }

            context.Update(reservaExistente);
            await context.SaveChangesAsync();

            var reservaAtualizada = await context.Reservas
                                                .Include(r => r.Cliente)
                                                .Include(r => r.Precificacao)
                                                .Include(r => r.Convidados)
                                                .FirstOrDefaultAsync(r => r.Id == reservaDTO.Id);

            serviceResponse.Data = new ReservaDTO
            {
                Id = reservaAtualizada.Id,
                DataReserva = reservaAtualizada.DataReserva,
                QtdConvidados = reservaAtualizada.QtdConvidados,

                Nome = reservaAtualizada.Cliente?.Nome,
                Email = reservaAtualizada.Cliente?.Email,

                NomesConvidados = reservaAtualizada.Convidados?.Select(c => c.Nome).ToList(),

                Quantidade = reservaAtualizada.Precificacao.Quantidade,
                Total = reservaAtualizada.Precificacao.Total,
                Status = reservaAtualizada.Precificacao.Status,
                TipoServico = reservaAtualizada.Precificacao.TipoServico,
                EmitirNF = reservaAtualizada.Precificacao.EmitirNF
            };

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
            var reserva = await context.Reservas.FindAsync(id);
            if (id == Guid.Empty|| reserva == null)
            {
                serviceRespose.Data = null;
                serviceRespose.Message = "Id invalido";
                serviceRespose.Success = false;
            }

            context.Reservas.Remove(reserva);
            await context.SaveChangesAsync();

            serviceRespose.Success = true;
            serviceRespose.Message = "deletado com sucesso";
            serviceRespose.Data = null;
            return serviceRespose;
        }
        catch (Exception e)
        {
            serviceRespose.Data = null;
            serviceRespose.Message = "Erro durante deleção: " + e.Message;
            serviceRespose.Success = false;
            return serviceRespose;
        }
    }

    public async Task<ServiceResponse<List<ReservaDTO>>> ListReservas()
    {
        var serviceResponse = new ServiceResponse<List<ReservaDTO>>();
        try
        {
            var reservas = await context.Reservas
                                    .Include(a => a.Cliente)
                                    .Include(r => r.Precificacao)
                                    .Include(r => r.Convidados)
                                    .ToListAsync();

            serviceResponse.Data = reservas.Select(reserva => new ReservaDTO
            {
                Id = reserva.Id,
                //CpfOuCnpj = reserva.CpfOuCnpj,
                DataReserva = reserva.DataReserva,
                QtdConvidados = reserva.QtdConvidados,

                Nome = reserva.Cliente.Nome,
                Email = reserva.Cliente.Email,

                NomesConvidados = reserva.Convidados?.Select(c => c.Nome).ToList(),

                Quantidade = reserva.Precificacao.Quantidade,
                Total = reserva.Precificacao.Total,
                Status = reserva.Precificacao.Status,
                TipoServico = reserva.Precificacao.TipoServico,
                EmitirNF = reserva.Precificacao.EmitirNF
            }).ToList();

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