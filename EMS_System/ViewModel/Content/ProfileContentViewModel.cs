using System.Collections.ObjectModel;
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

        public ProfileContentViewModel()
        {
            // TODO remove dummy
            Person = new Person
            {
                Name = "Henk",
                ProfileData = new ObservableCollection<string> { "data 1", "data 2" },
                Deparments = new ObservableCollection<string> {"deparment 1", "deparment 2" },
                Functions = new ObservableCollection<string> { "function 1" }
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
