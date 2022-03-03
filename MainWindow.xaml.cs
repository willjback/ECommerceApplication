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
using ECommerceApplication.Views;

namespace ECommerceApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ToLogonPage();
        }

        #region Navigation
        public void ToLogonPage()
        {
            loginView.Visibility = Visibility.Visible;
            homeView.Visibility = Visibility.Collapsed;
            signupView.Visibility = Visibility.Collapsed;
            cartView.Visibility = Visibility.Collapsed;
            accountView.Visibility = Visibility.Collapsed;
        }

        public void ToSignupPage()
        {
            signupView.Visibility = Visibility.Visible;
            homeView.Visibility = Visibility.Collapsed;
            loginView.Visibility = Visibility.Collapsed;
            cartView.Visibility = Visibility.Collapsed;
            accountView.Visibility = Visibility.Collapsed;
        }

        public void ToHomePage()
        {
            homeView.Visibility = Visibility.Visible;
            loginView.Visibility = Visibility.Collapsed;
            signupView.Visibility = Visibility.Collapsed;
            cartView.Visibility = Visibility.Collapsed;
            accountView.Visibility = Visibility.Collapsed;

            BtnHome.BorderThickness = new Thickness(1.0);
            BtnCart.BorderThickness = new Thickness(0);
            BtnAccount.BorderThickness = new Thickness(0);
        }

        public void ToCartPage()
        {
            cartView.Visibility = Visibility.Visible;
            signupView.Visibility = Visibility.Collapsed;
            homeView.Visibility = Visibility.Collapsed;
            loginView.Visibility = Visibility.Collapsed;
            accountView.Visibility = Visibility.Collapsed;

            BtnHome.BorderThickness = new Thickness(0);
            BtnCart.BorderThickness = new Thickness(1.0);
            BtnAccount.BorderThickness = new Thickness(0);
        }

        public void ToAccountPage()
        {
            accountView.Visibility = Visibility.Visible;
            cartView.Visibility = Visibility.Collapsed;
            signupView.Visibility = Visibility.Collapsed;
            homeView.Visibility = Visibility.Collapsed;
            loginView.Visibility = Visibility.Collapsed;

            BtnHome.BorderThickness = new Thickness(0);
            BtnCart.BorderThickness = new Thickness(0);
            BtnAccount.BorderThickness = new Thickness(1.0);
        }
        #endregion

        #region EventHandlers
        private void Login_Success(object sender, EventArgs e)
        {
            ToHomePage();
        }

        private void Login_Failed(object sender, EventArgs e)
        {
            MessageBox.Show("Incorrect username or password", "Login failed", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Signup_Clicked(object sender, EventArgs e)
        {
            ToSignupPage();
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            ToLogonPage();
        }

        private void CreateAccount_Clicked(object sender, EventArgs e)
        {
            ToLogonPage();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            ToHomePage();
        }

        private void BtnCart_Click(object sender, RoutedEventArgs e)
        {
            ToCartPage();
        }

        private void BtnAccount_Click(object sender, RoutedEventArgs e)
        {
            ToAccountPage();
        }

        private void BtnLogoff_Click(object sender, RoutedEventArgs e)
        {
            ECommerceEntities entities = new ECommerceEntities();

            foreach (Cart item in entities.Carts)
                entities.Carts.Remove(item);
            entities.SaveChanges();
            ToLogonPage();
        }

        private void Window_LayoutUpdated(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
