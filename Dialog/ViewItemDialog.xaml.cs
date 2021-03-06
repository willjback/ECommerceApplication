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
using System.Windows.Shapes;

namespace ECommerceApplication.Dialog
{
    /// <summary>
    /// Interaction logic for ViewItemDialog.xaml
    /// </summary>
    /// 
    public partial class ViewItemDialog : Window
    {
        List<string> cartList = new List<string>();
        public ViewItemDialog()
        {
            InitializeComponent();
        }

        public void ShowItem(Product item)
        {
            LblProductName.Content = item.ProductName;
            LblPrice.Content = String.Format("{0:C}", item.Price);
            LblQuantity.Content = "1";
            LblTotalPrice.Content = LblPrice.Content;

            this.Show();
        }

        private void BtnAddQuantity_Click(object sender, RoutedEventArgs e)
        {
            LblQuantity.Content = Int32.Parse(LblQuantity.Content.ToString()) + 1;
            LblTotalPrice.Content = String.Format("{0:C}", Decimal.Parse(LblTotalPrice.Content.ToString().TrimStart('$')) + Decimal.Parse(LblPrice.Content.ToString().TrimStart('$')));
        }

        private void BtnSubtractQuantity_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.Parse(LblQuantity.Content.ToString()) != 1)
            {
                LblQuantity.Content = Int32.Parse(LblQuantity.Content.ToString()) - 1;
                LblTotalPrice.Content = String.Format("{0:C}", Decimal.Parse(LblTotalPrice.Content.ToString().TrimStart('$')) - Decimal.Parse(LblPrice.Content.ToString().TrimStart('$')));
            }
        }
        private void BtnAddToCart_Click(object sender, RoutedEventArgs e)
        {
            Cart cart = new Cart();
            cart.Product = LblProductName.Content.ToString();
            cart.Quantity = Int32.Parse(LblQuantity.Content.ToString());
            cart.Price = LblTotalPrice.Content.ToString();

            ECommerceEntities entities = new ECommerceEntities();
            entities.Carts.Add(cart);
            entities.SaveChanges();

            this.Close();
        }

        private void BtnCloseViewItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
