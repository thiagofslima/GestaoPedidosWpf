namespace GestaoPedidosWpf.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; } = new Endereco();

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

        override public string ToString()
        {
            return $"{Rua}, {Numero} - {Bairro}, {Cidade} - {Estado}, {Cep}";
        }
    }
}
