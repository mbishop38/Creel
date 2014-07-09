using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converters.Models;
using System.ComponentModel;
using Commanding;
using System.Diagnostics;

namespace Converters.ViewModels
{
    public class AStudentViewModel : NotifyAndDataError, IComparable 
    {
        private Student _aStudent;
        public RelayCommand _CheckInCommand { get; private set; }
   

        // Constructor
        public AStudentViewModel(Student student)
        {
            _aStudent = student;
            _CheckInCommand = new RelayCommand(HandleCheckInCommand, CanExecuteCheckInCommand);
        }

        // Views
        public Student Student
        {
            get { return this._aStudent; }
        }

        public int CompareTo(object obj)
        {
            AStudentViewModel stud = obj as AStudentViewModel;
            if (stud == null)
            {
                throw new ArgumentException("Object is not student");
            }
            return this.LastName.CompareTo(stud.LastName);
        }

        public bool WeekOverdue 
        {
            get { return _aStudent.WeekOverdue; }
            set { _aStudent.WeekOverdue = value; }
        }
        public bool HasBooks
        {
            get { return _aStudent.HasBooks; }
            set { _aStudent.HasBooks = value; }
        }
        public string FullName
        {
            get
            {
                return (_aStudent.FullName);
            }

        }

        public string LastName
        {
            get
            {
                return (_aStudent.LastName);
            }

        }


        public string FirstName
        {
            get
            {
                return (_aStudent.FirstName);
            }

        }

        public bool ShowingInfo
        {
            get
            {
                return _aStudent.ShowingInfo; //for now return true
            }
            set
            {
                _aStudent.ShowingInfo = value; ;
                NotifyPropertyChanged("ShowingInfo");
  
            }
        }

        public string CurrentBagID
        {
            get { return _aStudent.CurrentBagID; }
            set { _aStudent.CurrentBagID = value; }
        }

        public string ID
        {
            get { return _aStudent.ID; }
            set { _aStudent.ID = value; }
        }
        public bool CheckIn()
        {
            bool bReturn = _aStudent.CheckIn();
            NotifyPropertyChanged("CurrentBagID");
            NotifyPropertyChanged("HasBooks");
            NotifyPropertyChanged("WeekOverdue");
            return bReturn;
          
        }

        public bool CheckOut(string bagID)
        {
            bool bReturn = _aStudent.CheckOut(bagID);
            NotifyPropertyChanged("CurrentBagID");
            NotifyPropertyChanged("HasBooks");
            return bReturn;
        }


        #region Commands

        public void HandleCheckInCommand(object obj)
        {
            if (this.HasBooks)
            {
             //   CheckInBookMethod(this, EventArgs.Empty);
      
            }

        }

        public bool CanExecuteCheckInCommand(object obj)
        {
            return this.HasBooks ? true : false;
        }
        #endregion Commands



    }
}
