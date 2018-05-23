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
    /// Interaction logic for GeneralContent.xaml
    /// </summary>
    public partial class GeneralContent : UserControl
    {
        public GeneralContent()
        {
            InitializeComponent();
            LoadText();
        }

        public void LoadText()
        {
            txtblck_ClockhoursHeader.Text = XMLReader.GetText("ClockHoursHeader");
            txtblck_PresenceHeader.Text = XMLReader.GetText("PresenceHeader");
            txtblck_ProflowHeader.Text = XMLReader.GetText("ProflowHeader");
            txtblck_AbsenceHeader.Text = XMLReader.GetText("AbsenceHeader");
            txtblck_OvertimeHeader.Text = XMLReader.GetText("OvertimeHeader");
        }
    }
}
