using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Commanding;
using System.Diagnostics;
using System.Windows;
using Converters.Models;



namespace Converters.ViewModels
{

    class AddANewStudentViewModel : NotifyAndDataError
    {
        public string FirstName { get; set; }
        public string LastName {get; set;}

        private StudentViewModel studentVM;
        private MainWindowViewModel mainVM;
        public bool IsEditing { get; private set; }
       
        public AddANewStudentViewModel(MainWindowViewModel inMainVM)
        {

            if (inMainVM == null)
                throw new Exception("Invalid new student command");

            studentVM = inMainVM.studentData;
            mainVM = inMainVM;
            IsEditing = false;
            _CancelButtonCmd = new RelayCommand(HandleCancelBtnCommand, CanExecuteCancelBtnCommand);
            _SaveStudentButtonCmd = new RelayCommand(HandleSaveStudentBtnCommand, CanExecuteSaveStudentCommand);
        }

  


        public RelayCommand _CancelButtonCmd { get; private set; }
        public RelayCommand _SaveStudentButtonCmd { get; private set; }

        #region Commands





        public void HandleCancelBtnCommand(object obj)
        {
            FirstName = LastName = "";
            NotifyPropertyChanged("FirstName");
            NotifyPropertyChanged("LastName");
            mainVM.ShowMainUI();
        }

        public bool CanExecuteCancelBtnCommand(object obj)
        {
            return true;
        }
        public void HandleSaveStudentBtnCommand(object obj)
        {
            if (mainVM.SaveStudent(FirstName, LastName, IsEditing))
            {

                FirstName = LastName = "";
                NotifyPropertyChanged("FirstName");
                NotifyPropertyChanged("LastName");
            }
        
        }

        public bool CanExecuteSaveStudentCommand(object obj)
        {
            return FirstName != null && FirstName.Length > 0 && LastName != null && LastName.Length > 0;
        }
        #endregion Commands
    }
}
