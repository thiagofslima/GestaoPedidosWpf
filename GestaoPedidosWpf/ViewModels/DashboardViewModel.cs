using GestaoPedidosWpf.Services;
using System.ComponentModel;
using System.Linq;

namespace GestaoPedidosWpf.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        public int TotalClientesAtivos { get; set; }
        public int TotalPedidos { get; set; }
        public int TotalProdutos { get; set; }
        public decimal TotalVendas { get; set; }

        public DashboardViewModel()
        {
            var pessoaService = new PessoaService();
            var pedidoService = new PedidoService();
            var produtoService = new ProdutoService();

            TotalClientesAtivos = pessoaService.ObterTodas().Count(p => p.Ativo == true);
            var pedidos = pedidoService.ObterTodos();
            TotalPedidos = pedidos.Count;
            TotalProdutos = produtoService.ObterTodos().Count;
            TotalVendas = pedidos.Sum(p => p.ValorTotal);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
