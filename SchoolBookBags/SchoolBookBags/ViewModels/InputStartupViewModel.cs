using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Converters.Models;
using System.Collections.ObjectModel;
using Commanding;
using System.Diagnostics;




namespace Converters.ViewModels
{
    class StudentHolder
    {
        public StudentHolder(InputStartupViewModel iParentVM, int iIndex)
        {
            Saved = false; _SaveStudentCommand = new RelayCommand(HandleSaveStudentCommand, CanExecuteSaveStudentCommand);
            parentVM = iParentVM;
            Index = iIndex;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Saved { get; set; }
        public int Index { get; set; }

        private InputStartupViewModel parentVM;
        public bool HasAllData
        {
            get
            {
                return (FirstName != null && FirstName != "" && LastName != null && LastName != "") ? true : false;
            }
          

        }


        public RelayCommand _SaveStudentCommand { get; private set; }


        public void HandleSaveStudentCommand(object obj)
        {
            if (HasAllData && parentVM != null)
                Saved = parentVM.AddNewStudent();


        }

        public bool CanExecuteSaveStudentCommand(object obj)
        {
            if (HasAllData == true && Saved == false)
                return true;
            return false;
        }



    }
    class InputStartupViewModel : NotifyAndDataError
    {
        public enum BBInputType
        {
            BBInputBasic = 0,
            bbInputStudents = 1,
            bbInputFinish = 2
        }

        MainWindowViewModel mainViewModel;
        public InputStartupViewModel(MainWindowViewModel ViewModel)
        {
            Saved = false;
            mainViewModel = ViewModel;
            InputType = BBInputType.BBInputBasic; ButtonText = "Next";
            HeaderText = "Enter teacher and class information below:";
            InputtedStudents = new ObservableCollection<StudentHolder>();
            _NextCommand = new RelayCommand(HandleNextCommand, CanExecuteNextCommand);
        }

        public static Window GetWindowRef(string WindowName)
        {
            Window retVal = null;    
            foreach (Window window in Application.Current.Windows)
            {
        
                // The window's Name is set in XAML. See comment below
        
                if (window.Name.Trim().ToLower() == WindowName.Trim().ToLower()) 
                {
                    retVal = window;            
                    break;       
                }    
            }
            return retVal;
        }

        public RelayCommand _NextCommand { get; private set; }


        public void HandleNextCommand(object obj)
        {
            if (InputType == BBInputType.BBInputBasic)
            {

               InputType = BBInputType.bbInputStudents;
                NotifyPropertyChanged("ShowStudents");
                NotifyPropertyChanged("ShowBasicInput");
                NotifyPropertyChanged("ShowFinish");
                ButtonText = "Next";
                HeaderText = "Enter each student first name and last name.  Check your data then add the student by pressing the button to commit:";
                NotifyPropertyChanged("ButtonText");
                 NotifyPropertyChanged("HeaderText");
                 AddNewStudent();
           }
            else if (InputType == BBInputType.bbInputStudents)
            {
                InputType = BBInputType.bbInputFinish;
                NotifyPropertyChanged("ShowStudents");
                NotifyPropertyChanged("ShowBasicInput");
                NotifyPropertyChanged("ShowFinish");
                ButtonText = "Finish";
                HeaderText = "Student Data Saved!";
                NotifyPropertyChanged("ButtonText");
                     NotifyPropertyChanged("HeaderText");
            }
            else
            {
                //check and save the data
                if (mainViewModel != null)
                {

                    mainViewModel.Teacher = TeacherName;
                    mainViewModel.Grade = Grade;

                    foreach (StudentHolder stud in InputtedStudents)
                    {
                        mainViewModel.studentData.AddANewStudent(stud.FirstName, stud.LastName);
                    }
                    Saved = true;


                    mainViewModel.SaveAll();

                    mainViewModel.SelectFirstStudent();
             

                    var window =GetWindowRef("InputStartupWindow");
                    window.Close();
                    
                }
            }
    
        }

        public bool CanExecuteNextCommand(object obj)
        {
            if (InputType == BBInputType.BBInputBasic)
            {
                Debug.WriteLine(NumStudents);

                if (Grade != null && Grade != "" &&
                    TeacherName != null && TeacherName != "" && NumStudents != "" &&
                    NumberOfStudentsConv > 0)
                    return true;
            }
            else if (InputType == BBInputType.bbInputStudents)
            {
                string testLine = "counts " + Convert.ToString(InputtedStudents.Count) + " " + Convert.ToString(NumberOfStudentsConv) + " " + Convert.ToString(InputtedStudents.Last().Saved);
                Debug.WriteLine(testLine);
                if (InputtedStudents.Count >= NumberOfStudentsConv && InputtedStudents.Last().Saved == true)
                    return true;

            }
            else
            {
                return true;
            }

            return false;

        }
        public bool Saved { get; private set; }

        public string Grade { get; set; }
        public string TeacherName { get; set; }
        private string _numStudents;
        public int NumberOfStudentsConv
        {
            get
            {
                int value = Convert.ToInt32(NumStudents); return value;
            }
        }
        public string NumStudents
        {
            get
            {
                return _numStudents;

            }
            set
            {
                _numStudents = value;
            
                 

            }
        }

        public string HeaderText { get; private set; }
        public string ButtonText { get; private set; }

        public BBInputType InputType { get; set; }
        public Visibility ShowStudents
        {
            get
            {
                return InputType == BBInputType.bbInputStudents ? Visibility.Visible : Visibility.Hidden;
            }

        }

        public Visibility ShowFinish
        {
            get
            {
                return InputType == BBInputType.bbInputFinish ? Visibility.Visible : Visibility.Hidden;
            }

        }
        public Visibility ShowBasicInput
        {
            get
            {
                return InputType == BBInputType.BBInputBasic ? Visibility.Visible : Visibility.Hidden;
            }

        }
        public ObservableCollection<StudentHolder> InputtedStudents
        {
            get;
            set;
        }

        public bool AddNewStudent()
        {
            if (InputtedStudents.Count >= NumberOfStudentsConv)
                return true;

            StudentHolder oneStudent = new StudentHolder(this, InputtedStudents.Count + 1);
            InputtedStudents.Add(oneStudent);
            NotifyPropertyChanged("InputtedStudents");

            return true;

        }
   

    }
}
