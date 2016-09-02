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
        DbConectedLayer con = new DbConectedLayer("SQLSERVER\\SQLEXPRESS", "Test1", "sa", "nicecti1!");
        DbDisconnectedLayer disCon = new DbDisconnectedLayer("SQLSERVER\\SQLEXPRESS", "Test1", "sa", "nicecti1!");
        public MainWindow()
        {
            InitializeComponent();  
                     
        }
        
        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //var con = new DbConectedLayer("SQLSERVER\\SQLEXPRESS", "Test1", "sa", "nicecti1!");
           

            Exception ex;

            disCon.GetDataFromDb();

            //if (!con.ConectToDb(UserTextBox.Text, passwordBox.Password, out ex))
            //    MessageBox.Show("User not found!" + ex.Message);
            //else
            //{
            //    var result = MessageBox.Show("To show the statistic?", "UserExists!", MessageBoxButton.YesNo);
            //    if (result != MessageBoxResult.Yes) return;
            //    con.CollectUserStatistics(out ex);
            //    if (ex != null)
            //        MessageBox.Show("Fail to get Statistic \n" + ex.Message);
            //    else
            //    {
            //        UserStatistics stat = new UserStatistics();
            //        foreach (var s in con.User.GamesScores)
            //        {
            //            stat.textBox.AppendText(string.Format("{0}     -    {1}", s.Key, s.Value));
            //            stat.textBox.AppendText(Environment.NewLine);
            //        }
            //        stat.Show();
            //    }
            //}

        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {           
            Exception ex;
            var result = con.AddNewUserToDb(UserTextBox.Text, passwordBox.Password, out ex);
            MessageBox.Show(!result ? ex.Message : "User added successfully");
        }
    }
}
