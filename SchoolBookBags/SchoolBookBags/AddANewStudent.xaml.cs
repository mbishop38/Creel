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
using System.Windows.Shapes;
using Converters.ViewModels;
using System.Windows.Threading;

namespace Converters.Views
{
    /// <summary>
    /// Interaction logic for AddANewStudent.xaml
    /// </summary>
    public partial class AddANewStudentView : UserControl
    {
        public AddANewStudentView()
        {
            InitializeComponent();

            this.IsVisibleChanged += new DependencyPropertyChangedEventHandler(
            UserControl_IsVisibleChanged);

   
          
        }

  
        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.Visibility == Visibility.Visible)
            {
           
                //set the focus to the first edit box.
                this.Dispatcher.BeginInvoke((Action)delegate
                {
                    Keyboard.Focus(FirstNameInput);
                }, DispatcherPriority.Render);
            }
        }
    }
}
