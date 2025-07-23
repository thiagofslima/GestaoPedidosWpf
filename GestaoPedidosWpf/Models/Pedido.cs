using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GestaoPedidosWpf.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        [JsonIgnore]
        public Pessoa Pessoa { get; set; }
        public List<ProdutoPedido> ProdutosPedido { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public Status Status { get; set; } = Status.Pendente;
    }

    public enum FormaPagamento
    {
        [Description("Dinheiro")]
        Dinheiro = 1,
        [Description("Cartão")]
        Cartao = 2,
        [Description("Boleto")]
        Boleto = 3
    }

    public enum Status
    {
        Pendente = 1,
        Pago = 2,
        Enviado = 3,
        Recebido = 4
    }
}
