using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using System.Windows;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for ProdutoCadastroView.xaml
    /// </summary>
    public partial class ProdutoCadastroView : Window
    {
        private readonly ProdutoService _servico;

        public ProdutoCadastroView(Produto produto)
        {
            InitializeComponent();
            DataContext = produto;

            _servico = new ProdutoService();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
