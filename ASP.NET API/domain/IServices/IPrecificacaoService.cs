using domain.Models;

namespace domain.IServices;

public interface IPrecificacaoService
{
    Task<ServiceResponse<Precificacao>> GetPrecificacao(Guid id);
    Task<ServiceResponse<List<Precificacao>>> GetPrecificacoes();
    Task<ServiceResponse<Precificacao>> CreatePrecificacao(Precificacao precificacao);
    Task<ServiceResponse<Precificacao>> UpdatePrecificacao(Precificacao precificacao);
    Task<ServiceResponse<string>> DeletePrecificacao(Guid id);
}