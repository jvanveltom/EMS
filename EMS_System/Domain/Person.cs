using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using EMS_System.Util;

namespace EMS_System.Domain
{
    public class Person : NotifyPropertyChangedBase
    {
        // Deze class bevat de data van de werknemer
        // Roep je aan bij profielpagina en in zoekresultaten
        private string _name;
        private object _photo;
        // ObservableCollection -> geeft aan wanneer een collectie gewijzigd is
        // Triggert een event wanneer de collectie wijzigd (niet de items in de lijst zitten)
        // BV. iets toevoegen / weggooien = trigger, item binnen de collectie wijzigd = geen trigger
        private ObservableCollection<string> _profileData;
        private ObservableCollection<string> _deparments;
        private ObservableCollection<string> _functions;
        private ObservableCollection<string> _residence;
        private ObservableCollection<string> _clockHours;

        // Constructor
        public Person()
        {
            _profileData = new ObservableCollection<string>();
        }

        // In dit geval heeft de persoon een naam
        // Return de naam
        // Property wijzigd -> wil je hier iets mee (onpropertychanged)
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Residence
        {
            get { return _residence; }
            set
            {
                _residence = value;
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
                // nameof = pak de naam van het volgende object
                OnPropertyChanged(nameof(DeparmentsAndFunctions));
            }
        }

        public ObservableCollection<string> ClockHours
        {
            get { return _clockHours; }
            set
            {
                _clockHours = value;
                OnPropertyChanged();
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
                // We gebruiken een list want we hoeven heb nog niet te triggeren bij een verandering
                var result = new List<string>();
                result.AddRange(Deparments.OrderBy(deparment => deparment));
                result.AddRange(Functions.OrderBy(functions => functions));

                return new ObservableCollection<string>(result);
            }
        }
    }
}
