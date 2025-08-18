using domain.Enums;
namespace domain.Models.DTO;

public class PagamentoDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataPagamento { get; set; }
    public string FormaPagamento { get; set; }
    public string AsaasId { get; set; }
    public OpcoesServico TipoServico { get; set; }
    public StatusPrecificacao Status { get; set; }
    public bool EmitirNF { get; set; }
    public Guid? ReservaId { get; set; } 
    public Guid? PacoteId { get; set; } 
    public Guid ClienteId { get; set; }
}
