using Microsoft.EntityFrameworkCore;
using domain.IServices;
using domain.Models;
using domain.Models.DTO;
using infra.DbContext;

namespace somandosabores.api.Services;

public class AlunoService(ApplicationDbContext context, IClienteService clienteService) : IAlunoService
{
    public async Task<ServiceResponse<AlunoDTO>> GetAlunoById(Guid id)
    {
        var serviceResponse = new ServiceResponse<AlunoDTO>();
        try
        {
            if (id == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            
            var aluno = await context.Alunos
                                    .Include(a => a.Cliente)
                                    .FirstOrDefaultAsync(a => a.Id == id);
            serviceResponse.Success = true;
            serviceResponse.Message = "Aluno encontrado";
            
            serviceResponse.Data = new AlunoDTO
            {
                Id = aluno.Id,
                RA = aluno.RA,
                Nome = aluno.Cliente?.Nome,
                Email = aluno.Cliente?.Email
            };

            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao buscar: " +  e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }
    
    public async Task<ServiceResponse<AlunoDTO>> GetAlunoByRA(string ra)
    {
        var serviceResponse = new ServiceResponse<AlunoDTO>();
        try
        {
            if (string.IsNullOrWhiteSpace(ra))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "RA inválido";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            
            var aluno = await context.Alunos
                                    .Include(a => a.Cliente)
                                    .FirstOrDefaultAsync(a => a.RA == ra);
            serviceResponse.Success = true;
            serviceResponse.Message = "Aluno encontrado";
            
            serviceResponse.Data = new AlunoDTO
            {
                Id = aluno.Id,
                RA = aluno.RA,
                Nome = aluno.Cliente?.Nome,
                Email = aluno.Cliente?.Email
            };

            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao buscar: " +  e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<List<AlunoDTO>>> GetAlunos()
    {
        var serviceResponse = new ServiceResponse<List<AlunoDTO>>();
        try
        {
            var alunos = await context.Alunos
                                    .Include(a => a.Cliente)
                                    .ToListAsync();

            serviceResponse.Data = alunos.Select(aluno => new AlunoDTO
            {
                Id = aluno.Id,
                RA = aluno.RA,
                Nome = aluno.Cliente?.Nome,
                Email = aluno.Cliente?.Email
            }).ToList();

            serviceResponse.Success = true;
            serviceResponse.Message = "Alunos encontrados";
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

    public async Task<ServiceResponse<Aluno>> CreateAluno(Aluno aluno)
    {
        var serviceResponse = new ServiceResponse<Aluno>();
        try
        {
            if (aluno == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Campos obrigatórios não preenchidos!";
                serviceResponse.Success = false;
                return serviceResponse;
            }
            
            await context.Alunos.AddAsync(aluno);
            await context.SaveChangesAsync();
            
            serviceResponse.Data = aluno;
            serviceResponse.Message = "Aluno criado com sucesso";
            serviceResponse.Success = true;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Data = null;
            serviceResponse.Message = "Erro ao salvar: "  + e.Message;
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<AlunoDTO>> CreateAlunoDTO(AlunoDTO alunoDTO)
    {
        var serviceResponse = new ServiceResponse<AlunoDTO>();
        try
        {
            if (
                string.IsNullOrWhiteSpace(alunoDTO.Nome) ||
                string.IsNullOrWhiteSpace(alunoDTO.Email) ||
                string.IsNullOrWhiteSpace(alunoDTO.RA))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Informe os dados do aluno";
                serviceResponse.Data = null;
                return serviceResponse;
            };

            // Checa se o aluno já existe
            var alunoExists = await GetAlunoByRA(alunoDTO.RA);
            if (alunoExists.Success && alunoExists.Data != null)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Aluno já cadastrado";
                serviceResponse.Data = null;
                return serviceResponse;
            }

            // Checa se o cliente já existe
            var clienteExiste = await clienteService.GetClienteByEmail(alunoDTO.Email);
            Guid idCliente;
            string nomeCliente;
            string emailCliente;

            if (clienteExiste.Success && clienteExiste.Data != null)
            {
                idCliente = clienteExiste.Data.Id;
                nomeCliente = clienteExiste.Data.Nome;
                emailCliente = clienteExiste.Data.Email;
            }
            else
            {
                var cliente = new Cliente
            {
                Nome = alunoDTO.Nome,
                Email = alunoDTO.Email
            };

            var clienteServiceResponse = await clienteService.CreateCliente(cliente);

                if (clienteServiceResponse.Success && clienteServiceResponse.Data != null)
                {
                    idCliente = clienteServiceResponse.Data.Id;
                    nomeCliente = clienteServiceResponse.Data.Nome;
                    emailCliente = clienteServiceResponse.Data.Email;

                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Falha ao criar o cliente: {clienteServiceResponse.Message}";
                    return serviceResponse;
                }           
            }

            var aluno = new Aluno
            {
                RA = alunoDTO.RA,
                ClienteId = idCliente
            };

            var alunoResponse = await CreateAluno(aluno);

            serviceResponse.Data = new AlunoDTO
            {
                Id = alunoResponse.Data.Id,
                RA = alunoResponse.Data.RA,
                Nome = nomeCliente,
                Email = emailCliente
            };
            serviceResponse.Success = true;
            serviceResponse.Message = "Aluno criado com sucesso";
            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            string errormsg = e.InnerException?.Message ?? e.Message;
            serviceResponse.Message = "Erro ao salvar: " + errormsg;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<AlunoDTO>> UpdateAluno(AlunoDTO alunoDTO)
    {
        var serviceResponse = new ServiceResponse<AlunoDTO>();
        try
        {
            var alunoExists = await context.Alunos.FindAsync(alunoDTO.Id);
            if (alunoExists == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Aluno não encontrado";
                serviceResponse.Data = null;
                return serviceResponse;
            };
            
            alunoExists.RA = alunoDTO.RA ?? alunoExists.RA;

            var clienteAtualizado = new Cliente
            {
                Id = alunoExists.ClienteId,
                Nome = alunoDTO.Nome,
                Email = alunoDTO.Email
            };

            var clienteServiceResponse = await clienteService.UpdateCliente(clienteAtualizado);
            if (!clienteServiceResponse.Success || clienteServiceResponse.Data == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Cliente não encontrado";
                serviceResponse.Data = null;
                return serviceResponse;
            };

            //await context.SaveChangesAsync();
            
            serviceResponse.Data = new AlunoDTO
            {
                Id = alunoExists.Id,
                RA = alunoExists.RA,
                Nome = clienteServiceResponse.Data.Nome,
                Email = clienteServiceResponse.Data.Email
            };
            serviceResponse.Success = true;
            serviceResponse.Message = "Aluno atualizado com sucesso";
            
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao atualizar: "  +  e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<string>> DeleteAluno(Guid id)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var aluno = await context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Aluno não encontrado ou Id invalido";
                serviceResponse.Data = null;
                return serviceResponse;
            }

            var clienteId = aluno.ClienteId;

            context.Alunos.Remove(aluno);
            await context.SaveChangesAsync();

            var clienteDeleteResponse = await clienteService.DeleteCliente(clienteId);
            if (!clienteDeleteResponse.Success)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Aluno removido, mas falha ao remover Cliente associado: {clienteDeleteResponse.Message}";
                serviceResponse.Data = null;
                return serviceResponse;
            };
            
            serviceResponse.Data = null;
            serviceResponse.Success = true;
            serviceResponse.Message = "Aluno removido com sucesso";
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao deletar aluno: " + e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }
}