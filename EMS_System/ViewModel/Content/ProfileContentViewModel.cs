using System.Collections.ObjectModel;
using EMS_System.View;
using EMS_System.Domain;
using EMS_System.Util;

namespace EMS_System.ViewModel.Content
{
    public class ProfileContentViewModel : NotifyPropertyChangedBase
    {
        // Uiteindelijke informatie
        // Hier moet dus de SQL in in plaats van de huidige dummy
        private Person _person;
        private string _profileHeader;
        private DatabaseHandler dbh = new DatabaseHandler();

        public ProfileContentViewModel()
        {
            dbh.OpenConnection();
            Person = new Person
            {
                Name = dbh.GetUsername(1),
                Department = "R&D",
                ProfileData = new ObservableCollection<string> { dbh.GetUsername(1), dbh.GetEmployeeAddress(1), dbh.GetEmployeeZipcode(1), dbh.GetEmployeeEmail(1) },
                Functions = new ObservableCollection<string> { "Developer", "Salesman" }
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
    }
}
