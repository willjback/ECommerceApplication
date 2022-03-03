using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Text;

namespace ECommerceApplication.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    /// 
    public partial class LoginView : UserControl
    {
        public event EventHandler LoginSuccess;
        public event EventHandler LoginFailed;
        public event EventHandler Signup;

        ECommerceEntities entities = new ECommerceEntities();
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (IsLoginInfoCorrect(TxtUsername.Text, TxtPassword.Password))
            {
                var user = entities.Users.FirstOrDefault(u => TxtUsername.Text == u.Username);

                string filepath = @"C:\MSSA\20483\Project\ECommerceApplication\userinfo.txt";
                StringBuilder userInfo = new StringBuilder();
                userInfo.Append(user.Username + "\n");
                userInfo.Append(user.Password + "\n");
                userInfo.Append(user.IsAdmin + "\n");

                File.WriteAllText(filepath, userInfo.ToString());

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
