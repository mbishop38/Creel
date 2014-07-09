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
using System.Windows.Data;
using System.Globalization;
using Converters;

namespace Converters
{

    public class HasBooksToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            bool hasBooks = (bool)value;

             string oImageName = string.Empty;

             if (hasBooks == true)
                 oImageName = App.currentBookBMP;
         //   else
           //      oImageName = MainWindow.unavailableBookBMP;

             return oImageName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
