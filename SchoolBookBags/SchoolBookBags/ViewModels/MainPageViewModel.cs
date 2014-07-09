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
using Converters.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using Converters.Tests;
using System.ComponentModel;
using Commanding;
using System.Threading;

namespace Converters.ViewModels
{
    public class HistoryCollectionHolder
    {
  
        public ObservableCollection<History> HistoryRepository { get; set; }

       
        public bool GetHistoryForSelectedStudent(AStudentViewModel oStudent, ref IEnumerable<string> ReturnedBookIDS)
        {
            var result = (from p in HistoryRepository
                          where (String.Compare(p.StudentID, oStudent.ID,
                            StringComparison.InvariantCultureIgnoreCase) == 0)
                          select p.ID).Distinct();

            ReturnedBookIDS = result;


            foreach (string id in result)
            {
                Debug.WriteLine("MainWindowViewModelReturned: " + id);
            }

            return true;
        }

    }
    public class MainWindowViewModel : NotifyAndDataError
    {
        private readonly static bool RunTests = false;
        public string School;
        public string Grade;
        public int NumberOfBooks;
        public string BookBagSet;

        private string _teacher;
        public string Teacher
        {
            get
         {
             return _teacher;

        }
             set
            {
                _teacher = value;
                NotifyPropertyChanged("HeaderText");
            }
        }
        public StudentViewModel studentData { get; private set; }
        public BookBagsViewModel bookBagData { get; private set; }
        public HistoryViewViewModel historyViewModel { get;  set; }
        private Timer BookOverWeekOldTimer;

        public string BookTabBMP
        {
            get
            {
                return App.availableBookBMP;
            }
           

        }
  
        public RelayCommand _AddNewStudentCommand { get; private set; }
        public RelayCommand _RemoveStudentCommand { get; private set; }
        public RelayCommand _SaveAllCommand { get; private set; }

        #region Commands
        public void SaveAll()
        {


            if (Save())
            {
 //               if (studentData != null)
      //              studentData.SaveData(false);

                if (bookBagData != null)
                    bookBagData.SaveData();

            }
        }
        public void HandleSaveAllCommand(object obj)
        {
            SaveAll();
        }

        public bool CanExecuteSaveAllCommand(object obj)
        {
            return IsDirty;
        }

        public void HandleAddNewStudentCommand(object obj)
        {

            ShowNewStudentUI();

        }

        public bool CanExecuteAddNewStudentCommand(object obj)
        {
            return true;
        }

        public void HandleRemoveStudentCommand(object obj)
        {

            AStudentViewModel selStudent = studentData.SelectedStudent;

            if (selStudent == null)
            {
                MessageBox.Show("No student selected to delete.");


            }
            if (MessageBox.Show("Are you sure you want to delete student: " + selStudent.FullName + "?", "Delete Student",
                    MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            studentData.RemoveStudent(selStudent);

            IsDirty = true;
        }

        public bool CanExecuteRemoveStudentCommand(object obj)
        {
            return true;
        }
        #endregion Commands


        public enum UIElementType
        {
            Main,
            NewStudent

        }
        private UIElementType _uiElementPriv = UIElementType.Main;
        public long UIElementShowing
        {
            get
            {
                return (long)_uiElementPriv;

            }
            private set
            {
                _uiElementPriv = (UIElementType)value;
                NotifyPropertyChanged("UIElementShowing");
  
            }
        }


        public void ShowNewStudentUI ()
        {
            UIElementShowing = (long)UIElementType.NewStudent; 
           
         }

        public void ShowMainUI()
        {
            UIElementShowing = (long)UIElementType.Main;
        }


        public bool SaveStudent(string firstName, string lastName, bool isEditing)
        {
            if (studentData.AddANewStudent(firstName, lastName))
            {
                ShowMainUI();
                IsDirty = true;
                return true;
            }
            return false;

        }
        public string HeaderText
        {
            get
            {
                if (Teacher != "")
                    return School + " Home Reading Program " + "\nTeacher : " + Teacher + "\tGrade : " + Grade;
                else return School + "\nHome Reading Program Initialization";
            }
        }

        public HistoryCollectionHolder historyHolder;
        private DatabaseReader Database;
        private bool _isDirty;
         private Visibility _historyUserControlVisibility = Visibility.Hidden;
        public Visibility HistoryUserControlVisibility
        {
            get
            {
                return _historyUserControlVisibility;
            }
            set
            {
                if (value != _historyUserControlVisibility)
                {
                    _historyUserControlVisibility = value;
                    NotifyPropertyChanged("HistoryUserControlVisibility");
                }
            }
        }
        public Visibility BookBagsUserControlVisibility
        {
            get
            {
                if (_historyUserControlVisibility == Visibility.Collapsed ||
                    _historyUserControlVisibility == Visibility.Hidden)
                    return Visibility.Visible;
                else return Visibility.Collapsed;
                
            }
          
        }       
        
   
        public bool IsRunningTests
        {
            get
            {
                return (RunTests == true);
            }
        }
        public bool Save() //save all the data
        {
            if (Database == null)
                throw new ArgumentNullException("Database.xml Instance");

          Database.SaveBasicInfo(this);
            Database.SaveHistory(this.historyHolder.HistoryRepository, false);

            if (studentData != null)
                studentData.SaveData(false);
       

            IsDirty = false;
            return true;

        }
        public MainWindowViewModel(DatabaseReader aDatabase)
        {
            if (aDatabase == null)
                throw new ArgumentNullException("Database.xml Instance");

            aDatabase.Init();
            aDatabase.InitBasicInfo( this );

            //setup some defaults
            School = "Dr. W. J. Creel School Soaring Eagle";

            Database = aDatabase;
            _isDirty = false;
            _AddNewStudentCommand = new RelayCommand(HandleAddNewStudentCommand, CanExecuteAddNewStudentCommand);
            _RemoveStudentCommand = new RelayCommand(HandleRemoveStudentCommand, CanExecuteRemoveStudentCommand);
            _SaveAllCommand = new RelayCommand(HandleSaveAllCommand, CanExecuteSaveAllCommand);

            studentData = new StudentViewModel(Database);
            bookBagData = new BookBagsViewModel(Database);
            studentData.FetchData();
            bookBagData.FetchData();

            bookBagData.ValidateData(studentData);
            SelectFirstStudent();
            FetchHistory();


            historyViewModel = new HistoryViewViewModel(studentData.SelectedStudent, historyHolder, studentData.Students, Teacher);


            BookTimeTestTick(null); //call once on startup
            BookOverWeekOldTimer = new Timer(BookTimeTestTick, null, 1000, 1000);
 
    
        }

        public void SelectFirstStudent(){           studentData.SelectedStudent = (studentData.getStudentByIndex(0));       }
            
    

        void BookTimeTestTick(object state)
        {
            foreach (AStudentViewModel stud in studentData.Students)
            {
                if (stud.HasBooks == false)
                    stud.WeekOverdue = false;

                if (stud.WeekOverdue == true)
                    break;

                if (IsCheckedOutBagAWeekOld(stud, historyHolder))
                {
                    stud.WeekOverdue = true;
                }
            }
        }

        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }
            set
            {
                _isDirty = value;
                NotifyPropertyChanged("IsDirty");
             
            }
        }


        public bool FetchHistory()
        {
            if (historyHolder == null)
                historyHolder = new HistoryCollectionHolder();

            if (IsRunningTests)
            {
                ObservableCollection<History> hr = new ObservableCollection<History>();

                for (int i = 0; i < 200; i++)
                {
                    History TempHistory = new History();
                    UnitTestUtility.PopulateWithTestData(TempHistory);
                    hr.Add(TempHistory);
                }
                historyHolder.HistoryRepository = hr;
            }
            else
            {
                ObservableCollection<History> hr = new ObservableCollection<History>();
                Database.mbImportHistory(hr);
                historyHolder.HistoryRepository = hr;
            }
            return true;

        }

        public bool AddHistoryLine(string oID, string oStudentID, BookEvent.BookEventType eType, bool oReturnedSheet, string oMemo)
        {
            historyHolder.HistoryRepository.Add(new History
            {
                ID = oID,
                StudentID = oStudentID,
                                                BookEventData = new BookEvent 
                                                { 
                                                    TheBookEventType = eType, 
                                                    ReturnedSheet = eType == BookEvent.BookEventType.BookEventCheckIn ? oReturnedSheet : false, //no matter what, no sheet for checkout
                                                    Memo = oMemo, Date = DateTime.Now }
             });
             IsDirty = true;
  
             NotifyPropertyChanged("HistoryRepository");
            return true;

        }
        static public bool IsCheckedOutBagAWeekOld(AStudentViewModel oStudent, HistoryCollectionHolder historyHolderIn)
        {
            if (oStudent.HasBooks)
            {
                 var weekAgo = DateTime.Now.AddDays(-7);


               var result = (from p in historyHolderIn.HistoryRepository
                              where (String.Compare(p.StudentID, oStudent.ID,
                                StringComparison.InvariantCultureIgnoreCase) == 0 &&
                                p.ID == oStudent.CurrentBagID && p.BookEventData.TheBookEventType == BookEvent.BookEventType.BookEventCheckOut)
                                && (p.BookEventData.Date < weekAgo)
                              select p);

               if (result != null && result.Count<History>() > 0)
               {
                   return true;
               }
                return false;
            }
            return false;
        }
        public bool HasCheckedOutBagAlready(AStudentViewModel oStudent, OneBookBagViewModel oBag)
        {

            var result = (from p in historyHolder.HistoryRepository
                          where (String.Compare(p.StudentID, oStudent.ID,
                            StringComparison.InvariantCultureIgnoreCase) == 0 &&
                            p.ID == oBag.ID && p.BookEventData.TheBookEventType == BookEvent.BookEventType.BookEventCheckOut)
                          select p);

            if (result == null || result.Count<History>() <= 0) //never checked out
                return false;
            return true;
        }
   
        public bool GetHistoryForSelectedStudent(AStudentViewModel oStudent, ref IEnumerable<string> ReturnedBookIDS)
        {
           return historyHolder.GetHistoryForSelectedStudent(oStudent, ref ReturnedBookIDS);
        }

        public bool ClearHistory(bool bSaveToDatabase)
        {

           historyHolder.HistoryRepository.Clear();
            if (bSaveToDatabase)
                Database.SaveHistory(historyHolder.HistoryRepository, true);
            historyViewModel.UpdateHistory(historyHolder);

            bookBagData.ClearAvailableBookBags();
            studentData.ClearCheckedOutBooks();

            if (bSaveToDatabase)
            {
                if (studentData != null)
                    studentData.SaveData(true);

                if (bookBagData != null)
                    bookBagData.SaveData();

                IsDirty = false;
                
            }
 
            return true;
        }


        public bool ClearAll(bool bSaveToDatabase)
        {

            historyHolder.HistoryRepository.Clear();
            historyViewModel.UpdateHistory(historyHolder);
            bookBagData.ClearAvailableBookBags();
            studentData.ClearCheckedOutBooks();

            studentData.Students.Clear();
            Teacher = "";

            if (bSaveToDatabase)
            {

                if (Save())
                {
                    if (studentData != null)
                        studentData.SaveData(true);

                    if (bookBagData != null)
                        bookBagData.SaveData();

                }
            }
  

            return true;
        }

        public bool CheckOutBook(CheckOutData coData)
        {
            if (coData != null)
            {
               
                AStudentViewModel selStudent = coData.student;

                if (selStudent == null)
                    selStudent = studentData.SelectedStudent;

                OneBookBagViewModel selectedBag = coData.bag;

                if (selStudent == null || selectedBag == null)
                {
                    MessageBox.Show("Invalid student or bag");
                    return false;
                }
                    
                string errMsg;

                //make sure we haven't checked out already
                
                if (HasCheckedOutBagAlready(selStudent, selectedBag))
                {
                        MessageBox.Show(selStudent.FullName + " has already had book bag # " + selectedBag.ID);
                        return false;
                    }


                    if (bookBagData.CheckOut(selectedBag.ID, selStudent, out errMsg))
                    {
                        //add the line in history
                        AddHistoryLine(selectedBag.ID, selStudent.ID, BookEvent.BookEventType.BookEventCheckOut, true, "");

                        historyViewModel.UpdateHistory(historyHolder);
                    }
                    else
                    {
                        if (errMsg != "")
                            MessageBox.Show(errMsg);

                    }

            }
            return true;
        }

        public bool CheckInBook(AStudentViewModel selStudent)
        {
            string selectedBook = selStudent.CurrentBagID;
            if (bookBagData != null)
            {
              
                string errMsg;

                //show a dialog with the sharing sheet checkbox and an optional memo location here.  Can cancel as well
                CheckInConfirmationViewModel ciConfVM = new CheckInConfirmationViewModel(selStudent);
                CheckInConfirmationDialog ciConfDialog = new CheckInConfirmationDialog(ciConfVM);
                if (ciConfDialog.ShowDialog() == true)
                {
                    if (bookBagData.CheckIn(selStudent, out errMsg))
                    {
                        //add the line in history
                        AddHistoryLine(selectedBook, selStudent.ID, BookEvent.BookEventType.BookEventCheckIn,
                            ciConfVM.HasSharingSheet,
                            IsRunningTests == true ?
                            UnitTestUtility.GetReasonablyUniqueString("check in memo1 for " + selStudent.FullName) + " " + selStudent.FullName
                            : ciConfVM.Memo);
                        historyViewModel.UpdateHistory(historyHolder);
                        return true;
   
                    }
                    else
                    {
                        if (errMsg != "")
                            MessageBox.Show(errMsg);
                        return false;
                    }
                }
            }
            return false;
        }

        public bool SelStudentChanged()
        {
            IEnumerable<string> BooksInHistory = null;

            AStudentViewModel selStudent = studentData.SelectedStudent;

            if (selStudent == null)
            {
                return false;
            }

            GetHistoryForSelectedStudent(selStudent, ref BooksInHistory);
            bookBagData.SetImageForBagsForSelectedStudent(selStudent, BooksInHistory);         
            historyViewModel.SelectedStudent = selStudent;
            historyViewModel.UpdateSelectedStatus();
 
            return true;
        }
    }
}
