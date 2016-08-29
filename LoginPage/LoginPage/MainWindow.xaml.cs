using System;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace LoginPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();           
        }
        
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var u = new DbLogic();
            Exception ex = null;
          //  var result = u.ConectToDb(UserTextBox.Text, passwordBox.Password, out ex);

            if (u.ConectToDb(UserTextBox.Text, passwordBox.Password, out ex))
                MessageBox.Show("User not found!" + ex.Message);
            else
            {
                var result = MessageBox.Show("UserExists!", "To show the statistic?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                    u.CollectUserStatistics(out ex);
            }
            
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            var u = new DbLogic();
            Exception ex;
            var result = u.AddNewUserToDb(UserTextBox.Text, passwordBox.Password, out ex);
            MessageBox.Show(!result ? ex.Message : "User added successfully");
        }
    }
}
