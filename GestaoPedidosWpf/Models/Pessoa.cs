using System.ComponentModel;

namespace GestaoPedidosWpf.Models
{
    public class Pessoa : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public bool Ativo { get; set; }
        private Endereco _endereco;
        public Endereco Endereco
        {
            get => _endereco;
            set
            {
                if (_endereco != value)
                {
                    _endereco = value;
                    OnPropertyChanged(nameof(Endereco));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));

        public override string ToString()
        {
            return $"#{Id} - {Nome} - {Cpf}";
        }
    }

    public class Endereco
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }

        public Endereco Copiar()
        {
            if (string.IsNullOrWhiteSpace(Rua)
                  && string.IsNullOrWhiteSpace(Numero)
                  && string.IsNullOrWhiteSpace(Bairro)
                  && string.IsNullOrWhiteSpace(Cidade)
                  && string.IsNullOrWhiteSpace(Estado)
                  && string.IsNullOrWhiteSpace(Cep))
                return null;

            return (Endereco)MemberwiseClone();
        }

        override public string ToString()
        {
            return $"{Rua}, {Numero} - {Bairro}, {Cidade} - {Estado}, {Cep}";
        }
    }
}
