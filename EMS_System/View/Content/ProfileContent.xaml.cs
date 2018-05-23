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
using EMS_System.Util;

namespace EMS_System.View.Content
{
    /// <summary>
    /// Interaction logic for ProfileContent.xaml
    /// </summary>
    public partial class ProfileContent : UserControl
    {
        public ProfileContent()
        {
            InitializeComponent();
            LoadText();
        }

        public void LoadText()
        {
            txtblck_DepartmentHeader.Text = XMLReader.GetText("DepartmentHeader");
            txtblck_FunctionHeader.Text = XMLReader.GetText("FunctionHeader");
        }
    }
}
