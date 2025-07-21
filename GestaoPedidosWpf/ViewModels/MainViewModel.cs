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
            NavegarPessoasCommand = new RelayCommand(() => ConteudoAtual = new PessoaView());
            NavegarProdutosCommand = new RelayCommand(() => ConteudoAtual = new ProdutoView());
            NavegarPedidosCommand = new RelayCommand(() => ConteudoAtual = new PedidoView());

            ConteudoAtual = new DashboardView();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }

}
