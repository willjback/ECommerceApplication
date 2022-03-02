using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ECommerceApplication.Views
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
            if (IsLoginInfoCorrect(TxtUsername.Text, TxtPassword.Password))
            {
                TxtUsername.Clear();
                TxtPassword.Clear();
                LoginSuccess(this, null);
            }
            else
            {
                LoginFailed(this, null);
            }
        }

        private void BtnSignup_Click(object sender, RoutedEventArgs e)
        {
            TxtUsername.Clear();
            TxtPassword.Clear();
            Signup(this, null);
        }

        private void BtnLogin_LayoutUpdated(object sender, EventArgs e)
        {
            if (TxtUsername.Text != string.Empty && TxtPassword.Password != string.Empty)
                BtnLogin.IsEnabled = true;
            else
                BtnLogin.IsEnabled = false;
        }


        ECommerceEntities entities = new ECommerceEntities();
        public bool IsLoginInfoCorrect(string username, string password)
        {
            var user = entities.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
