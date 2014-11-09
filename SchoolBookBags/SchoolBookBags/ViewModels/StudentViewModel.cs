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
using System.Diagnostics;
using Converters.Models;
using Converters.Tests;
using Commanding;
using System.ComponentModel;
using System.Windows.Data;
using System.Linq;
using SimpleLogNS;

namespace Converters.ViewModels
{
    public class StudentViewModel : NotifyAndDataError, IModelViewInterface  
    {
        public ObservableCollection<AStudentViewModel> Students { get; set; }
        private IDatabaseReader Database;
        public event EventHandler StudentSelectionChangedMethod;
        public RelayCommand _BookButtonPressed { get; private set; }
        public event EventHandler CheckInBookMethod;

       //   private ICommand newStudentCommand;
        #region Constructor
        public StudentViewModel(IDatabaseReader aDatabase)
        {
            Database = aDatabase;
            if (Database == null)
                throw new ArgumentNullException("Database.xml is NULL");

            _BookButtonPressed = new RelayCommand(BookButtonPressedFunc);
        }
        #endregion Constructor

        #region MemberProps


        void BookButtonPressedFunc(object obj)
        {
            SelectedStudent = obj as AStudentViewModel;

            if (SelectedStudent != null && SelectedStudent.HasBooks)
            {

                if (SelectedStudent != null && CheckInBookMethod != null)
                    CheckInBookMethod(SelectedStudent, EventArgs.Empty);
            }
        }
        private AStudentViewModel _selectedStudent;
        public AStudentViewModel SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                if (_selectedStudent == value)
                    return;
                _selectedStudent = value;

                NotifyPropertyChanged("SelectedStudent");

                if (StudentSelectionChangedMethod != null)
                    StudentSelectionChangedMethod(this, EventArgs.Empty);                 
                // Publish event when the selected item changes
            }
        }
        #endregion MemberProps


        #region Methods
        public bool RemoveStudent(AStudentViewModel studIn)
        {      
            Students.Remove(studIn);
            return true;
        }

        public bool AddANewStudent(string firstName, string lastName)
        {
            string newID = generateNewStudentID();
            Student aNewStudent = new Student(newID, firstName, lastName);

            AStudentViewModel aNewStudentVM = new AStudentViewModel(aNewStudent);
            Students.Add(aNewStudentVM);

            NotifyPropertyChanged("Students");

            return true;
        }

        public bool SaveData(bool bCleared)
        {

            // this is where your service call would be
            if (Database == null)
                throw new ArgumentNullException("Database.xml is NULL");

            Database.SaveStudents(Students, bCleared);
            return true;
        }


        public void FetchData()
        {
            ObservableCollection<AStudentViewModel> s = new ObservableCollection<AStudentViewModel>();

            // this is where your service call would be
            if (Database == null)
                throw new ArgumentNullException("Database.xml is NULL");

            Database.mbImportStudents(s);

            Students = s;

            iSort();

        }


        private bool iSort()
        {
            //SORTING - yea!
            ICollectionView _studentsView = CollectionViewSource.GetDefaultView(Students);

            //sort by last then first name
            _studentsView.SortDescriptions.Add(new SortDescription("LastName", ListSortDirection.Ascending));
            _studentsView.SortDescriptions.Add(new SortDescription("FirstName", ListSortDirection.Ascending));
            return true;
        }
        public bool CheckInBook()
        {

            AStudentViewModel oStudent = SelectedStudent;

            if (oStudent == null)
                throw new Exception("Student is not selected.");
            if (oStudent.HasBooks == false)
                throw new Exception("Student doesn't have books.");

            if (oStudent != null && oStudent.HasBooks)
                oStudent.CheckIn();

            return true;

        }

        #endregion Methods

        #region Accessors

        public bool IsBookCheckedOutToStudent(string BookID)
        {
            if (_selectedStudent.CurrentBagID == BookID)
                return true;
            else return false;

        }

   

        public AStudentViewModel getStudentByIndex(int nIndex)
        {
            int nCurIndex = 0;
            foreach (AStudentViewModel stud in Students)
            {
                if (nCurIndex == nIndex)
                    return stud;
                nCurIndex++;

            }
            return null;
        }

        public bool HasCheckedOutBooks()
        {
            
            foreach (AStudentViewModel stud in Students)
            {
                if (stud.HasBooks == true)
                    return true;
               
            }
            return false;

        }
        public bool ClearCheckedOutBooks()
        {

            foreach (AStudentViewModel stud in Students)
            {
                if (stud.HasBooks == true)
                {
                    stud.CurrentBagID = "";
                    stud.HasBooks = false;
                }
            }
            return true;

        }
        public AStudentViewModel getStudentByID(int nID)
        {

            foreach (AStudentViewModel stud in Students)
            {
                int nCurID = Convert.ToInt32(stud.ID);

                if (nCurID == nID)
                    return stud;
                  
            }
            return null;
        }
        private string generateNewStudentID()
        {
            int nID = 0;
            int nMaxID = 0;

            foreach (AStudentViewModel stud in Students)
            {
                int nCurID = Convert.ToInt32(stud.ID);

                if (nCurID > nMaxID)
                    nMaxID = nCurID;
            }

            nID = nMaxID + 1;
            return nID.ToString();

        }
        #endregion Accessors



    }
        public class StudentViewModelTest : IModelViewInterface
        {
            public ObservableCollection<Student> Students { get; set; }


            private string generateNewStudentID()
            {
                int nID = 1;
                int nMaxID = 1;

                foreach (Student stud in Students)
                {
                    int nCurID = Convert.ToInt32(stud.ID);

                    if (nCurID > nMaxID)
                        nMaxID = nCurID;
                }

                nID = nMaxID + 1;
                return nID.ToString();

            }


            public void FetchData()
            {
                ObservableCollection<Student> s = new ObservableCollection<Student>();

                for (int i = 0; i < 33; i++)
                {

                    Student aTempStudent = new Student(generateNewStudentID());
                    UnitTestUtility.PopulateWithTestData(aTempStudent);
                    s.Add(aTempStudent);
                }
                Students = s;
            }
        }


    
}