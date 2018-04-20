using System.Collections.ObjectModel;
using EMS_System.Domain;
using EMS_System.Util;

namespace EMS_System.ViewModel.Content
{
    public class AbcenseContentViewModel : NotifyPropertyChangedBase
    {
        private Person _person;

        public AbcenseContentViewModel()
        {
            Person = new Person
            {
                Name = "Henk",
                ProfileData = new ObservableCollection<string> { "data 1", "data 2" },
                Deparments = new ObservableCollection<string> { "Department 1", "Department 2" },
                Residence = new ObservableCollection<string> { "Breda" },
                ClockHours = new ObservableCollection<string> { "Monday: \t 09:00 - 18:00"},
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
            }
        }

    }
}
