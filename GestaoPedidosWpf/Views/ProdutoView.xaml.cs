using GestaoPedidosWpf.ViewModels;
using System.Windows.Controls;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for ProdutoView.xaml
    /// </summary>
    public partial class ProdutoView : UserControl
    {
        public ProdutoView()
        {
            InitializeComponent();
            DataContext = new ProdutoViewModel();
        }
    }
}
