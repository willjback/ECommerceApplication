using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
        ECommerceEntities entities = new ECommerceEntities();

        string filepath = @"C:\MSSA\20483\Project\ECommerceApplication\userinfo.txt";

        public HomeView()
        {
            InitializeComponent();

            gridProducts.ItemsSource = entities.Products.ToList();
        }

        private void BtnSelectItem_Click(object sender, RoutedEventArgs e)
        {
            if (gridProducts.SelectedItem == null)
            {
                MessageBox.Show("You must click an item in the grid first", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            ViewItemDialog productViewer = new ViewItemDialog();
            productViewer.ShowItem((Product)gridProducts.SelectedItem);
        }

        private void  BtnCreateItem_Click(object sender, RoutedEventArgs e)
        {
            if (TxtProductName.Text == string.Empty || TxtProductPrice.Text == string.Empty)
            {
                MessageBox.Show("Product Name and Product Price must be filled out", "Item not added", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                Decimal.Parse(TxtProductPrice.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Product Price must only contain integers or decimals", "Item not added", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Product product = new Product();
            product.ProductName = TxtProductName.Text;
            product.Price = Decimal.Parse(TxtProductPrice.Text);
            entities.Products.Add(product);
            entities.SaveChanges();
            RefreshData();
        }

        private void BtnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (gridProducts.SelectedItem == null)
            {
                MessageBox.Show("You must click an item in the grid first", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            entities.Products.Remove((Product)gridProducts.SelectedItem);
            entities.SaveChanges();
            RefreshData();
        }

        private void Added_Product(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            gridProducts.ItemsSource = null;
            gridProducts.ItemsSource = entities.Products.ToList();

            TxtProductName.Clear();
            TxtProductPrice.Clear();

            if (File.ReadLines(filepath).Skip(3).Take(1).First().ToString() == "True")
            {
                AdminGrid.Visibility = Visibility.Visible;
            }
            else
            {
                AdminGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void gridProducts_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ProductID")
            {
                e.Cancel = true;
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RefreshData();
        }
    }
}
