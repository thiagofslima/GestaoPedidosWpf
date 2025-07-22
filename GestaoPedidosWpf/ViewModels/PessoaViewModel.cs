using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using GestaoPedidosWpf.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace GestaoPedidosWpf.ViewModels
{
    public class PessoaViewModel : INotifyPropertyChanged
    {
        private readonly PessoaService _pessoaService;
        public Pessoa Pessoa { get; set; }
        public ObservableCollection<Pessoa> Pessoas { get; set; }
        public ObservableCollection<Pessoa> PessoasFiltradas { get; set; }

        private string _textoFiltro;
        public string TextoFiltro
        {
            get { return _textoFiltro; }
            set
            {
                if (_textoFiltro != value)
                {
                    _textoFiltro = value;
                    FiltrarPessoas();
                }
            }
        }

        public ICommand LimparFiltroCommand { get; }
        public PessoaViewModel()
        {
            _pessoaService = new PessoaService();
            Pessoas = new ObservableCollection<Pessoa>(_pessoaService.ObterTodas());
            PessoasFiltradas = new ObservableCollection<Pessoa>(Pessoas)

            LimparFiltroCommand = new RelayCommand(() => TextoFiltro = string.Empty);
            AdicionarPessoaCommand = new RelayCommand(AbrirTelaCadastro);
            SalvarCommand = new RelayCommand(SalvarPessoa);
        }

        private void FiltrarPessoas()
        {
            PessoasFiltradas.Clear();
            foreach (var pessoa in _pessoaService.ObterPorNomeOuCpf(TextoFiltro))
                PessoasFiltradas.Add(pessoa);
        }

        private void AbrirTelaCadastro()
        {
            var novaPessoa = new Pessoa();
            var janela = new PessoaCadastroView(novaPessoa);
            if (janela.ShowDialog() == true)
            {
                Pessoas.Add(novaPessoa);
            }
            TextoFiltro = string.Empty;
        }

        private void SalvarPessoa()
        {
            if (Pessoa != null)
            {
                _pessoaService.Adicionar(Pessoa);
                Pessoas.Add(Pessoa);
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nome)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
        }
    }
}
