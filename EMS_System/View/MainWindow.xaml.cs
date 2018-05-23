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
using EMS_System.Resource;
using EMS_System.Util;

namespace EMS_System.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int employee_ID;

        DatabaseHandler dbh;
        public MainWindow(int emp_ID)
        {
            employee_ID = emp_ID;
            InitializeComponent();
            LoadText();
            dbh = new DatabaseHandler();

            AdminCheck(employee_ID.ToString());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            dbh.OpenConnection();
            dbh.Checkout(employee_ID);
            dbh.CloseConnection();

            Environment.Exit(8);
        }

        public virtual int GetLoggedInUserID()
        {
            return employee_ID;
        }

        private void SearchOnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btn_Search.Command.Execute(SearchTextBox.Text);
            }
        }

        private void AdminCheck(string employee_ID)
        {
            int adminRights;

            dbh.OpenConnection();
            adminRights = dbh.CheckForAdminRights(employee_ID);

            if (adminRights == 1)
            {
                btn_AdminMenuPage.Visibility = Visibility.Visible;
            }
            else
            {
                btn_AdminMenuPage.Visibility = Visibility.Hidden;
            }
            dbh.CloseConnection();
        }

        public void LoadText()
        {
            btn_HomeMenuPage.Content = XMLReader.GetText("MenuHome");
            btn_AbsenceMenuPage.Content = XMLReader.GetText("MenuAbsence");
            btn_ProfileMenuPage.Content = XMLReader.GetText("MenuProfile");
            btn_AdminMenuPage.Content = XMLReader.GetText("MenuAdmin");
            SearchTextBox.BackgroundText = XMLReader.GetText("SearchTextboxPlaceHolder");
        }
    }
}
