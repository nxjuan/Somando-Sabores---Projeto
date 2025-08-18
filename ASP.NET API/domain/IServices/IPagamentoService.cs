using domain.Models;
using domain.Models.DTO;

namespace domain.IServices;

public interface IPagamentoService
{
    Task<ServiceResponse<PagamentoDTO>> GetPagamentoById(Guid id);
    Task<ServiceResponse<List<PagamentoDTO>>> GetPagamentos();
    Task<ServiceResponse<PagamentoDTO>> CreatePagamento(PagamentoDTO pagamento);
    Task<ServiceResponse<PagamentoDTO>> UpdatePagamento(PagamentoDTO pagamento);
    Task<ServiceResponse<string>> DeletePagamento(Guid id);
}