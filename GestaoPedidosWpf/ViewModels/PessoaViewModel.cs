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
        private readonly PessoaService _pessoaService = new PessoaService();

        public ObservableCollection<Pessoa> Pessoas { get; set; }

        private Pessoa _pessoaSelecionada;
        public Pessoa PessoaSelecionada
        {
            get { return _pessoaSelecionada; }
            set
            {
                if (_pessoaSelecionada != value)
                {
                    _pessoaSelecionada = value;
                    OnPropertyChanged(nameof(PessoaSelecionada));
                }
            }
        }

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
                    OnPropertyChanged(nameof(TextoFiltro));
                }
            }
        }

        public ICommand LimparFiltroCommand => new RelayCommand(() => TextoFiltro = string.Empty);
        public ICommand AdicionarPessoaCommand => new RelayCommand(AdicionarPessoa);
        public ICommand EditarPessoaCommand => new RelayCommand(EditarPessoa, () => PessoaSelecionada != null);
        public ICommand ExcluirPessoaCommand => new RelayCommand(ExcluirPessoa, () => PessoaSelecionada != null);

        public PessoaViewModel()
        {
            Pessoas  = new ObservableCollection<Pessoa>(_pessoaService.ObterTodas());
        }

        private void FiltrarPessoas()
        {
            Pessoas.Clear();
            foreach (var pessoa in _pessoaService.ObterPorNomeOuCpf(TextoFiltro))
                Pessoas.Add(pessoa);
        }

        private void AdicionarPessoa()
        {
            var novaPessoa = new Pessoa();
            var janela = new PessoaCadastroView(novaPessoa);
            if (janela.ShowDialog() == false)
                return;

            _pessoaService.Adicionar(novaPessoa);

            Pessoas.Add(novaPessoa);
            TextoFiltro = string.Empty;

            MessageBox.Show("Salvo com sucesso!");
        }

        private void EditarPessoa()
        {
            var janela = new PessoaCadastroView(PessoaSelecionada);
            if (janela.ShowDialog() == false)
                return;

            _pessoaService.Atualizar(PessoaSelecionada);
            PessoaSelecionada = null;

            MessageBox.Show("Atualizado com sucesso!");
        }

        private void ExcluirPessoa()
        {
            var resultado = MessageBox.Show(
                    "Deseja realmente excluir este item?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
            );

            if (resultado != MessageBoxResult.Yes)
                return;

            _pessoaService.Excluir(PessoaSelecionada.Id);
            Pessoas.Remove(PessoaSelecionada);

            MessageBox.Show("Excluído com sucesso!");
            
            PessoaSelecionada = null;   
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
