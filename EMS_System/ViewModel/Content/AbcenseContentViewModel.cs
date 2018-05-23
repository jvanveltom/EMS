using System.Collections.ObjectModel;
using EMS_System.Domain;
using EMS_System.Util;
using System.Collections.Generic;

namespace EMS_System.ViewModel.Content
{
    public class AbcenseContentViewModel : NotifyPropertyChangedBase
    {
        private DatabaseHandler dbh = new DatabaseHandler();
        private Person _person;

        public AbcenseContentViewModel()
        {
            dbh.OpenConnection();
            Person = new Person
            {
                Name = dbh.GetUsername(View.MainWindow.employee_ID),
                ClockHours = LoadClockhours(dbh.GetClockHours(View.MainWindow.employee_ID)),
                Absence = LoadAbsence(dbh.GetAbsence(View.MainWindow.employee_ID)),
                Presence = LoadPresence(View.MainWindow.employee_ID)
            };
            dbh.CloseConnection();
        }

        public Person Person
        {
            get { return _person; }
            set
            {
                _person = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> LoadClockhours(ObservableCollection<string> clockHours)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            List<string> list_days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };

            for (int i = 0; i < clockHours.Count; i++)
            {
                string clockhoursPerDay;
                if (list_days[i].Length < 8)
                {
                    clockhoursPerDay = list_days[i] + "\t\t\t" + clockHours[i];
                    result.Add(clockhoursPerDay);
                }
                else
                {
                    clockhoursPerDay = list_days[i] + "\t\t" + clockHours[i];
                    result.Add(clockhoursPerDay);
                }
            }
            return result;
        }

        public ObservableCollection<string> LoadAbsence(ObservableCollection<ObservableCollection<string>> absence)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            ObservableCollection<string> absenceDates = absence[0];
            ObservableCollection<string> absenceAuthorization = absence[1];
            ObservableCollection<string> convertedAbsenceAuthorization = new ObservableCollection<string>();

            foreach (string authorization in absenceAuthorization)
            {
                if (authorization == "0")
                    convertedAbsenceAuthorization.Add("Unauthorized");
                else
                    convertedAbsenceAuthorization.Add("Authorized");
            }

            for (int i = 0; i < absenceDates.Count; i++)
            {
                result.Add(absenceDates[i].ToString() + "\t\t" + convertedAbsenceAuthorization[i]);
            }

            return result;
        }

        public ObservableCollection<string> LoadPresence(int employee_ID)
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            ObservableCollection<ObservableCollection<string>> checkins = dbh.GetCheckin(employee_ID);
            ObservableCollection<ObservableCollection<string>> checkouts = dbh.GetCheckOut(employee_ID);

            ObservableCollection<string> checkinDates = checkins[0];
            ObservableCollection<string> checkinTimes = checkins[1];
            ObservableCollection<string> checkoutTimes = checkouts[1];

            for (int i = 0; i < checkoutTimes.Count; i++)
            {
                result.Add(checkinDates[i].ToString() + "\t\t" + checkinTimes[i].ToString() + "-" + checkoutTimes[i].ToString());
            }

            return result;
        }

    }
}
