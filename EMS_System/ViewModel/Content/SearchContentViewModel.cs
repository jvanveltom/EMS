using System.Collections.ObjectModel;
using EMS_System.Domain;
using EMS_System.ViewModel.Util;

namespace EMS_System.ViewModel.Content
{
    public class SearchContentViewModel : NavigationBaseViewModel<Person>
    {
        public void ExecuteSearch(string searchParamter)
        {
            // TODO: Add search

            ItemsSource = new ObservableCollection<Person>
            {
                new Person { Name = $"{searchParamter} 0"},
                new Person { Name = $"{searchParamter} 1"},
                new Person { Name = $"{searchParamter} 2"}
            };
        }
    }
}
