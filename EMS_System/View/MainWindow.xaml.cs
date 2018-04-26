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
        public int employee_ID;

        DatabaseHandler dbh;
        public MainWindow(int emp_ID)
        {
            InitializeComponent();
            dbh = new DatabaseHandler();
            employee_ID = emp_ID;

            Console.WriteLine(GetLoggedInUserID());
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Environment.Exit(8);
        }

        public virtual int GetLoggedInUserID()
        {
            return employee_ID;
        }
    }
}
