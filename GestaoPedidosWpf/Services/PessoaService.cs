using GestaoPedidosWpf.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestaoPedidosWpf.Services
{
    public class PessoaService
    {
        private readonly string caminhoPasta;
        private readonly string caminhoArquivo;

        public PessoaService()
        {
            caminhoPasta = Path.GetFullPath(@"..\..\") + "Data";
            caminhoArquivo = Path.Combine(caminhoPasta, "pessoa.json");
        }

        public List<Pessoa> ObterTodas()
        {
            List<Pessoa> personList;

            if (File.Exists(caminhoArquivo))
            {
                var json = File.ReadAllText(caminhoArquivo);
                return JsonConvert.DeserializeObject<List<Pessoa>>(json) ?? new List<Pessoa>();
            }

            return personList = new List<Pessoa>();
        }

        public void Adicionar(Pessoa pessoa)
        {
            if (!Directory.Exists(caminhoPasta))
                Directory.CreateDirectory(caminhoPasta);

            if (File.Exists(caminhoArquivo))
            {
                var listaPessoas = ObterTodas();
                pessoa.Id = listaPessoas.Any() ? listaPessoas.Max(x => x.Id) + 1 : 1;
                listaPessoas.Add(pessoa);
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaPessoas, Formatting.Indented));
            }
            else
            {
                pessoa.Id = 1;
                var listaPessoas = new List<Pessoa> { pessoa };
                File.WriteAllText(caminhoArquivo, JsonConvert.SerializeObject(listaPessoas, Formatting.Indented));
            }
        }
    }
}
