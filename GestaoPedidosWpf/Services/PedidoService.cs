using GestaoPedidosWpf.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GestaoPedidosWpf.Services
{
    public class PedidoService
    {
        private readonly string caminhoPasta;
        private readonly string caminhoArquivo;

        public PedidoService()
        {
            caminhoPasta = Path.GetFullPath(@"..\..\") + "Data";
            caminhoArquivo = Path.Combine(caminhoPasta, "pedidos.json");
        }

        public List<Pedido> ObterTodos()
        {
            if (File.Exists(caminhoArquivo))
            {
                var json = File.ReadAllText(caminhoArquivo);
                var listaPedidos = JsonConvert.DeserializeObject<List<Pedido>>(json) ?? new List<Pedido>();
                var listaPessoas = new PessoaService().ObterTodas();

                return listaPedidos.Select(pedido =>
                {
                    pedido.Pessoa = listaPessoas.FirstOrDefault(p => p.Id == pedido.PessoaId);
                    return pedido;
                }).ToList();
            }

            return new List<Pedido>();
        }

        public List<Pedido> ObterPorPessoa(int pessoaId)
        {
            return ObterTodos().Where(p => p.PessoaId == pessoaId).ToList();
        }

        public void Adicionar(Pedido produto)
        {
            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            if (File.Exists(caminhoArquivo))
            {
                var listaPedidos = ObterTodos();
                produto.Id = listaPedidos.Any() ? listaPedidos.Max(x => x.Id) + 1 : 1;
                listaPedidos.Add(produto);
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaPedidos, Formatting.Indented));
            }
            else
            {
                produto.Id = 1;
                var listaPedidos = new List<Pedido> { produto };
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaPedidos, Formatting.Indented));
            }
        }
    }
}
