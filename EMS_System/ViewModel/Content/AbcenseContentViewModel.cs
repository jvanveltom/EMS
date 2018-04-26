using System.Collections.ObjectModel;
using EMS_System.Domain;
using EMS_System.Util;

enum Days
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday
};

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
                Name = "Henk",
                ProfileData = new ObservableCollection<string> { "data 1", "data 2" },
                //Deparments = new ObservableCollection<string> { "Department 1", "Department 2" },
                Residence = new ObservableCollection<string> { "Breda" },
                ClockHours = dbh.GetClockHours(1),
                //ClockHours = new ObservableCollection<string> { "Monday: \t 09:00 - 18:00"},
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

    }
}
