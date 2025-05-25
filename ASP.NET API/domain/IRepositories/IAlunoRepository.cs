using domain.Models;

namespace domain.IRepositories;

public interface IAlunoRepository
{
    Task<Aluno> GetByIdAsync(int id);
    Task<IEnumerable<Aluno>> GetAllAsync();
    Task<string> Delete(int id);
    Task<Aluno> Update(Aluno aluno);
    Task<Aluno> AddAsync(Aluno aluno);
}