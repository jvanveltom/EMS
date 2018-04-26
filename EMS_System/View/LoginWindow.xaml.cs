using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using EMS_System.Util;
using EMS_System.View;

namespace EMS_System.View
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public DatabaseHandler dbh = new DatabaseHandler();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void TextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                tb.SelectAll();
            }
        }

        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            dbh.OpenConnection();
            if (dbh.Login(txtb_Username.Text, txtb_Password.Password))
            {
                MainWindow mainWindow = new MainWindow(dbh.GetLoggedInUserID(txtb_Username.Text, txtb_Password.Password));
                dbh.CloseConnection();
                mainWindow.Owner = this;
                this.Hide();
                mainWindow.ShowDialog();
            }
            else
            {
                dbh.CloseConnection();
                MessageBox.Show("Incorrect username or password", "Login error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

