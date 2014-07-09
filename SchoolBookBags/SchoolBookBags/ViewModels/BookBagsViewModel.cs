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
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using Converters.Models;


namespace Converters.ViewModels
{
    public class BookBagsViewModel : NotifyAndDataError, IModelViewInterface
	{
        private bool bShowAvailableOnly = true;
        public bool ClearAvailableBookBags()
        {
            foreach (OneBookBagViewModel b in BookBags)
            {
                b.CheckedOutStudentID = "";
            
            }
            return true;
        }
  
        private OneBookBagViewModel _selectedBookBag;
        public OneBookBagViewModel SelectedBookBag
        {
            get { return _selectedBookBag; }
            set {
                _selectedBookBag = value;
                NotifyPropertyChanged("SelectedBookBag");
 
            }
        }
        
		public ObservableCollection<OneBookBagViewModel> BookBags { get; set; }
        public ObservableCollection<OneBookBagViewModel> AvailableBookBags 
        {
            get
            {
                if (bShowAvailableOnly)
                {
                    var result = (from p in BookBags
                                  where (p.IsAvailable == true)
                                  select p);
                    return new ObservableCollection<OneBookBagViewModel>(result.Cast<OneBookBagViewModel>());
                }
                else
                    return BookBags;
            }
        }
        public ObservableCollection<OneBookBagViewModel> CheckedOutBookBags
        {
            get
            {
                if (bShowAvailableOnly)
                {
                    var result = (from p in BookBags
                                  where (p.IsAvailable == false)
                                  select p);
                    return new ObservableCollection<OneBookBagViewModel>(result.Cast<OneBookBagViewModel>());
                }
                else
                    return BookBags;
            }
        }

        public void ValidateData(StudentViewModel studentsVM)
        {
            foreach (OneBookBagViewModel b in CheckedOutBookBags)
            {
                if (b.CheckedOutStudentID != "")
                {
                    AStudentViewModel aStudent = studentsVM.getStudentByID(Convert.ToInt32(b.CheckedOutStudentID));
                    if (aStudent == null)
                    {
                        MessageBox.Show("Error! Book bag " + b.ID + " looks corrupt for student " + aStudent.FullName);
                        return;
                    }
                    else
                        aStudent.CheckOut( b.ID );
                }
            }

        }
	    private DatabaseReader Database;
 

        public BookBagsViewModel(DatabaseReader aDatabase)
		{
			Database = aDatabase;
			if (Database == null)
				throw new ArgumentNullException("Database.xml Instance");
		}
        public bool SaveData()
        {

            // this is where your service call would be
            if (Database == null)
                throw new ArgumentNullException("Database.xml is NULL");


            Database.SaveBagData(BookBags);
            return true;
        }


        public void FetchData()
        {
            ObservableCollection<OneBookBagViewModel> bb = new ObservableCollection<OneBookBagViewModel>();

           Database.mbImportBags(bb);
           BookBags = bb;
          
        }

        public bool CheckIn(AStudentViewModel oStudent, out string errMsg)
        {
            errMsg = null;
            if (oStudent == null)
                return false;

            if (oStudent.HasBooks == false)
            {
                errMsg = "Student " + oStudent.FullName + " doesn't have a book checked out.";
                return false;
            }

            bool bFound = false;
            foreach (OneBookBagViewModel b in BookBags)
            {
                if (String.Compare(b.ID, oStudent.CurrentBagID, //checked out to me
                                StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        b.CheckIn();                       
                        bFound = true;
                    }
            }
            if (bFound)
            {
                NotifyPropertyChanged("AvailableBookBags");
                NotifyPropertyChanged("BookBags");
                return oStudent.CheckIn();
              

            }
            else
            {
                errMsg = "Bag information is corrupt for bag id " + oStudent.CurrentBagID;
                return false;
            }
 
        }

        public bool  CheckOut(string oID, AStudentViewModel oStudent, out string errMsg)
        {
            errMsg = null;
            if (oStudent == null)
                return false;

            if (oStudent.HasBooks == true)
            {
                errMsg = "Student " + oStudent.FullName + " already has book bag # " + oStudent.CurrentBagID + " checked out.";
                return false;
            }

            bool bFound = false;
            foreach (OneBookBagViewModel b in BookBags)
            {
                if (String.Compare(b.ID, oID, //checked out to me
                                StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    b.CheckedOutStudentID = oStudent.ID;
                    bFound = true;
                }
            }
            if (bFound)
            {
                NotifyPropertyChanged("AvailableBookBags");
                NotifyPropertyChanged("BookBags");
                return oStudent.CheckOut(oID);
    
            }
            else 
            {
               errMsg = "Bag information is corrupt for bag id " + oID;
               return false;
            }
 
          
        }

        public bool SetImageForBagsForSelectedStudent(AStudentViewModel oStudent, IEnumerable<string> BooksInHistory)
        {
            if (BooksInHistory == null)
                return false;

            foreach (OneBookBagViewModel b in BookBags)
            {
                if (b.CheckedOutStudentID == "" || (Convert.ToInt32(b.CheckedOutStudentID) <=0)) //not checked out

                {
                    var result = (from p in BooksInHistory
                                  where (String.Compare(p, b.ID,
                            StringComparison.InvariantCultureIgnoreCase) == 0)
                      select p).Distinct();

                    if (result == null || result.Count<string>() <= 0) //available
                    {
                        b.ImageStatus = App.availableBookBMP;
                    }
                    else
                    {
                        b.ImageStatus = App.usedBookBMP;
                    }
                }
                else {

                    if (String.Compare(b.ID, oStudent.CurrentBagID, //checked out to me
                        //could also do compare(b.StudentID, oStudent.id)
                                StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        b.ImageStatus = App.currentBookBMP;
                    }
                    else
                    {
                        //checked out to someone else
                        b.ImageStatus = App.unavailableBookBMP;
                    }
                }
            }

            return true;
        }


	}
}
