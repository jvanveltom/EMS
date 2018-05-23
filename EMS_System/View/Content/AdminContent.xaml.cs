using System;
using System.Collections.ObjectModel;
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
using System.Collections.Generic;

namespace EMS_System.View.Content
{
    /// <summary>
    /// Interaction logic for ProfileContent.xaml
    /// </summary>
    public partial class AdminContent : UserControl
    {
        private DatabaseHandler dbh = new DatabaseHandler();
        private List<List<string>> listOfAllAbsences = new List<List<string>>();
        public ObservableCollection<string> temp = new ObservableCollection<string>();

        public AdminContent()
        {
            dbh.OpenConnection();
            InitializeComponent();
            listViewAbsence.ItemsSource = ReadOutAbsences(dbh.GetAbsence());
            dbh.CloseConnection();
            LoadText();
        }

        public ObservableCollection<string> ReadOutAbsences(List<List<string>> allAbsences)
        {
            ObservableCollection<string> absence = new ObservableCollection<string>();
            List<string> usernames = allAbsences[0];
            List<string> convertedUsernames = new List<string>();
            List<string> absenceDates = allAbsences[1];
            List<string> absencesAuthorization = allAbsences[2];
            List<string> convertedAbsencesAuthorization = new List<string>();

            foreach (string id in usernames)
            {
                convertedUsernames.Add(dbh.GetUsername(Convert.ToInt32(id)));
            }


            foreach (string authorization in absencesAuthorization)
            {
                if (authorization == "0")
                    convertedAbsencesAuthorization.Add("Unauthorized");
                else
                    convertedAbsencesAuthorization.Add("Authorized");
            }

            for (int i = 0; i < usernames.Count; i++)
            {
                if (convertedUsernames[i].Length >= 17)
                    absence.Add(convertedUsernames[i] + "\t\t" + absenceDates[i] + "\t\t\t" + convertedAbsencesAuthorization[i]);
                else
                    absence.Add(convertedUsernames[i] + "\t\t\t" + absenceDates[i] + "\t\t\t" + convertedAbsencesAuthorization[i]);
            }

            return absence;
        }

        private void listViewAbsence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dbh.OpenConnection();
            listViewAvailableEmployees.ItemsSource = dbh.GetEmployableEmployees(listViewAbsence.SelectedItem.ToString().Split('\t')[0]);
            dbh.CloseConnection();
        }

        public void LoadText()
        {
            txtblck_AbsenceHeader.Text = XMLReader.GetText("AbsenceHeader");
            txtblck_AvailableEmployeesHeader.Text = XMLReader.GetText("AvailableEmployeesHeader");
        }
    }
}
