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

        
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (TxtUsername.Text == string.Empty || TxtPassword.Password == string.Empty)
            {
                MessageBox.Show("Username and password must be filled out", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (IsLoginInfoCorrect(TxtUsername.Text, TxtPassword.Password))
            {
                ECommerceEntities entities = new ECommerceEntities();
                var user = entities.Users.FirstOrDefault(u => TxtUsername.Text == u.Username);

                string filepath = @"C:\MSSA\20483\Project\ECommerceApplication\userinfo.txt";
                StringBuilder userInfo = new StringBuilder();
                userInfo.Append(user.UserID + "\n");
                userInfo.Append(user.Username + "\n");
                userInfo.Append(user.Password + "\n");
                userInfo.Append(user.IsAdmin + "\n");
                userInfo.Append(user.Balance + "\n");

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

        public bool IsLoginInfoCorrect(string username, string password)
        {
            ECommerceEntities entities = new ECommerceEntities();
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
