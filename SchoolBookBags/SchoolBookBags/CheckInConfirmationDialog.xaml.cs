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
using System.Windows.Shapes;
using Converters.ViewModels;

namespace Converters
{
    /// <summary>
    /// Interaction logic for CheckInConfirmationDialog.xaml
    /// </summary>
    public partial class CheckInConfirmationDialog : Window
    {
        
        public CheckInConfirmationDialog(CheckInConfirmationViewModel ciConfVM)
        {
            InitializeComponent();

            DataContext = ciConfVM;

          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void buttonCI_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
