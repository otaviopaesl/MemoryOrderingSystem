using System.ComponentModel;

namespace MemoryOrderingSystem.Models.Enums
{
    public enum OrderStatus
    {
        [Description("Aguardando Pagamento")]
        AguardandoPagamento,

        [Description("Pagamento Aprovado")]
        PagamentoAprovado,

        [Description("Enviado para Transportadora")]
        EnviadoTransportadora,

        [Description("Entregue")]
        Entregue,

        [Description("Cancelada")]
        Cancelada,
    }
}
