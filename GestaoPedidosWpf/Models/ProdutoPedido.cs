using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestaoPedidosWpf.Models
{
    public class ProdutoPedido : INotifyPropertyChanged
    {
        private Produto _produto;
        public Produto Produto
        {
            get => _produto;
            set
            {
                if (_produto != value)
                {
                    _produto = value;
                    OnPropertyChanged();

                    ProdutoId = _produto?.Id ?? 0;
                    ValorUnitario = _produto?.ValorUnitario ?? 0m;
                }
            }
        }

        private int _produtoId;
        public int ProdutoId
        {
            get => _produtoId;
            set
            {
                if (_produtoId != value)
                {
                    _produtoId = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _valorUnitario;
        public decimal ValorUnitario
        {
            get => _valorUnitario;
            set
            {
                if (_valorUnitario != value)
                {
                    _valorUnitario = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        private int _quantidade = 1;
        public int Quantidade
        {
            get => _quantidade;
            set
            {
                if (_quantidade != value)
                {
                    _quantidade = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Total));
                }
            }
        }

        public decimal Total => ValorUnitario * Quantidade;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
