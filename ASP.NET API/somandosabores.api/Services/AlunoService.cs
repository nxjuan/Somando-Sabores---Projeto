using domain.IServices;
using domain.Models;
using infra.DbContext;

namespace somandosabores.api.Services;

public class AlunoService(ApplicationDbContext context) : IAlunoService
{
    public async Task<ServiceResponse<Aluno>> GetAlunoById(int id)
    {
        var serviceResponse = new ServiceResponse<Aluno>();
        try
        {
            if (id < 1 || id == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Id invalido";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            
            serviceResponse.Data = await context.Alunos.FindAsync(id);
            serviceResponse.Success = true;
            serviceResponse.Message = "Aluno encontrado";
            
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

    public async Task<ServiceResponse<List<Aluno>>> GetAlunos()
    {
        var serviceResponse = new ServiceResponse<List<Aluno>>();
        try
        {
            serviceResponse.Success = true;
            serviceResponse.Data = context.Alunos.ToList();
            serviceResponse.Message = "Alunos encontrado";
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

    public async Task<ServiceResponse<Aluno>> CreateAluno(Aluno aluno)
    {
        var serviceResponse = new ServiceResponse<Aluno>();
        try
        {
            if (aluno == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Informe os dados do aluno";
                serviceResponse.Data = null;
                return serviceResponse;
            }

            if (
                (aluno.Nome == null || aluno.Nome == "")
                || (aluno.RA == null || aluno.RA == "")
                || (aluno.Email == null || aluno.Email == "")
            )
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Informe os dados do aluno";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            
            await context.Alunos.AddAsync(aluno);
            await context.SaveChangesAsync();
            serviceResponse.Data = aluno;
            serviceResponse.Success = true;
            serviceResponse.Message = "Aluno criado com sucesso";
            return serviceResponse;

        }
        catch (Exception e)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Erro ao salvar: " +  e.Message;
            serviceResponse.Data = null;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Aluno>> UpdateAluno(Aluno aluno)
    {
        var serviceResponse = new ServiceResponse<Aluno>();
        try
        {
            var alunoExists = await context.Alunos.FindAsync(aluno.Id);
            if (alunoExists == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Aluno não encontrado";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            
            alunoExists.Nome = aluno.Nome ?? alunoExists.Nome;
            alunoExists.RA = aluno.RA ??  alunoExists.RA;
            alunoExists.Email = aluno.Email  ?? alunoExists.Email;
            
            await context.SaveChangesAsync();
            
            serviceResponse.Data = alunoExists;
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

    public async Task<ServiceResponse<string>> DeleteAluno(int id)
    {
        var serviceResponse = new ServiceResponse<string>();
        try
        {
            var  aluno = await context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Aluno não encontrado ou Id invalido";
                serviceResponse.Data = null;
                return serviceResponse;
            }
            context.Alunos.Remove(aluno);
            await context.SaveChangesAsync();
            
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