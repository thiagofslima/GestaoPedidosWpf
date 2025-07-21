using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using System.ComponentModel;
using System.Windows;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for ProdutoCadastroView.xaml
    /// </summary>
    public partial class ProdutoCadastroView : Window
    {
        public Produto Produto { get; set; } = new Produto();
        private readonly ProdutoService _servico;

        public ProdutoCadastroView(Produto produto)
        {
            InitializeComponent();
            DataContext = produto;

            _servico = new ProdutoService();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var produto = (Produto)DataContext;
            _servico.Adicionar(produto);

            MessageBox.Show("Adicionado com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string nome) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nome));
    }
}
