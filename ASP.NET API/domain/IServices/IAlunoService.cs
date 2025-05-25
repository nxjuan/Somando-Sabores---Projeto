using domain.Models;

namespace domain.IServices;

public interface IAlunoService
{
    Task<ServiceResponse<Aluno>> GetAlunoById(int id);
    Task<ServiceResponse<List<Aluno>>> GetAlunos();
    Task<ServiceResponse<Aluno>> CreateAluno(Aluno aluno);
    Task<ServiceResponse<Aluno>> UpdateAluno(Aluno aluno);
    Task<ServiceResponse<string>> DeleteAluno(int id);
}