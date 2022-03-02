using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ECommerceApplication.Views
{
    /// <summary>
    /// Interaction logic for SignupView.xaml
    /// </summary>
    public partial class SignupView : UserControl
    {
        public event EventHandler Back;
        public event EventHandler CreateAccount;

        public SignupView()
        {
            InitializeComponent();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            TxtUsernameSignup.Clear();
            TxtPasswordSignup.Clear();
            TxtConfirmPasswordSignup.Clear();
            Back(this, null);
        }

        private void BtnCreateAccount_Click(object sender, RoutedEventArgs e)
        {
            ECommerceEntities entities = new ECommerceEntities();
            var user = entities.Users.FirstOrDefault(un => un.Username == TxtUsernameSignup.Text);
            if (user != null)
            {
                MessageBox.Show("Username taken", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (TxtPasswordSignup.Password != TxtConfirmPasswordSignup.Password)
            {
                MessageBox.Show("Passwords do not match", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            user = new User();
            user.Username = TxtUsernameSignup.Text;
            user.Password = TxtPasswordSignup.Password;
            user.IsAdmin = false;
            entities.Users.Add(user);
            entities.SaveChanges();

            TxtUsernameSignup.Clear();
            TxtPasswordSignup.Clear();
            TxtConfirmPasswordSignup.Clear();

            CreateAccount(this, null);
        }

        private void BtnCreateAccount_LayoutUpdated(object sender, EventArgs e)
        {
            if (TxtUsernameSignup.Text != string.Empty && TxtPasswordSignup.Password != string.Empty && TxtConfirmPasswordSignup.Password != string.Empty)
                BtnCreateAccount.IsEnabled = true;
            else
                BtnCreateAccount.IsEnabled = false;
        }
    }
}
