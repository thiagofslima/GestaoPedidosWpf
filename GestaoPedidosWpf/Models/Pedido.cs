using System;
using System.Collections.Generic;

namespace GestaoPedidosWpf.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int PessoaId { get; set; }
        public Pessoa Pessoa { get; set; }
        public List<ProdutoPedido> ProdutosPedido { get; set; }
        public List<Produto> Produtos { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public Status Status { get; set; } = Status.Pendente;
    }

    public class ProdutoPedido
    {
        public int ProdutoId { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal Total => ValorUnitario * Quantidade;
    }

    public enum FormaPagamento
    {
        Dinheiro,
        Cartao,
        Boleto
    }

    public enum Status
    {
        Pendente,
        Pago,
        Enviado,
        Recebido
    }
}
