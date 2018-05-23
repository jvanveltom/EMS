using System.Collections.ObjectModel;
using EMS_System.View;
using EMS_System.Domain;
using EMS_System.Util;

namespace EMS_System.ViewModel.Content
{
    public class AdminContentViewModel : NotifyPropertyChangedBase
    {
        // Uiteindelijke informatie
        // Hier moet dus de SQL in in plaats van de huidige dummy
        private Person _person;
        private DatabaseHandler dbh = new DatabaseHandler();

        public AdminContentViewModel()
        {
            dbh.OpenConnection();
            Person = new Person
            {
                Name = dbh.GetUsername(View.MainWindow.employee_ID),
                Department = dbh.GetDepartment(View.MainWindow.employee_ID),
                ProfileData = new ObservableCollection<string> { dbh.GetUsername(View.MainWindow.employee_ID), dbh.GetEmployeeAddress(View.MainWindow.employee_ID), dbh.GetEmployeeZipcode(View.MainWindow.employee_ID), dbh.GetEmployeeEmail(View.MainWindow.employee_ID) }
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
