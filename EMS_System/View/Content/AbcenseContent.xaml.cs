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
using System.Collections.ObjectModel;

namespace EMS_System.View.Content
{
    /// <summary>
    /// Interaction logic for AbcenseContent.xaml
    /// </summary>
    public partial class AbcenseContent : UserControl
    {
        DatabaseHandler dbh = new DatabaseHandler();
        public AbcenseContent()
        {
            InitializeComponent();
            LoadText();
            CalculateOvertime();
        }

        public void LoadText()
        {
            txtblck_ClockhoursHeader.Text = XMLReader.GetText("ClockHoursHeader");
            txtblck_PresenceHeader.Text = XMLReader.GetText("PresenceHeader");
            txtblck_AbsenceHeader.Text = XMLReader.GetText("AbsenceHeader");
            txtblck_ProflowHeader.Text = XMLReader.GetText("ProflowHeader");
            txtblck_OvertimeHeader.Text = XMLReader.GetText("OvertimeHeader");
        }

        public void CalculateOvertime()
        {
            //Show overtime per month
            dbh.OpenConnection();
            ObservableCollection<string> clockhours = dbh.GetClockHours(1);
            ObservableCollection<string> clockHoursStartTimes = new ObservableCollection<string>();
            ObservableCollection<string> clockHoursEndTimes = new ObservableCollection<string>();

            foreach (string clockHours in clockhours)
                clockHoursStartTimes.Add(clockHours.Substring(0, 5));

            foreach (string clockHours in clockhours)
                clockHoursEndTimes.Add(clockHours.Substring(6, 5));




        }
    }
}
