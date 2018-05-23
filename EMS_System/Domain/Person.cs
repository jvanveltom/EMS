﻿using System.Collections.Generic;
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
        private string _address;
        private string _zipcode;
        private int _overtime;
        private object _photo;
        // ObservableCollection -> geeft aan wanneer een collectie gewijzigd is
        // Triggert een event wanneer de collectie wijzigd (niet de items in de lijst zitten)
        // BV. iets toevoegen / weggooien = trigger, item binnen de collectie wijzigd = geen trigger
        private ObservableCollection<string> _profileData;
        private ObservableCollection<string> _function;
        private ObservableCollection<string> _residence;
        private ObservableCollection<string> _clockHours;
        private ObservableCollection<string> _absence;
        private ObservableCollection<string> _department;
        private ObservableCollection<string> _presence;

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

        public int Overtime
        {
            get { return _overtime; }
            set
            {
                _overtime = value;
                OnPropertyChanged();                //Not sure if used
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

        public ObservableCollection<string> Presence
        {
            get { return _presence; }
            set
            {
                _presence = value;
                OnPropertyChanged();
            }
        }

        public string Zipcode
        {
            get { return _zipcode; }
            set
            {
                _zipcode = value;
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

        public ObservableCollection<string> Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(DeparmentsAndFunctions));
            }
        }

        public ObservableCollection<string> Absence
        {
            get { return _absence; }
            set
            {
                _absence = value;
                OnPropertyChanged();
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
            get { return _function; }
            set
            {
                _function = value;
                OnPropertyChanged();
                //OnPropertyChanged(nameof(DeparmentsAndFunctions));
            }
        }

        //public ObservableCollection<string> DeparmentsAndFunctions
        //{
        //    get
        //    {
        //        // We gebruiken een list want we hoeven heb nog niet te triggeren bij een verandering
        //        var result = new List<string>();
        //        result.Add(Department);
        //        result.AddRange(Functions.OrderBy(functions => functions));

        //        return new ObservableCollection<string>(result);
        //    }
        //}

        public ObservableCollection<string> 
            alData
        {
            get
            {
                var result = new List<string>();
                result.Add(Name);
                result.AddRange(Residence.OrderBy(residence => residence));
                result.Add(Zipcode);

                return new ObservableCollection<string>(result);
            }
        }
    }
}
