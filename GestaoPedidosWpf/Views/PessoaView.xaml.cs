using GestaoPedidosWpf.ViewModels;
using System.Windows.Controls;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for PessoaView.xaml
    /// </summary>
    public partial class PessoaView : UserControl
    {
        public PessoaView()
        {
            InitializeComponent();
            DataContext = new PessoaViewModel();
        }
    }
}
