using GestaoPedidosWpf.ViewModels;
using System.Windows.Controls;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for PedidoView.xaml
    /// </summary>
    public partial class PedidoView : UserControl
    {
        public PedidoView()
        {
            InitializeComponent();
            DataContext = new PedidoViewModel();
        }
    }
}
