using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using GestaoPedidosWpf.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GestaoPedidosWpf.ViewModels
{
    public class PessoaViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public Pessoa Pessoa { get; set; }
        public ICommand AdicionarPessoaCommand { get; }
        public ICommand SalvarCommand { get; set; }

        private readonly PessoaService _pessoaService;

        public PessoaViewModel()
        {
            _pessoaService = new PessoaService();
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.ObterTodas());

            AdicionarPessoaCommand = new RelayCommand(AbrirTelaCadastro);
            SalvarCommand = new RelayCommand(SalvarPessoa);
        }

        private void SalvarPessoa()
        {
            _pessoaService.Adicionar(Pessoa);
        }

        private void AbrirTelaCadastro()
        {
            var novaPessoa = new Pessoa();
            var janela = new PessoaCadastroView(novaPessoa);
            if (janela.ShowDialog() == true)
            {
                Pessoas.Add(novaPessoa);
            }
        }
    }
}
