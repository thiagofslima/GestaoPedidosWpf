using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace GestaoPedidosWpf.ViewModels
{
    public class PedidoViewModel : INotifyPropertyChanged
    {
        public event Action CriarPedidoRequested;
        public event Action PedidoFinalizado;

        public ProdutoPedido ProdutoPedido { get; set; } = new ProdutoPedido();
        public ProdutoPedido ItemSelecionado { get; set; } = new ProdutoPedido();

        private decimal _totalPedido;
        public decimal TotalPedido
        {
            get => _totalPedido;
            set { _totalPedido = value; OnPropertyChanged(nameof(TotalPedido)); } 
        }

        public ICommand LimparFiltrosCommand => new RelayCommand(LimparFiltros);
        public ICommand NovoPedidoCommand => new RelayCommand(AbrirNovoPedido);
        public ICommand AdicionarItemCommand => new RelayCommand(AdicionarPedido);
        public ICommand RemoverItemCommand { get; }
        public ICommand FinalizarPedidoCommand { get; }
        public ICommand MarcarComoPagoCommand { get; }
        public ICommand MarcarComoEnviadoCommand { get; }
        public ICommand MarcarComoRecebidoCommand { get; }

        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<ProdutoPedido> ItensPedido { get; }
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
                if (value != null && Pessoas != null && Pessoas.Any())
                {
                    var encontrada = Pessoas.FirstOrDefault(p => p.Id == value.Id);
                    _pessoaSelecionada = encontrada ?? value;
                }
                else
                {
                    _pessoaSelecionada = value;
                }

                FiltrarPedidos();
                OnPropertyChanged(nameof(PessoaSelecionada));
            }
        }

        private string _valorMinimo;
        public string ValorMinimo
        {
            get => _valorMinimo;
            set
            {
                _valorMinimo = value;
                FiltrarPedidos();
                OnPropertyChanged(nameof(ValorMinimo));
            }
        }

        private string _valorMaximo;
        public string ValorMaximo
        {
            get => _valorMaximo;
            set
            {
                _valorMaximo = value;
                FiltrarPedidos();
                OnPropertyChanged(nameof(ValorMaximo));
            }
        }

        private DateTime? _dataInicial;
        public DateTime? DataInicial
        {
            get => _dataInicial;
            set
            {
                _dataInicial = value;
                FiltrarPedidos();
                OnPropertyChanged(nameof(DataInicial));
            }
        }

        private DateTime? _dataFinal;
        public DateTime? DataFinal
        {
            get => _dataFinal;
            set
            {
                _dataFinal = value;
                FiltrarPedidos();
                OnPropertyChanged(nameof(DataFinal));
            }
        }

        public IEnumerable<FormaPagamento> FormasPagamento
            => Enum.GetValues(typeof(FormaPagamento))
                   .Cast<FormaPagamento>();

        private FormaPagamento? _pgtSelecionado;
        public FormaPagamento? PgtoSelecionado
        {
            get => _pgtSelecionado;
            set
            {
                if (_pgtSelecionado == value) return;
                _pgtSelecionado = value;
                FiltrarPedidos();
                OnPropertyChanged();
            }
        }

        public IEnumerable<Status> StatusPedidos
            => Enum.GetValues(typeof(Status))
            .Cast<Status>();

        private Status? _statusSelecionado;
        public Status? StatusSelecionado
        {
            get => _statusSelecionado;
            set
            {
                if (_statusSelecionado == value) return;
                _statusSelecionado = value;
                FiltrarPedidos();
                OnPropertyChanged();
            }
        }

        public PedidoViewModel()
        {
            Pessoas = new ObservableCollection<Pessoa>(new PessoaService().ObterTodas());
            Pedidos = new ObservableCollection<Pedido>(new PedidoService().ObterTodos());
            Produtos = new ObservableCollection<Produto>(new ProdutoService().ObterTodos());
            ItensPedido = new ObservableCollection<ProdutoPedido>();

            MarcarComoPagoCommand = new RelayCommand<Pedido>(MarcarComoPago);
            MarcarComoEnviadoCommand = new RelayCommand<Pedido>(MarcarComoEnviado);
            MarcarComoRecebidoCommand = new RelayCommand<Pedido>(MarcarComoRecebido);

            FinalizarPedidoCommand = new RelayCommand(() =>
            {
                if (PessoaSelecionada == null)
                {
                    MessageBox.Show("Selecione uma pessoa");
                    return;
                }

                if (PgtoSelecionado == null)
                {
                    MessageBox.Show("Selecione uma forma de pagamento");
                    return;
                }

                if (!ItensPedido.Any())
                {
                    MessageBox.Show("Nenhum produto adicionado");
                }

                new PedidoService().Adicionar(new Pedido
                {
                    PessoaId = PessoaSelecionada.Id,
                    ProdutosPedido = ItensPedido.ToList(),
                    ValorTotal = ItensPedido.Sum(i => i.ValorUnitario * i.Quantidade),
                    DataVenda = DateTime.Now,
                    FormaPagamento = (FormaPagamento)PgtoSelecionado,
                    Status = Status.Pendente
                });

                MessageBox.Show("Pedido finalizado com sucesso");

                ItensPedido.Clear();
                TotalPedido = 0m;

                PedidoFinalizado?.Invoke();
            });

            RemoverItemCommand = new RelayCommand(() =>
            {
                TotalPedido -= ItemSelecionado?.Total ?? 0m;

                if (ItemSelecionado != null)
                    ItensPedido.Remove(ItemSelecionado);
            });
        }

        private void FiltrarPedidos()
        {
            decimal? valorMin = decimal.TryParse(ValorMinimo, out var parsedMin) ? parsedMin : (decimal?)null;
            decimal? valorMax = decimal.TryParse(ValorMaximo, out var parsedMax) ? parsedMax : (decimal?)null;

            var listaFiltrada = new PedidoService().ObterFiltrado(
                PessoaSelecionada?.Id,
                PgtoSelecionado,
                StatusSelecionado,
                valorMin,
                valorMax,
                DataInicial,
                DataFinal
            );

            Pedidos.Clear();
            foreach (var pedido in listaFiltrada)
                Pedidos.Add(pedido);
        }

        public void AtualizarPedidos() => FiltrarPedidos();

        private void LimparFiltros()
        {
            PessoaSelecionada = null;
            PgtoSelecionado = null;
            StatusSelecionado = null;
            ValorMinimo = string.Empty;
            ValorMaximo = string.Empty;
            DataInicial = null;
            DataFinal = null;
        }

        private void AbrirNovoPedido()
        {
            CriarPedidoRequested?.Invoke();
        }

        private void AdicionarPedido()
        {
            if (ProdutoPedido.Produto == null || ProdutoPedido.Quantidade <= 0)
            {
                MessageBox.Show("Selecione um produto e informe a quantidade");
                return;
            }

            var novoItem = new ProdutoPedido
            {
                Produto = ProdutoPedido.Produto,
                Quantidade = ProdutoPedido.Quantidade,
                ValorUnitario = ProdutoPedido.Produto?.ValorUnitario ?? 0
            };
            TotalPedido += novoItem.Total;

            ItensPedido.Add(novoItem);

            ProdutoPedido = new ProdutoPedido();
            OnPropertyChanged(nameof(ProdutoPedido));
        }

        private void MarcarComoPago(Pedido pedido) => AtualizarStatus(pedido.Id, Status.Pago);

        private void MarcarComoEnviado(Pedido pedido) => AtualizarStatus(pedido.Id, Status.Enviado);

        private void MarcarComoRecebido(Pedido pedido) => AtualizarStatus(pedido.Id, Status.Recebido);

        private void AtualizarStatus(int pedidoId, Status status)
        {
            new PedidoService().AtualizarStatus(pedidoId, status);
            AtualizarPedidos();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
