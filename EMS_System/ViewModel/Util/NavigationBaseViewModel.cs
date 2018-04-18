using System.Collections.ObjectModel;
using EMS_System.Util;

namespace EMS_System.ViewModel.Util
{
    public class NavigationBaseViewModel<T> : NotifyPropertyChangedBase
    {
        private ObservableCollection<T> _itemsSource;
        private T _selectedItem;

        public ObservableCollection<T> ItemsSource
        {
            get { return _itemsSource; }
            set
            {
                _itemsSource = value;
                OnPropertyChanged();
            }
        }

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
                OnSelectedItemChanged();
            }
        }

        protected virtual void OnSelectedItemChanged()
        {
        }
    }
}
