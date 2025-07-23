using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using GestaoPedidosWpf.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GestaoPedidosWpf.ViewModels
{
    public class ProdutoViewModel : INotifyPropertyChanged
    {
        private readonly ProdutoService _produtoService = new ProdutoService();

        public ObservableCollection<Produto> Produtos { get; set; }

        private Produto _produtoSelecionado;
        public Produto ProdutoSelecionado
        {
            get { return _produtoSelecionado; }
            set
            {
                if (_produtoSelecionado != value)
                {
                    _produtoSelecionado = value;

                    OnPropertyChanged(nameof(ProdutoSelecionado));
                }
            }
        }

        private string _textoFiltro;
        public string TextoFiltro
        {
            get { return _textoFiltro; }
            set
            {
                if (_textoFiltro != value)
                {
                    _textoFiltro = value;
                    FiltrarProdutos();
                    OnPropertyChanged(nameof(TextoFiltro));
                }
            }
        }

        public ICommand LimparFiltroCommand => new RelayCommand(() => TextoFiltro = string.Empty);
        public ICommand AdicionarProdutoCommand => new RelayCommand(AdicionarProduto);
        public ICommand EditarProdutoCommand => new RelayCommand(EditarProduto, () => ProdutoSelecionado != null);
        public ICommand ExcluirProdutoCommand => new RelayCommand(ExcluirProduto, () => ProdutoSelecionado != null);

        public ProdutoViewModel()
        {
            Produtos = new ObservableCollection<Produto>(_produtoService.ObterTodos());

        }

        private void FiltrarProdutos()
        {
            Produtos.Clear();
            foreach (var produto in _produtoService.ObterPorNomeOuCodigo(TextoFiltro))
                Produtos.Add(produto);
        }

        private void AdicionarProduto()
        {
            var novaProduto = new Produto();
            var janela = new ProdutoCadastroView(novaProduto);
            if (janela.ShowDialog() == false)
                return;

            _produtoService.Adicionar(novaProduto);

            Produtos.Add(novaProduto);
            TextoFiltro = string.Empty;

            MessageBox.Show("Salvo com sucesso!");
        }

        private void EditarProduto()
        {
            var copiaProduto = ProdutoSelecionado.Clonar();
            var janela = new ProdutoCadastroView(copiaProduto);
            if (janela.ShowDialog() == false)
                return;

            ProdutoSelecionado.Copiar(copiaProduto);

            _produtoService.Atualizar(ProdutoSelecionado);
            
            ProdutoSelecionado = null;

            MessageBox.Show("Atualizado com sucesso!");
        }

        private void ExcluirProduto()
        {
            var resultado = MessageBox.Show(
                    "Deseja realmente excluir este item?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
            );

            if (resultado != MessageBoxResult.Yes)
                return;

            _produtoService.Excluir(ProdutoSelecionado.Id);
            Produtos.Remove(ProdutoSelecionado);

            MessageBox.Show("Excluído com sucesso!");

            ProdutoSelecionado = null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
