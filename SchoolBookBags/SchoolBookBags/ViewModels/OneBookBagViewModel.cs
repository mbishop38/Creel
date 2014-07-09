using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converters.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Converters.ViewModels
{
    public class OneBookBagViewModel : NotifyAndDataError
    {
        private BookBag _aBookBag;
        public ObservableCollection<string> testName
        {
            get {
                string s = "myname";
                string t = "yourname";
                ObservableCollection<string> tt = new ObservableCollection<string>();
                tt.Add(s);
                tt.Add(t);
                return tt;
            }
        }

        public ObservableCollection<AStudentViewModel> availableStudents { get; set; }

        public AStudentViewModel FindStudentFromFullName(string fullName)
        {
            foreach (AStudentViewModel stud in availableStudents)
            {
                if (string.Compare(stud.FullName, fullName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    return stud;

            }
            return null;

        }
        // Constructor
        public OneBookBagViewModel(BookBag bag)
        {
            _aBookBag = bag;
        }

        // Views
        public BookBag BookBag
        {
            get { return this._aBookBag; }
        }

        public bool IsAvailable
        {
            get
            {
                return _aBookBag.IsAvailable;
            }
          
        }
        public string CheckedOutStudentID
        {
            get
            {
                return _aBookBag.CheckedOutStudentID;
            }
            set
            {
                _aBookBag.CheckedOutStudentID = value;
            }
        }
        public string ID
        {
            get
            {
                return _aBookBag.ID;
            }
        }

        public void CheckIn()
        {
           _aBookBag.CheckIn();
        }
        public string ImageStatus
        {
            get
            {
                return _aBookBag.ImageStatus; //for now return true
            }
            set
            {
                _aBookBag.ImageStatus = value;
                NotifyPropertyChanged("ImageStatus");
            }
        }
    }
}
