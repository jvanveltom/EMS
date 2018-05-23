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
using System.Security.Cryptography;

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
            LoadText();
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
            Console.WriteLine(CreateHash(txtb_Password.Password));
            dbh.OpenConnection();
            if (dbh.Login(txtb_Username.Text, CreateHash(txtb_Password.Password)))
            {
                dbh.Checkin(dbh.GetLoggedInUserID(txtb_Username.Text, CreateHash(txtb_Password.Password)));
                MainWindow mainWindow = new MainWindow(dbh.GetLoggedInUserID(txtb_Username.Text, CreateHash(txtb_Password.Password)));
                dbh.CloseConnection();
                mainWindow.Owner = this;
                this.Hide();
                //Console.WriteLine(DateTime.Now.ToString("hh:mm tt"));
                //Console.WriteLine(DateTime.Now.ToString("dd-MM-yyyy"));
                mainWindow.ShowDialog();
            }
            else
            {
                dbh.CloseConnection();
                MessageBox.Show("Incorrect username or password", "Login error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void btn_Enter_Pushed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtb_Username.Text != "" && txtb_Password.Password != "")
                {
                    dbh.OpenConnection();
                    if (dbh.Login(txtb_Username.Text, CreateHash(txtb_Password.Password)))
                    {
                        MainWindow mainWindow = new MainWindow(dbh.GetLoggedInUserID(txtb_Username.Text, CreateHash(txtb_Password.Password)));
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

        private string CreateHash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputInBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashedInput = md5.ComputeHash(inputInBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hashedInput.Length; i++)
            {
                sb.Append(hashedInput[i].ToString("X2"));
            }
            return sb.ToString();

        }

        public void LoadText()
        {
            txtblck_LoginHeader.Text = XMLReader.GetText("LoginHeader");
            btn_Login.Content = XMLReader.GetText("LoginButton");
        }

        private void txtb_Username_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void txtb_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).SelectAll();
        }
    }
}

