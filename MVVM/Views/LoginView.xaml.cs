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
using ECommerceApplication.MVVM.ViewModels;

namespace ECommerceApplication.MVVM.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public event EventHandler LoginSuccess;
        public event EventHandler LoginFailed;
        public event EventHandler Signup;

        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginViewModel user = new LoginViewModel();
            if (user.IsLoginInfoCorrect(TxtUsername.Text, TxtPassword.Password))
            {
                LoginSuccess(this, null);
            }
            else
            {
                LoginFailed(this, null);
            }
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            Signup(this, null);
        }
    }
}
