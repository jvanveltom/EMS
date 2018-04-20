using System.Collections.ObjectModel;
using EMS_System.Domain;
using EMS_System.Util;

namespace EMS_System.ViewModel.Content
{
    public class GeneralContentViewModel : NotifyPropertyChangedBase
    {
        private Person _person;
        private string _profileHeader;

        public GeneralContentViewModel()
        {
            Person = new Person
            {
                Name = "Henk",
                ProfileData = new ObservableCollection<string> { "Data 1", "Data 2" },
                Deparments = new ObservableCollection<string> { "Department 1", "Department 2" },
                ClockHours = new ObservableCollection<string> { "Monday: \t 09:00 - 18:00" },
                Functions = new ObservableCollection<string> { "Function 1" }
            };
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
