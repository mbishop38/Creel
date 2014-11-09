using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Converters.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Documents;
using System.Diagnostics;
using System.Windows.Media;
using SimpleLogNS;

namespace Converters.ViewModels
{
    public class StudentsAndHistory
    {
        public string studentName { get;   set; }
        public string studentID { get;  set; }
        public bool IsFlagged { get; set; }
        public long NumberOfBags {get; set;}

        public string formattedNumberOfBooks
        {
            get
            {
                return "Number of bags: " + NumberOfBags.ToString();
            }
        }
        public string formattedStudentName
        {
            get
            {
                return "Student name: " + studentName;
            }
        }        
    
        public ObservableCollection<FormattedHistory> historyList { get; set; }
        public ObservableCollection<FormattedHistory> historyListWithFilter { get; set; }

        public bool UpdateFilter(HistoryViewViewModel.TypeOfFilter type)
        {

            historyListWithFilter.Clear();
        
             switch (type)
            {
                case HistoryViewViewModel.TypeOfFilter.FilterNone:
                default:
                    foreach (FormattedHistory histLine in historyList)
                    {
                            historyListWithFilter.Add(histLine);

                    }
         
                break;
                case HistoryViewViewModel.TypeOfFilter.FilterDelinquentBooks:

                if (IsFlagged == true)
                {
                    //only add the last.
                    FormattedHistory histLine = historyList.Last();
                    historyListWithFilter.Add(histLine);

                }

                    break;
                case HistoryViewViewModel.TypeOfFilter.FilterNoSharingSheet:
                    foreach (FormattedHistory histLine in historyList)
                    {
                        if (histLine.action == BookEvent.BookEventType.BookEventCheckIn && histLine.sharingSheet == false)                
                            historyListWithFilter.Add(histLine);

                    }              
                    break;
                case HistoryViewViewModel.TypeOfFilter.FilterMemo:
                     foreach (FormattedHistory histLine in historyList)
                    {
                        if (histLine.Memo != null && histLine.Memo.Length > 0)
                            historyListWithFilter.Add(histLine);

                    }              
                     break;
             
   
            }
               return true;

        }

    }
    public class FormattedHistory
    {

        public string student;
        public BookEvent.BookEventType action;
        public string bookID;
        public bool sharingSheet;
        public DateTime Date;
        public string Memo;

        public FormattedHistory(string studIn, string bookIDIn, BookEvent.BookEventType actionIn, DateTime DateIn, bool sharingSheetIn, string memoIn)
        {
            if ( studIn == "" || bookIDIn == "" )
                throw new ArgumentNullException();


            student = studIn;
            action = actionIn;
            bookID = bookIDIn;
            Date = DateIn;
            sharingSheet = sharingSheetIn;
            Memo = memoIn;
        }

        public string formattedHistoryLine
        {

            get
            {
                if (Memo != "")
                    return DateOccured + ": " + action + " " + bookID + " bag" + "\n\tSharingSheet: " + SharingSheet + "\n\tNotes: " + MemoStr;
                else
                    return DateOccured + ": " + action + " " + bookID + " bag" + "\n\tSharingSheet: " + SharingSheet;
   
            }
        }

        public string formattedBag
        {
            get { return "Bag #" + bookID; }
          //  get { return "Bag #" + bookID + "\n" + formattedHistoryLine2   + "\n" +  MemoStr;  }
        }

        public string formattedHistoryLine1
        {

            get
            {
                return DateOccured + ": " + History.FormattedBookEventType(action) + " bag # " + bookID;

            }
        }

        public string formattedHistoryLine2
        {

            get
            {
                if (action == BookEvent.BookEventType.BookEventCheckIn)
                    return SharingSheet;
                else return "";

            }
        }
        public string formattedHistoryLine3
        {

            get
            {
                     return Memo;
               

            }
        }
        public string StudentName
        {
            get { return student; }

        }
        public string BookID
        {
            get { return bookID; }

        }
        public BookEvent.BookEventType Action
        {
            get { return action; }

        }
        public bool SharingSheetBool
        {
            get
            {
                if (action == BookEvent.BookEventType.BookEventCheckIn)
                    return (sharingSheet);
                else return true;
            }

        }
        public string SharingSheet
        {
            get {
                if (action == BookEvent.BookEventType.BookEventCheckIn)
                    return sharingSheet == true ? "yes" : "no";
                else return "";
            }

        }
        public string DateOccured
        {

            get {
                string shortDate = Date.ToShortDateString();
                return shortDate;
            
            }

        }
        public string MemoStr
        {
            get { return Memo; }

        }

        public string EventType
        {
            get { return History.FormattedBookEventType(action);  }
        }
    }

    public class HistoryViewViewModel : NotifyAndDataError
    {
        public enum TypeOfFilter
        {
            FilterNone,
            FilterDelinquentBooks,
            FilterNoSharingSheet,
            FilterMemo

        };
        public  HistoryCollectionHolder _historyHolder;
        private AStudentViewModel _selectedStudent;
        private string teacher { get; set; }
        public ObservableCollection<AStudentViewModel> Students { get; private set; }

        public ObservableCollection<StudentsAndHistory> MappedHistoryToStudent { get; private set; }
        private ObservableCollection<StudentsAndHistory> MappedHistoryToStudentAll { get; set; }

        private bool _showSelectedStudentOnly;
        public bool ShowSelectedStudentOnly 
        {
            get
            {
                return _showSelectedStudentOnly; //for now return true
            }
            set
            {
                _showSelectedStudentOnly = value;
                UpdateSelectedStatus();
                NotifyPropertyChanged("ShowSelectedStudentOnly");

            }
        }

        private TypeOfFilter _filterType;
        public long FilterType
        {
            get
            {
                return (long) _filterType; //for now return true
            }
            set
            {
                _filterType = (TypeOfFilter) value;
                UpdateSelectedStatus();
                NotifyPropertyChanged("FilterType");

            }
        }
        
        public ObservableCollection<History> HistoryRepository 
        {
            get {

                if (ShowSelectedStudentOnly == true)
                {
                    return _historyHolder.HistoryRepository; 
                 

                }
                else
                    return _historyHolder.HistoryRepository; 
            
            }
         
        }

        public string SelectedStudentName
        {
            get { return _selectedStudent.FullName; }
        }

        public Visibility ShowList
        {
            get
            {
                return MappedHistoryToStudent.Count > 0 ? Visibility.Visible : Visibility.Hidden;
            }
       
        }

        public Visibility ShowMessage
        {
            get
            {
                return MappedHistoryToStudent.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
            }

        }
        public AStudentViewModel SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value; }
        }


        public bool UpdateSelectedStatus()
        {
            SimpleLog.Info("History UpdateSelectedStatus.");

            if (ShowSelectedStudentOnly)
            {
                MappedHistoryToStudent.Clear();
                foreach (StudentsAndHistory StudAndHist in MappedHistoryToStudentAll)
                {
                    if (string.Compare(StudAndHist.studentID, _selectedStudent.ID, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        StudAndHist.UpdateFilter(_filterType);
                        MappedHistoryToStudent.Add(StudAndHist);

                    }
                }

            }
            else
            {
                MappedHistoryToStudent.Clear();
                foreach (StudentsAndHistory StudAndHist in MappedHistoryToStudentAll)
                {
                    StudAndHist.UpdateFilter(_filterType);
                    if (StudAndHist.historyListWithFilter.Count > 0)
                        MappedHistoryToStudent.Add(StudAndHist);

                   }
            }

          
            NotifyPropertyChanged("ShowMessage");
            NotifyPropertyChanged("ShowList");
            NotifyPropertyChanged("historyListWithFilter");
            return true;
        }

        public FlowDocument GetDocumentPrintOutput()
        {
            SimpleLog.Info("History GetDocumentPrintOutput.");

            // Create a FlowDocument dynamically. 
            FlowDocument doc = new FlowDocument();
            doc.Name = "StudentReport";

            Paragraph para = new Paragraph();
            para.Margin = new Thickness(10);
            string reportHeader = "Book Bag Report Output " + DateTime.Now + "\nTeacher: " + teacher ;
           
            Bold b = new Bold();
            b.Inlines.Add(reportHeader);
            para.Inlines.Add(b);
            doc.Blocks.Add(para);
  // lets puts formatting in tonight     http://msdn.microsoft.com/en-us/magazine/cc163371.aspx

            int nNum = 1;

            foreach (StudentsAndHistory StudAndHist in MappedHistoryToStudent)
            {
             
                Paragraph para2 = new Paragraph();
                para2.Margin = new Thickness(10);

                para2.Inlines.Add("Student " + nNum.ToString() + ": " + StudAndHist.studentName);
                para2.Inlines.Add("\n" + StudAndHist.formattedNumberOfBooks );
                doc.Blocks.Add(para2);
              
                foreach (FormattedHistory histLine in StudAndHist.historyListWithFilter)
                {
                    Paragraph para3 = new Paragraph();
                    para3.Margin = new Thickness(30,5,30,0);

                    para3.Inlines.Add (histLine.formattedHistoryLine1);
                    if (histLine.action == BookEvent.BookEventType.BookEventCheckIn)
                        para3.Inlines.Add("\nSharing Sheet: " + histLine.formattedHistoryLine2);
                     if (histLine.MemoStr != "")
                     para3.Inlines.Add("\nMemo: " + histLine.formattedHistoryLine3);
                    doc.Blocks.Add(para3);
                }
                nNum++;

            }

          
            return doc;


        }

        public bool UpdateHistory(HistoryCollectionHolder historyHolder)
        {
            SimpleLog.Info("Updating history.");
            if (MappedHistoryToStudent == null)
                MappedHistoryToStudent = new ObservableCollection<StudentsAndHistory>();
            else MappedHistoryToStudent.Clear();
            if (MappedHistoryToStudentAll == null)
                MappedHistoryToStudentAll = new ObservableCollection<StudentsAndHistory>();
            else MappedHistoryToStudentAll.Clear();

            
            long numberOfBagsTotal =0;
            //create a map from the student id to student name
            foreach (AStudentViewModel stud in Students)
            {
                numberOfBagsTotal = 0;

                ObservableCollection<FormattedHistory> tempHistory = null;
                StudentsAndHistory studHist = new StudentsAndHistory();
                studHist.studentName = stud.FullName;
                studHist.IsFlagged = stud.WeekOverdue;
                studHist.studentID = stud.ID;
                tempHistory = new ObservableCollection<FormattedHistory>();
                studHist.historyList = tempHistory;
                studHist.historyListWithFilter = new ObservableCollection<FormattedHistory>();
             

                //create the history list that is displayed in the report
                foreach (History hist in historyHolder.HistoryRepository)
                {
                    if (hist.StudentID == stud.ID)
                    {
                        FormattedHistory history = new FormattedHistory(studHist.studentName, hist.ID, hist.BookEventData.TheBookEventType, hist.BookEventData.Date, hist.BookEventData.ReturnedSheet, hist.BookEventData.Memo);
                        tempHistory.Add(history);

                        if (hist.BookEventData.TheBookEventType == BookEvent.BookEventType.BookEventCheckOut)
                        {
                            numberOfBagsTotal++;
                        }

                    }
                }

                if (studHist.historyList.Count > 0)
                {//don't even display if no history.
                   studHist.NumberOfBags = numberOfBagsTotal;
                    MappedHistoryToStudent.Add(studHist);
                    MappedHistoryToStudentAll.Add(studHist);
                }

                
                studHist.UpdateFilter(_filterType);
            } 
            return true;
        }

        // Constructor
        public HistoryViewViewModel(AStudentViewModel selectedStudentIn, HistoryCollectionHolder historyHolder, ObservableCollection<AStudentViewModel> StudentsIn, string teacherIn)
        {
            _selectedStudent = selectedStudentIn;
            _historyHolder = historyHolder;
            Students = StudentsIn;
            _showSelectedStudentOnly = false;
            _filterType = TypeOfFilter.FilterNone;
            teacher = teacherIn;

            UpdateHistory(historyHolder);
 
                      
        }
    }
}
