using System;
using System.Data;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

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
            DbLogic u = new DbLogic();
            var result = u.ConectToDB("select * from dbo.Users");

            if (result != null)
                MessageBox.Show(result.Message);

            //var users = u.Users;
            string password = passwordBox.Password;
            string user_Id = UserTextBox.Text;
            if (u.CheckUserCredentionals(user_Id, password))
                MessageBox.Show("UserExists!");
            else
                MessageBox.Show("User not found!");

        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            DbLogic u = new DbLogic();
            var result = u.AddNewUserToDB(UserTextBox.Text, passwordBox.Password);

            if (result != null)
                MessageBox.Show(result.Message);
            else
                MessageBox.Show("User added successfully");

        }
    }
}
