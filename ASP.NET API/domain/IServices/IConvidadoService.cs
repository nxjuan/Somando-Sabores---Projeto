using domain.Models;

namespace domain.IServices;

public interface IConvidadoService
{
    Task<ServiceResponse<Convidado>> GetConvidado(Guid id);
    Task<ServiceResponse<List<Convidado>>> GetConvidados();
    Task<ServiceResponse<Convidado>> CreateConvidado(Convidado convidado);
    Task<ServiceResponse<Convidado>> UpdateConvidado(Convidado convidado);
    Task<ServiceResponse<string>> DeleteConvidado(Guid id);
    Task<ServiceResponse<List<Convidado>>> CreateConvidados(IEnumerable<Convidado> convidados);
    Task<ServiceResponse<List<Convidado>>> UpdateConvidados(IEnumerable<Convidado> convidados);
}