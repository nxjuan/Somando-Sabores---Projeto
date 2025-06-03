using domain.Models;

namespace domain.IServices;

public interface IAlunoService
{
    Task<ServiceResponse<AlunoDTO>> GetAlunoById(Guid id);
    Task<ServiceResponse<List<AlunoDTO>>> GetAlunos();
    Task<ServiceResponse<AlunoDTO>> CreateAluno(AlunoDTO alunoDTO);
    Task<ServiceResponse<AlunoDTO>> UpdateAluno(AlunoDTO alunoDTO);
    Task<ServiceResponse<string>> DeleteAluno(Guid id);
}