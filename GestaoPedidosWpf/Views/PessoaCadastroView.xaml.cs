using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using System.Windows;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for PessoaCadastroView.xaml
    /// </summary>
    public partial class PessoaCadastroView : Window
    {
        private readonly PessoaService _servico;

        public PessoaCadastroView(Pessoa pessoa)
        {
            InitializeComponent();
            DataContext = pessoa;

            _servico = new PessoaService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
