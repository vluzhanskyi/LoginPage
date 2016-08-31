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
            var u = new DbConectedLayer();
            Exception ex;
          //  var result = u.ConectToDb(UserTextBox.Text, passwordBox.Password, out ex);

            if (!u.ConectToDb(UserTextBox.Text, passwordBox.Password, out ex))
                MessageBox.Show("User not found!" + ex.Message);
            else
            {
                var result = MessageBox.Show("To show the statistic?", "UserExists!", MessageBoxButton.YesNo);
                if (result != MessageBoxResult.Yes) return;
                u.CollectUserStatistics(out ex);
                if (ex != null)
                    MessageBox.Show("Fail to get Statistic \n" + ex.Message);
                else
                {
                    UserStatistics stat = new UserStatistics();
                    foreach(var s in u.User.GamesScores)
                    {
                        stat.textBox.Text.Insert(s.Value, s.Key);
                    }
                }
            }
            
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            var u = new DbConectedLayer();
            Exception ex;
            var result = u.AddNewUserToDb(UserTextBox.Text, passwordBox.Password, out ex);
            MessageBox.Show(!result ? ex.Message : "User added successfully");
        }
    }
}
