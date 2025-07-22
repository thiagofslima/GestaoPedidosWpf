namespace GestaoPedidosWpf.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }

        public override string ToString()
        {
            return $"#{Id} - {Nome} - {Cpf}";
        }
    }
}
