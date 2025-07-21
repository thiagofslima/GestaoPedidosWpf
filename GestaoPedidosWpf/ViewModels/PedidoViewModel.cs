using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace GestaoPedidosWpf.ViewModels
{
    public class PedidoViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Pessoa> Pessoas { get; set; }
        private ObservableCollection<Pedido> _pedidos;
        public ObservableCollection<Pedido> Pedidos
        {
            get => _pedidos;
            set
            {
                _pedidos = value;
                OnPropertyChanged(nameof(Pedidos));
            }
        }
        private Pessoa _pessoaSelecionada;
        public Pessoa PessoaSelecionada
        {
            get => _pessoaSelecionada;
            set
            {
                _pessoaSelecionada = value;
                OnPropertyChanged(nameof(PessoaSelecionada));

                Pedidos = new ObservableCollection<Pedido>(new PedidoService().ObterPorPessoa(PessoaSelecionada.Id));

            }
        }

        public PedidoViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>(new PessoaService().ObterTodas());
            Pedidos = new ObservableCollection<Pedido>(new PedidoService().ObterTodos());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));

    }
}
