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
        public event Action<Pessoa> CriarPedidoRequested;
        public ProdutoPedido ProdutoPedido { get; set; } = new ProdutoPedido();
        public ProdutoPedido ItemSelecionado { get; set; } = new ProdutoPedido();

        public decimal TotalPedido { get => _totalPedido; set { _totalPedido = value; OnPropertyChanged(nameof(TotalPedido)); } }
        private decimal _totalPedido;

        public ICommand NovoPedidoCommand { get; }
        public ICommand AdicionarItemCommand { get; }
        public ICommand RemoverItemCommand { get; }
        public ICommand FinalizarPedidoCommand { get; }

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
                _pessoaSelecionada = value;
                OnPropertyChanged(nameof(PessoaSelecionada));

                Pedidos = new ObservableCollection<Pedido>(new PedidoService().ObterPorPessoa(PessoaSelecionada.Id));

            }
        }

        public IEnumerable<FormaPagamento> FormasPagamento
            => Enum.GetValues(typeof(FormaPagamento))
                   .Cast<FormaPagamento>();

        private FormaPagamento _formaSelecionada;
        public FormaPagamento FormaSelecionada
        {
            get => _formaSelecionada;
            set
            {
                if (_formaSelecionada == value) return;
                _formaSelecionada = value;
                OnPropertyChanged();
            }
        }

        public PedidoViewModel()
        {
            NovoPedidoCommand = new RelayCommand(AbrirNovoPedido);

            Pessoas = new ObservableCollection<Pessoa>(new PessoaService().ObterTodas());
            Pedidos = new ObservableCollection<Pedido>(new PedidoService().ObterTodos());
            Produtos = new ObservableCollection<Produto>(new ProdutoService().ObterTodos());
            ItensPedido = new ObservableCollection<ProdutoPedido>();

            AdicionarItemCommand = new RelayCommand(() =>
            {
                if (ProdutoPedido.Produto == null || ProdutoPedido.Quantidade <= 0)
                {
                    MessageBox.Show("Selecione um produto e informe a quantidade");
                    return;
                }

                var novoItem = new ProdutoPedido
                {
                    Produto = this.ProdutoPedido.Produto,
                    Quantidade = this.ProdutoPedido.Quantidade,
                    ValorUnitario = this.ProdutoPedido.Produto?.ValorUnitario ?? 0
                };

                TotalPedido += novoItem.Total;

                ItensPedido.Add(novoItem);

                ProdutoPedido = new ProdutoPedido();
                OnPropertyChanged(nameof(ProdutoPedido));
            });

            FinalizarPedidoCommand = new RelayCommand(() =>
            {
                if (PessoaSelecionada == null)
                {
                    MessageBox.Show("Selecione uma pessoa");
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
                    FormaPagamento = FormaSelecionada,
                    Status = Status.Pendente
                });

                MessageBox.Show("Pedido finalizado com sucesso");

                ItensPedido.Clear();
                TotalPedido = 0m;
            });

            RemoverItemCommand = new RelayCommand(() =>
            {
                TotalPedido -= ItemSelecionado?.Total ?? 0m;

                if (ItemSelecionado != null)
                    ItensPedido.Remove(ItemSelecionado);
            });
        }


        private void AbrirNovoPedido()
        {
            CriarPedidoRequested?.Invoke(PessoaSelecionada);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
