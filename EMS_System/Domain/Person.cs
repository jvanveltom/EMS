using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EMS_System.Util;

namespace EMS_System.Domain
{
    public class Person : NotifyPropertyChangedBase
    {
        private string _name;
        private object _photo;
        private ObservableCollection<string> _profileData;
        private ObservableCollection<string> _deparments;
        private ObservableCollection<string> _functions;

        public Person()
        {
            _profileData = new ObservableCollection<string>();
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public object Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> ProfileData
        {
            get { return _profileData; }
            set
            {
                _profileData = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Deparments
        {
            get { return _deparments; }
            set
            {
                _deparments = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DeparmentsAndFunctions));
            }
        }

        public ObservableCollection<string> Functions
        {
            get { return _functions; }
            set
            {
                _functions = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DeparmentsAndFunctions));
            }
        }

        public ObservableCollection<string> DeparmentsAndFunctions
        {
            get
            {
                // use a list cause we don't need to trigger the property changed event
                var result = new List<string>();
                result.AddRange(Deparments.OrderBy(deparment => deparment));
                result.AddRange(Functions.OrderBy(functions => functions));

                return new ObservableCollection<string>(result);
            }
        }
    }
}
