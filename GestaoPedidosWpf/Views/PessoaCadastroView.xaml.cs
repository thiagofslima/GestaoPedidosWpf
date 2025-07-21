using GestaoPedidosWpf.Models;
using GestaoPedidosWpf.Services;
using GestaoPedidosWpf.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

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

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var pessoa = (Pessoa)DataContext;
            _servico.Adicionar(pessoa);

            MessageBox.Show("Adicionado com sucesso!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            DialogResult = true;
            Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
