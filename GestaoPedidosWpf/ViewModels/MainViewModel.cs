using GestaoPedidosWpf.Services;
using GestaoPedidosWpf.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GestaoPedidosWpf.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand NavegarDashboardCommand { get; }
        public ICommand NavegarPessoasCommand { get; }

        private UserControl _conteudoAtual;
        public UserControl ConteudoAtual
        {
            get => _conteudoAtual;
            set
            {
                _conteudoAtual = value;
                OnPropertyChanged(nameof(ConteudoAtual));
            }
        }

        public MainViewModel()
        {
            NavegarDashboardCommand = new RelayCommand(() => ConteudoAtual = new DashboardView());
            NavegarPessoasCommand = new RelayCommand(() => ConteudoAtual = new PessoaView());

            ConteudoAtual = new DashboardView(); // inicial
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }

}
