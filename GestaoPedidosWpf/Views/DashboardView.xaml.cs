﻿using GestaoPedidosWpf.ViewModels;
using System.Windows.Controls;

namespace GestaoPedidosWpf.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        public DashboardView()
        {
            InitializeComponent();
            DataContext = new DashboardViewModel();
        }
    }
}
