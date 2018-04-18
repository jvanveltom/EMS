using System.Windows;
using System.Windows.Input;

namespace EMS_System
{
    /// <summary>
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Window
    {
        public SearchPage()
        {
            InitializeComponent();
            SetSelectedItemCommand = new RelayCommand(ExecuteStartServicesCommand, CanExecuteStartServicesCommand);
        }

        private bool CanExecuteStartServicesCommand(object obj) // mag ik dit uitvoeren?
        {
            return true; // ja / nee
        }

        private void ExecuteStartServicesCommand(object obj) // wat moet ik uitvoeren
        {
            MessageBox.Show("Hoi"); // vul hier de sql mee, in het geval van deze pagina
        }

        public ICommand SetSelectedItemCommand { get; private set; }
    }
}
