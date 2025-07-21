using GestaoPedidosWpf.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GestaoPedidosWpf.Services
{
    public class ProdutoService
    {
        private readonly string caminhoPasta;
        private readonly string caminhoArquivo;

        public ProdutoService()
        {
            caminhoPasta = Path.GetFullPath(@"..\..\") + "Data";
            caminhoArquivo = Path.Combine(caminhoPasta, "produtos.json");
        }

        public List<Produto> ObterTodas()
        {
            List<Produto> listaProdutos;

            if (File.Exists(caminhoArquivo))
            {
                var json = File.ReadAllText(caminhoArquivo);
                return JsonConvert.DeserializeObject<List<Produto>>(json) ?? new List<Produto>();
            }

            return listaProdutos = new List<Produto>();
        }

        public void Adicionar(Produto produto)
        {
            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            if (File.Exists(caminhoArquivo))
            {
                var listaProdutos = ObterTodas();
                produto.Id = listaProdutos.Any() ? listaProdutos.Max(x => x.Id) + 1 : 1;
                listaProdutos.Add(produto);
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaProdutos, Formatting.Indented));
            }
            else
            {
                produto.Id = 1;
                var listaProdutos = new List<Produto> { produto };
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaProdutos, Formatting.Indented));
            }
        }
    }
}
