using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using Converters.Common;


namespace Converters.Models
{

    public class Student : ModelBase
    {
        public Student(Student inStudent)
        {
            ID = inStudent.ID;
            _firstName = inStudent._firstName;
            _lastName = inStudent._lastName;
            CurrentBagID = "";
            _hasBooks = false;
            _bShowingInfo = false;

        }

        public Student(string inID)
        {
            ID = inID;
            CurrentBagID = "";
            _hasBooks = false;
            _bShowingInfo = false;
        }



        public Student(string inID, string firstN, string lastN)
        {
            ID = inID;
            _firstName = firstN;
            _lastName = lastN;
            CurrentBagID = "";
            _hasBooks = false;
            _bShowingInfo = false;
        }


        private string _firstName;
        private string _lastName;
        [Required]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (object.ReferenceEquals(_firstName, value) != true)
                {
                    _firstName = value;
                    NotifyPropertyChanged("FirstName");
                }
            }
        }
        [Required]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (object.ReferenceEquals(_lastName, value) != true)
                {
                    _lastName = value;
                    NotifyPropertyChanged("LastName");
                }
            }
        }
        [Required]
        public string ID { get; set; }

        public string CurrentBagID { get; set; }

        private bool _hasBooks;
        public bool HasBooks
        {
            get
            {
                return _hasBooks; //for now return true
            }
            set
            {
                _hasBooks = value;
                NotifyPropertyChanged("HasBooks");
                NotifyPropertyChanged("CurrentBagID");
            }
        }
        private bool _weekOverdue;
        public bool WeekOverdue
        {
            get
            {
                return _weekOverdue; //for now return true
            }
            set
            {
                _weekOverdue = value;
                NotifyPropertyChanged("WeekOverdue");
            }
        }
        public string FullName
        {
            get
            {
                return (FirstName + " " + LastName);
            }

        }
        bool _bShowingInfo;
        public bool ShowingInfo
        {
            get
            {
                return _bShowingInfo; //for now return true
            }
            set
            {
                _bShowingInfo = value; ;
                
            }
        }
		public bool CheckIn()
		{
			CurrentBagID = "";
			HasBooks = false;
            WeekOverdue = false;

			return true;
		}

        public bool CheckOut(string bagID)
        {
            CurrentBagID = bagID;
            HasBooks = true;

            NotifyPropertyChanged("CurrentBagID");
            NotifyPropertyChanged("HasBooks");
            return true;
        }


        public bool Validate(ref string errorOut)
        {
            if (FirstName == "" || FirstName == null)
                errorOut = "invalid first name";
            if (LastName == "" || LastName == null )
                errorOut = "invalid last name";
            if (ID == "" || ID == null)
                errorOut = "invalid id";

            if (errorOut != "")
                return false;
            else return true;
        }

    }
}
