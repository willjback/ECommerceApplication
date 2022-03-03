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
        string filepath = @"C:\MSSA\20483\Project\ECommerceApplication\userinfo.txt";
        public CartView()
        {
            InitializeComponent();
            gridCart.ItemsSource = entities.Carts.ToList();
        }

        private void BtnCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (gridCart.Items.Count <= 1)
            {
                MessageBox.Show("Shopping Cart is empty!", "Checkout Incomplete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            decimal accountBalance = Decimal.Parse(File.ReadLines(filepath).Skip(4).Take(1).First());
            decimal total = Decimal.Parse(LblTotalCart.Content.ToString().TrimStart('$'));
            if ((accountBalance - total) < 0)
            {
                MessageBox.Show("Balance too low!", "Checkout Incomplete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure you want to checkout?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            foreach (Cart item in entities.Carts)
                entities.Carts.Remove(item);
            entities.SaveChanges();

            decimal newBalance = accountBalance - total;

            int userID = Int32.Parse(File.ReadLines(filepath).Take(1).First());
            var user = entities.Users.FirstOrDefault(u => u.UserID == userID);
            if (user == null)
            {
                MessageBox.Show("There was an error. Returned user is NULL.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] arrLines = File.ReadAllLines(filepath);
            arrLines[4] = newBalance.ToString();
            File.WriteAllLines(filepath, arrLines);

            user.Balance = newBalance;
            entities.SaveChanges();

            RefreshData();

            MessageBox.Show("Your order has been submitted", "Checkout Successful", MessageBoxButton.OK);
        }

        private void BtnRemoveCartItem_Click(object sender, RoutedEventArgs e)
        {
            if (gridCart.Items.Count <= 1)
            {
                MessageBox.Show("There are no items left.", "Checkout Incomplete", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
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
