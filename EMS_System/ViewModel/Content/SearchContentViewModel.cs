using System.Collections.ObjectModel;
using EMS_System.Domain;
using EMS_System.Util;
using EMS_System.ViewModel.Util;

namespace EMS_System.ViewModel.Content
{
    public class SearchContentViewModel : NavigationBaseViewModel<Person>
    {
        DatabaseHandler dbh = new DatabaseHandler();
        // Uiteindelijke informatie
        // Hier moet dus de SQL in in plaats van de huidige collection
        public void ExecuteSearch(string searchParamter)
        {
            // TODO: Add search
            dbh.OpenConnection();

            ItemsSource = dbh.SearchForEmployee(searchParamter);

            dbh.CloseConnection();
            //ItemsSource = new ObservableCollection<Person>
            //{
            //    foreach (string str in dbh.SearchForEmployee(searchParamter))
            //{

            //}
            //    //new Person { Name = $"{searchParamter} 0"},
            //    //new Person { Name = $"{searchParamter} 1"},
            //    //new Person { Name = $"{searchParamter} 2"}
            //};
        }
    }
}
