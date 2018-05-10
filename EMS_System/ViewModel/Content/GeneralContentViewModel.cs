using System.Collections.ObjectModel;
using EMS_System.Domain;
using EMS_System.Util;
using EMS_System.Resource;
using System.Collections.Generic;

namespace EMS_System.ViewModel.Content
{
    public class GeneralContentViewModel : NotifyPropertyChangedBase
    {
        private Person _person;
        private string _profileHeader;
        private DatabaseHandler dbh = new DatabaseHandler();

        public GeneralContentViewModel()
        {
            dbh.OpenConnection();
            Person = new Person
            {
                Name = "Henk",
                Overtime = 5,
                Residence = new ObservableCollection<string> { "Zevenbergen", "Breda" },
                ProfileData = new ObservableCollection<string> { "Data 1", "Data 2" },
                //Deparments = new ObservableCollection<string> { "Department 1", "Department 2" },
                ClockHours = LoadClockhours(dbh.GetClockHours(1)),
                Functions = new ObservableCollection<string> { "Function 1" }
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

                ProfileHeader = $"Profile: {Person.Name}";
            }
        }


        public string ProfileHeader
        {
            get { return _profileHeader; }
            set
            {
                _profileHeader = value;
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
                    clockhoursPerDay = list_days[i] + "\t\t" + clockHours[i];
                    result.Add(clockhoursPerDay);
                }
                else
                {
                    clockhoursPerDay = list_days[i] + "\t" + clockHours[i];
                    result.Add(clockhoursPerDay);
                }
            }
            return result;
        }
    }
}
