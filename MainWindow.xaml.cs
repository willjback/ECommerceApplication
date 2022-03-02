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
using ECommerceApplication.MVVM.Views;

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

        public void ToLogonPage()
        {
            loginView.Visibility = Visibility.Visible;
            homeView.Visibility = Visibility.Collapsed;
            signupView.Visibility = Visibility.Collapsed;
        }
        public void ToHomePage()
        {
            homeView.Visibility = Visibility.Visible;
            loginView.Visibility = Visibility.Collapsed;
            signupView.Visibility = Visibility.Collapsed;
        }

        public void ToSignupPage()
        {
            signupView.Visibility = Visibility.Visible;
            homeView.Visibility = Visibility.Collapsed;
            loginView.Visibility = Visibility.Collapsed;
        }


        private void Login_Success(object sender, EventArgs e)
        {
            loginView.Visibility = Visibility.Collapsed;
            homeView.Visibility = Visibility.Visible;
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
    }
}
