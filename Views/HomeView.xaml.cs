using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ECommerceApplication.Dialog;

namespace ECommerceApplication.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public event EventHandler Cart;
        ECommerceEntities entities = new ECommerceEntities();
        public HomeView()
        {
            InitializeComponent();
            gridProducts.ItemsSource = entities.Products.ToList();
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            ViewItemDialog productViewer = new ViewItemDialog();
            productViewer.ShowItem((Product)gridProducts.SelectedItem);
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            Cart(this, null);
        }
    }
}
