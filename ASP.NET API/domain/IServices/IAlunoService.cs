using domain.Models;
using domain.Models.DTO;

namespace domain.IServices;

public interface IAlunoService
{
    Task<ServiceResponse<AlunoDTO>> GetAlunoById(Guid id);
    Task<ServiceResponse<List<AlunoDTO>>> GetAlunos();
    Task<ServiceResponse<Aluno>> CreateAluno(Aluno aluno);
    Task<ServiceResponse<AlunoDTO>> CreateAlunoDTO(AlunoDTO alunoDTO);
    Task<ServiceResponse<AlunoDTO>> UpdateAluno(AlunoDTO alunoDTO);
    Task<ServiceResponse<string>> DeleteAluno(Guid id);
}