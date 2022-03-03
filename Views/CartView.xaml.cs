using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ECommerceApplication.Views
{
    /// <summary>
    /// Interaction logic for CartView.xaml
    /// </summary>
    public partial class CartView : UserControl
    {
        ECommerceEntities entities = new ECommerceEntities();
        public CartView()
        {
            InitializeComponent();
            gridCart.ItemsSource = entities.Carts.ToList();
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            gridCart.ItemsSource = null;
            gridCart.ItemsSource = entities.Carts.ToList();

        }

        private void gridCart_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ItemNumber")
            {
                e.Cancel = true;
            }
        }
    }
}
