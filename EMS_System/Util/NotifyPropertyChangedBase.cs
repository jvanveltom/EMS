using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EMS_System.Util
{
   public  class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        // De basisimplementatie van het OnPropertyChanged, wordt toegelicht in Person
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
