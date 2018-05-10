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
                Name = dbh.GetUsername(1),
                ProfileData = new ObservableCollection<string> { "data 1", "data 2" },
                //Deparments = new ObservableCollection<string> { "Department 1", "Department 2" },
                Residence = new ObservableCollection<string> { "Breda" },
                ClockHours = LoadClockhours(dbh.GetClockHours(1)),
                Functions = new ObservableCollection<string> { "function 1" }
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

    }
}
