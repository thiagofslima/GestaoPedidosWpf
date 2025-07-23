using GestaoPedidosWpf.Models;
using Newtonsoft.Json;
using System;
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

        public List<Produto> ObterTodos()
        {
            List<Produto> listaProdutos;

            if (File.Exists(caminhoArquivo))
            {
                var json = File.ReadAllText(caminhoArquivo);
                return JsonConvert.DeserializeObject<List<Produto>>(json) ?? new List<Produto>();
            }

            return listaProdutos = new List<Produto>();
        }

        public List<Produto> ObterFiltrado(string filtroTexto, decimal? valorMinimo, decimal? valorMaximo)
        {
            var lista = ObterTodos();

            if (!string.IsNullOrWhiteSpace(filtroTexto))
            {
                lista = lista.Where(p =>
                    (!string.IsNullOrEmpty(p.Nome) && p.Nome.IndexOf(filtroTexto, StringComparison.OrdinalIgnoreCase) >= 0) ||
                    (!string.IsNullOrEmpty(p.Codigo) && p.Codigo.IndexOf(filtroTexto, StringComparison.OrdinalIgnoreCase) >= 0)
                ).ToList();
            }

            if (valorMinimo.HasValue || valorMaximo.HasValue)
            {
                lista = lista.Where(p =>
                    (!valorMinimo.HasValue || p.ValorUnitario >= valorMinimo) &&
                    (!valorMaximo.HasValue || p.ValorUnitario <= valorMaximo)
                ).ToList();
            }

            return lista;
        }

        public void Adicionar(Produto produto)
        {
            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            if (File.Exists(caminhoArquivo))
            {
                var listaProdutos = ObterTodos();
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

        public void Atualizar(Produto produto)
        {
            var listaProdutos = ObterTodos();

            var index = listaProdutos.FindIndex(p => p.Id == produto.Id);
            if (index >= 0)
            {
                listaProdutos[index] = produto;
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaProdutos, Formatting.Indented));
            }
        }

        internal void Excluir(int id)
        {
            var listaProdutos = ObterTodos();

            var index = listaProdutos.FindIndex(p => p.Id == id);
            if (index >= 0)
            {
                //listaProdutos[index].Ativo = false;
                listaProdutos.RemoveAt(index);
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaProdutos, Formatting.Indented));
            }
        }
    }
}
