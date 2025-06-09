using domain.Models;

namespace domain.IServices;

public interface IPagamentoService
{
    Task<ServiceResponse<Pagamento>> GetPagamentosById(Guid id);
    Task<ServiceResponse<List<Pagamento>>> GetPagamentos();
    Task<ServiceResponse<Pagamento>> CreatePagamento(Pagamento pagamento);
    Task<ServiceResponse<Pagamento>> UpdatePagamento(Pagamento pagamento);
    Task<ServiceResponse<string>> DeletePagamento(Guid id);
}