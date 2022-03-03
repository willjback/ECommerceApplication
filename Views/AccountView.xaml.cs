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
using System.IO;

namespace ECommerceApplication.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public event EventHandler UsernameChanged;

        string filepath = @"C:\MSSA\20483\Project\ECommerceApplication\userinfo.txt";
        ECommerceEntities entities = new ECommerceEntities();

        public AccountView()
        {
            InitializeComponent();
        }

        private void BtnChangeUsername_Click(object sender, RoutedEventArgs e)
        {
            if (TxtNewUsername.Text == string.Empty || TxtConfirmUsername.Text == string.Empty)
            {
                MessageBox.Show("Username text fields must be filled out!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TxtNewUsername.Text != TxtConfirmUsername.Text)
            {
                MessageBox.Show("Usernames don't match!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string currentUsername = File.ReadLines(filepath).Skip(1).Take(1).First();
            var user = entities.Users.FirstOrDefault(u => currentUsername == u.Username);

            if (user == null)
            {
                MessageBox.Show("There was an error. Returned user is NULL.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TxtNewUsername.Text == user.Username)
            {
                MessageBox.Show("That's already your username!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"New username: {TxtNewUsername.Text}. Do you want to continue?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            user.Username = TxtNewUsername.Text;
            entities.SaveChanges();

            string[] arrLines = File.ReadAllLines(filepath);
            arrLines[1] = TxtNewUsername.Text;
            File.WriteAllLines(filepath, arrLines);

            RefreshAccountPage();

            UsernameChanged(this, null);
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {

            if (TxtCurrentPassword.Password == string.Empty || TxtNewPassword.Password == string.Empty || TxtConfirmPassword.Password == string.Empty)
            {
                MessageBox.Show("Password text fields must be filled out!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string currentPassword = File.ReadLines(filepath).Skip(2).Take(1).First();

            if (TxtCurrentPassword.Password != currentPassword)
            {
                MessageBox.Show("Current password invalid", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TxtNewPassword.Password != TxtConfirmPassword.Password)
            {
                MessageBox.Show("Passwords don't match!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = entities.Users.FirstOrDefault(u => currentPassword == u.Password);

            if (user == null)
            {
                MessageBox.Show("There was an error. Returned user is NULL.", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (TxtNewPassword.Password == user.Password)
            {
                MessageBox.Show("That's already your password!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Password will be changed. Do you want to continue?", "", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.No)
                return;

            user.Password = TxtNewPassword.Password;
            entities.SaveChanges();

            string[] arrLines = File.ReadAllLines(filepath);
            arrLines[1] = TxtNewPassword.Password;
            File.WriteAllLines(filepath, arrLines);

            RefreshAccountPage();
        }

        private void RefreshAccountPage()
        {
            entities.SaveChanges();
            int userId = Int32. Parse(File.ReadLines(filepath).Take(1).First());

            var user = entities.Users.FirstOrDefault(u => u.UserID == userId);

            if (user != null)
            {
                LblUserIDAccountPage.Content = user.UserID;
                LblUsernameAccountPage.Content = user.Username;
                LblBalanceAccountPage.Content = String.Format("{0:C}", decimal.Parse(File.ReadLines(filepath).Skip(4).Take(1).First()));
                if (user.IsAdmin)
                    LblAccountTypeAccountPage.Content = "Admin";
                else
                    LblAccountTypeAccountPage.Content = "User";
            }

            TxtNewUsername.Clear();
            TxtConfirmUsername.Clear();
            TxtCurrentPassword.Clear();
            TxtNewPassword.Clear();
            TxtConfirmPassword.Clear();
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            RefreshAccountPage();
        }
    }
}
