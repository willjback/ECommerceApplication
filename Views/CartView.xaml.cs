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
            MessageBoxResult result = MessageBox.Show("Are you sure you want to checkout?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            foreach (Cart item in entities.Carts)
                entities.Carts.Remove(item);
            entities.SaveChanges();

            RefreshData();

            MessageBox.Show("Your order has been submitted", "Checkout successful", MessageBoxButton.OK);
        }

        private void BtnRemoveCartItem_Click(object sender, RoutedEventArgs e)
        {
            if (gridCart.Items.Count <= 1)
                return;
            Cart cart = (Cart)gridCart.SelectedItem;
            var item = entities.Carts.FirstOrDefault(i => cart.ItemNumber == i.ItemNumber);
            entities.Carts.Remove(item);
            entities.SaveChanges();
            RefreshData();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RefreshData();
        }

        private void gridCart_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ItemNumber")
            {
                e.Cancel = true;
            }
        }

        private void RefreshData()
        {
            gridCart.ItemsSource = null;
            gridCart.ItemsSource = entities.Carts.ToList();

            double subtotal = 0;
            foreach (Cart item in entities.Carts)
            {
                subtotal += double.Parse(item.Price.TrimStart('$'));
            }
            double tax = Math.Round(0.08 * subtotal, 2);
            double total = subtotal + tax;

            LblSubtotalCart.Content = String.Format("{0:C}", subtotal);
            LblTaxCart.Content = String.Format("{0:C}", tax);
            LblTotalCart.Content = String.Format("{0:C}", total);
        }
    }
}
