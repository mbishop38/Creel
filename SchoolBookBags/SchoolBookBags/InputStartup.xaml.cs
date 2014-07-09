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
    /// Interaction logic for InputStartup.xaml
    /// </summary>
    public partial class InputStartup : Window
    {
        public InputStartup()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
           try
           {
              int value = Convert.ToInt32(e.Text);

               
           
           }
           catch
           {
              e.Handled = true;
           }
        }

       private void FocusTextBoxOnLoad(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null) return;
            textbox.Focus();
        }

    }
}
