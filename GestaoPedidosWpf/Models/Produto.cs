using System.ComponentModel;

namespace GestaoPedidosWpf.Models
{
    public class Produto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public decimal? ValorUnitario { get; set; }

        public Produto Clonar()
        {
            return (Produto)MemberwiseClone();
        }

        public void Copiar(Produto produto)
        {
            Id = produto.Id;
            Nome = produto.Nome;
            Codigo = produto.Codigo;
            ValorUnitario = produto.ValorUnitario;

            OnPropertyChanged(nameof(Nome));
            OnPropertyChanged(nameof(Codigo));
            OnPropertyChanged(nameof(ValorUnitario));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
