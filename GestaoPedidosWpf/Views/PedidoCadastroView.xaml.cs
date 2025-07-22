using GestaoPedidosWpf.ViewModels;
using System.Windows.Controls;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for PedidoCadastroView.xaml
    /// </summary>
    public partial class PedidoCadastroView : UserControl
    {
        public PedidoCadastroView()
        {
            InitializeComponent();
            DataContext = new PedidoViewModel();
        }
    }
}
