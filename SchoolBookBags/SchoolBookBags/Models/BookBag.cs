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
using System.Collections.Generic;
using Converters.ViewModels;

namespace Converters.Models
{
    public class BookBag : INotifyPropertyChanged
    {
        [Required]
        public string ID { get; set; }
        public string CheckedOutStudentID { get; set; }
        public bool IsMissingItems { get; set; } //are books missing from this?
        public bool IsAvailable 
        {   
            get
            {
                return CheckedOutStudentID == "" ;
            }
        }

      
        private string _imageStatus;

        public string ImageStatus
        {
            get
            {
                return _imageStatus; //for now return true
            }
            set
            {
                _imageStatus = value;
                RaisePropertyChanged("ImageStatus");
            }
        }
        public void CheckIn()
        {
            CheckedOutStudentID = "";
            ImageStatus = App.usedBookBMP;
            RaisePropertyChanged("IsAvailable");
        }
        public bool Validate(List<string> checkedOutStudentsList, ref string errorOut)
        {
            if (ID == "")
                errorOut = "invalid book bag id";

            if (CheckedOutStudentID != "" && checkedOutStudentsList.Contains(CheckedOutStudentID))
            {
                errorOut = "Error in the book bag data! Bag: " + ID + " is checked out twice. Please validate the database.";
            }

            if (errorOut != "")
                return false;
            else return true;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
       

    }

    public class CheckOutData
    {
        public OneBookBagViewModel bag { get; set; }
        public AStudentViewModel student { get; set; }

    }
}
