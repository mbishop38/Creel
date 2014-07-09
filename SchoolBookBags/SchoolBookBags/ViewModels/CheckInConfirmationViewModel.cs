using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Converters.Models;
using Converters.ViewModels;

namespace Converters
{
    public class CheckInConfirmationViewModel : NotifyAndDataError
    {
        #region Constructor
        public CheckInConfirmationViewModel(AStudentViewModel selStudent)
        {
            _topLabelText = "You are checking in the current book bag for student : " + selStudent.FullName + ".  Please input the sharing sheet as well as any extra information below.";
            _title = "Checking in Book Bag for Student : " + selStudent.FullName;
            _hasSharingSheet = false; //false by default.
        }
        #endregion

        #region membervars
        private string _topLabelText;
        private string _title;
        private bool _hasSharingSheet;
        public bool HasSharingSheet
        {
            get
            {
                return _hasSharingSheet;
            }
            set
            {
                _hasSharingSheet = value;
                NotifyPropertyChanged("HasSharingSheet");
            }
        }
        public string Memo { get; set; }
        public string TopLabelText
        {
            get
            {
                return _topLabelText;
            }
        }
         public string Title
        {
            get
            {
                return _title;
            }
        }   
        #endregion

    }
}
