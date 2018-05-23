using System.Windows;
using System.Windows.Input;
using EMS_System.Util;
using EMS_System.View.Content;
using EMS_System.ViewModel.Content;

namespace EMS_System.ViewModel
{
    public class MainViewModel : NotifyPropertyChangedBase
    {
        private FrameworkElement _mainContentElement;
        private string _searchInput;

        /* views */
        private readonly AbcenseContent _abcenseContent;
        private readonly AdminContent _adminContent;
        private readonly GeneralContent _generalContent;
        private readonly ProfileContent _profileContent;
        private readonly SearchContent _searchContent;

        /* view models */
        private readonly AbcenseContentViewModel _abcenseContentViewModel = new AbcenseContentViewModel();
        private readonly AdminContentViewModel _adminContentViewModel = new AdminContentViewModel();
        private readonly GeneralContentViewModel _generalContentViewModel = new GeneralContentViewModel();
        private readonly ProfileContentViewModel _profileContentViewModel = new ProfileContentViewModel();
        private readonly SearchContentViewModel _searchContentViewModel = new SearchContentViewModel();

        public MainViewModel()
        {
            _abcenseContent = new AbcenseContent {DataContext = _abcenseContentViewModel};
            _adminContent = new AdminContent {DataContext = _adminContentViewModel};
            _generalContent = new GeneralContent {DataContext = _generalContentViewModel};
            _profileContent = new ProfileContent {DataContext = _profileContentViewModel};
            _searchContent = new SearchContent {DataContext = _searchContentViewModel};

            AbcenseNavigationCommand = new RelayCommand(AbcenseNavigationCommandExecute);
            AdminNavigationCommand = new RelayCommand(AdminNavigationCommandExecute);
            GeneralNavigationCommand = new RelayCommand(GeneralNavigationCommandExecute);
            ProfileNavigationCommand = new RelayCommand(ProfileNavigationCommandExecute);
            SearchCommand = new RelayCommand(SearchCommandExecute, SearchCommandCanExecute);

            MainContentElement = _generalContent;
        }

        public FrameworkElement MainContentElement
        {
            get { return _mainContentElement; }
            set
            {
                _mainContentElement = value;
                OnPropertyChanged();
            }
        }

        public string SearchInput
        {
            get { return _searchInput; }
            set
            {
                _searchInput = value;
                OnPropertyChanged();
            }
        }

        public ICommand AbcenseNavigationCommand { get; private set; }

        public ICommand GeneralNavigationCommand { get; private set; }

        public ICommand ProfileNavigationCommand { get; private set; }

        public ICommand AdminNavigationCommand { get; private set; }

        public ICommand SearchCommand { get; private set; }

        private void AbcenseNavigationCommandExecute(object searchParamter)
        {
            MainContentElement = _abcenseContent;
        }

        private void GeneralNavigationCommandExecute(object searchParamter)
        {
            MainContentElement = _generalContent;
        }

        private void ProfileNavigationCommandExecute(object searchParamter)
        {
            MainContentElement = _profileContent;
        }

        private void AdminNavigationCommandExecute(object searchParamter)
        {
            MainContentElement = _adminContent;
        }

        private void SearchCommandExecute(object searchParamter)
        {
            MainContentElement = _searchContent;
            _searchContentViewModel.ExecuteSearch((string)searchParamter);

            SearchInput = null;
        }

        private static bool SearchCommandCanExecute(object searchParamter)
        {
            return !string.IsNullOrEmpty((string)searchParamter);
        }
    }
}
