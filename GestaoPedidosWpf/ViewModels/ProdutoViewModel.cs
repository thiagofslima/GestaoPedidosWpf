using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using GestaoPedidosWpf.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GestaoPedidosWpf.ViewModels
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Produto> Produtos { get; set; }
        public Produto Produto { get; set; }
        public ICommand AdicionarProdutoCommand { get; }
        public ICommand SalvarCommand { get; set; }

        private readonly ProdutoService _produtoService;

        public ProdutoViewModel()
        {
            _produtoService = new ProdutoService();
            Produtos = new ObservableCollection<Produto>(_produtoService.ObterTodas());

            AdicionarProdutoCommand = new RelayCommand(AbrirTelaCadastro);
            SalvarCommand = new RelayCommand(SalvarProduto);
        }

        private void SalvarProduto()
        {
            _produtoService.Adicionar(Produto);
        }

        private void AbrirTelaCadastro()
        {
            var novoProduto = new Produto();
            var janela = new ProdutoCadastroView(novoProduto);
            if (janela.ShowDialog() == true)
            {
                Produtos.Add(novoProduto);
            }
        }
    }
}
