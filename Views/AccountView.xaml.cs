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
            if (TxtNewUsername.Text != TxtConfirmUsername.Text)
            {
                MessageBox.Show("Usernames don't match!", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string currentUsername = File.ReadLines(filepath).Take(1).First();
            var user = entities.Users.FirstOrDefault(u => currentUsername == u.Username);

            if (user == null)
            {
                MessageBox.Show("There was an error. Username is NULL.", "", MessageBoxButton.OK, MessageBoxImage.Error);
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
            arrLines[0] = TxtNewUsername.Text;
            File.WriteAllLines(filepath, arrLines);

            UsernameChanged(this, null);
        }

        private void BtnChangePassword_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
