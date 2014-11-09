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
using System.ComponentModel;
using Converters.ViewModels;


namespace Converters.Views
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : UserControl
    {
        public HistoryView()
        {
            InitializeComponent();
        }


        //clicked on report.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = null;
            HistoryViewViewModel vm = this.DataContext as HistoryViewViewModel;
            if (vm != null)
                doc = vm.GetDocumentPrintOutput();

            if (doc != null)
            {

                PrintDialog dialog = new PrintDialog();
                if (dialog.ShowDialog() == true)
                {
                   
                        IDocumentPaginatorSource idpSource = doc;
                        // Call PrintDocument method to send document to printer 

                     dialog.PrintDocument(idpSource.DocumentPaginator, "Hello WPF Printing.");
                    
                }
            }
            else            
                        throw new Exception("Error printing student report.");

                    

        }
    }
}

