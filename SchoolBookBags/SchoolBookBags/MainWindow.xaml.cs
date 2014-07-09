#define bagInfo
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using Converters.ViewModels;
using Converters.Models;
using System.Diagnostics;
using Converters.Views;
using Converters.Tests;
using System.Collections.ObjectModel;



namespace Converters
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool bShowInputWindow = true;


        public MainWindowViewModel ViewModel { get; private set; }      
        DatabaseReader Database;



  
      //  private Popup _settingsPopup;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
            Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

      
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            StudentsDataView.StudentSelectionChangedMethod += new EventHandler(SelectedStudentChanged);
            bShowInputWindow = true;
            Debug.WriteLine("Write code to check for cleared data");


         }
        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ViewModel.IsDirty == true)
            {
                if (MessageBox.Show("You have unsaved changes in this session.  Would you like to save now?", "Save Changes",
                         MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    ViewModel.SaveAll();

            }
        }

        public bool InitViews()
        {

            if (ViewModel.IsRunningTests)
            {
                StudentViewModelTest studentData = new StudentViewModelTest();
                BookBagsViewModel bookBagData = new BookBagsViewModel(Database);
                studentData.FetchData();
               StudentsDataView.DataContext = studentData;
                bookBagData.FetchData();
                BagDataView.DataContext = bookBagData;
                ViewModel.FetchHistory();            
                DataContext = ViewModel;

                
            }
            else
            {
 
                StudentsDataView.DataContext = ViewModel;
                StudentsDataView.StudentsStackPanel.DataContext = ViewModel.studentData;
                ViewModel.studentData.CheckInBookMethod += new EventHandler(CheckInBook);
 
                BagDataView.DataContext = ViewModel.bookBagData;

                DataContext = ViewModel;
                HistoryView.DataContext = ViewModel.historyViewModel;
                AddANewStudentViewModel addStudentViewModel = new AddANewStudentViewModel(ViewModel);
                ANewStudentView.DataContext = addStudentViewModel;
               
            }

             return true;

        }

  
        protected void CheckInBook(object sender, EventArgs e)
        {
            Debug.WriteLine("CheckInBook called by ChildButton_Click");

            if (sender == null)
                throw new Exception("check in book failed");

            AStudentViewModel selStudent = sender as AStudentViewModel;
            ViewModel.CheckInBook(selStudent);

      
        }

        protected void CheckOutBook(object sender, EventArgs e)
        {        
            if (sender == null)
                throw new Exception("check out book failed");

            CheckOutData coData = sender as CheckOutData;

            if (coData != null)
            {
                ViewModel.CheckOutBook (coData);       
            }
        }

   
       

        //need this to update the available books for checking out.
  
        protected void SelectedStudentChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("SelectedStudentChanged called by ChildButton_Click");

            ViewModel.SelStudentChanged();
       
        }

      


        #region Event Handlers

        private void OnClearMenuItemClick(object sender, RoutedEventArgs e)
        {
            var mi = e.OriginalSource as MenuItem;
            if (mi == null) return;

            string sClearType = mi.Tag as string;
            int clearType = Convert.ToInt32(sClearType); ;
           
            if (ViewModel.studentData != null && ViewModel.studentData.HasCheckedOutBooks())
            {
                if (MessageBox.Show("Students still have checked out book bags.  Are you sure you want to clear the history?", "Clear History",
                    MessageBoxButton.YesNo) == MessageBoxResult.No)
                    return;
            }

            switch (clearType)
            {
                default:
                case 0: //clear history only
                    ViewModel.ClearHistory(true);
                    break;
                 case   1: //clear all of it.
                    ViewModel.ClearAll(true);
                    ShowInputDialog();
                    break;


            }
        }

    
        #endregion

        private void ShowInputDialog()
        {

            InputStartup inputWindow = new InputStartup();
            InputStartupViewModel vm = new InputStartupViewModel(ViewModel);
            inputWindow.DataContext = vm;
            inputWindow.Owner = this;
            inputWindow.Show();
            inputWindow.Activate();
            inputWindow.Topmost = true;

        }
   
        private void Window_SourceInitialized(object sender, EventArgs e)
        {

            // this is where your service call would be
            Database = new DatabaseReader();
            ViewModel = new MainWindowViewModel(Database);
            InitViews();

            StudentsDataView.CheckOutBookMethod += new EventHandler(CheckOutBook);
            BagDataView.CheckOutBookMethod += new EventHandler(CheckOutBook);
            
   
            //show the popup if we don't have any students or bags
            if (bShowInputWindow)
            {
                if (ViewModel.studentData.Students.Count == 0 || ViewModel.Teacher == "")
                   ShowInputDialog();
   
            }

        }



    }
}
