using domain.Models;

namespace domain.IServices;

public interface IPagamentoService
{
    Task<ServiceResponse<Pagamentos>> GetPagamentosById(Guid id);
    Task<ServiceResponse<List<Pagamentos>>> GetPagamentos();
    Task<ServiceResponse<Pagamentos>> CreatePagamento(Pagamentos pagamento);
    Task<ServiceResponse<Pagamentos>> UpdatePagamento(Pagamentos pagamento);
    Task<ServiceResponse<string>> DeletePagamento(Guid id);
}