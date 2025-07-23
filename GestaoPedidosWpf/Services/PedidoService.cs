using GestaoPedidosWpf.Models;
using Newtonsoft.Json;
using System;
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

        public List<Pedido> ObterFiltrado(
            int? pessoaId,
            FormaPagamento? formaPgto,
            Status? status,
            decimal? valorMinimo,
            decimal? valorMaximo,
            DateTime? dataInicial,
            DateTime? dataFinal)
        {
            IEnumerable<Pedido> lista = ObterTodos();

            if (pessoaId.HasValue)
                lista = lista.Where(p =>
                    p.PessoaId == pessoaId
                );

            if (formaPgto.HasValue)
                lista = lista.Where(p =>
                    p.FormaPagamento == formaPgto
                );

            if (status.HasValue)
                lista = lista.Where(p =>
                    p.Status == status
                );

            if (valorMinimo.HasValue || valorMaximo.HasValue)
                lista = lista.Where(p =>
                    (!valorMinimo.HasValue || p.ValorTotal >= valorMinimo) &&
                    (!valorMaximo.HasValue || p.ValorTotal <= valorMaximo)
                );

            if (dataInicial.HasValue || dataFinal.HasValue)
                lista = lista.Where(p =>
                    (!dataInicial.HasValue || p.DataVenda >= dataInicial) &&
                    (!dataFinal.HasValue || p.DataVenda <= dataFinal)
                );

            return lista.ToList();
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
