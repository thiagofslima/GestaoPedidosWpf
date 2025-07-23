using GestaoPedidosWpf.Services;
using GestaoPedidosWpf.Views;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestaoPedidosWpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand NavegarDashboardCommand { get; }
        public ICommand NavegarPessoasCommand { get; }
        public ICommand NavegarProdutosCommand { get; }
        public ICommand NavegarPedidosCommand { get; }

        private UserControl _conteudoAtual;
        public UserControl ConteudoAtual
        {
            get => _conteudoAtual;
            set
            {
                _conteudoAtual = value;
                OnPropertyChanged(nameof(ConteudoAtual));
            }
        }

        public MainViewModel()
        {
            NavegarDashboardCommand = new RelayCommand(() => ConteudoAtual = new DashboardView());
            NavegarPessoasCommand = new RelayCommand(NavegarParaPessoas);
            NavegarProdutosCommand = new RelayCommand(() => ConteudoAtual = new ProdutoView());
            NavegarPedidosCommand = new RelayCommand(NavegarParaPedidos);
            ConteudoAtual = new DashboardView();
        }

        private void NavegarParaPessoas()
        {
            var pedidoVm = new PedidoViewModel();

            var pessoaVm = new PessoaViewModel();
            pessoaVm.CriarPedidoRequested += pessoa =>
            {
                pedidoVm.PessoaSelecionada = pessoa;

                var pedidoView = new PedidoView
                {
                    DataContext = pedidoVm
                };

                pedidoVm.PedidoFinalizado += () =>
                {
                    ConteudoAtual = pedidoView;
                };

                pedidoVm.CriarPedidoRequested += () =>
                {
                    var cadastroView = new PedidoCadastroView
                    {
                        DataContext = pedidoVm
                    };
                    ConteudoAtual = cadastroView;
                };

                ConteudoAtual = pedidoView;
            };

            var pessoaView = new PessoaView
            {
                DataContext = pessoaVm
            };

            ConteudoAtual = pessoaView;
        }



        private void NavegarParaPedidos()
        {
            var pedidosVm = new PedidoViewModel();

            var pedidosView = new PedidoView
            {
                DataContext = pedidosVm
            };

            pedidosVm.CriarPedidoRequested += () =>
            {
                var cadastroView = new PedidoCadastroView
                {
                    DataContext = pedidosVm
                };

                // Evento de retorno após finalizar o pedido
                pedidosVm.PedidoFinalizado += () =>
                {
                    ConteudoAtual = pedidosView;
                };

                ConteudoAtual = cadastroView;
            };

            ConteudoAtual = pedidosView;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }

}
