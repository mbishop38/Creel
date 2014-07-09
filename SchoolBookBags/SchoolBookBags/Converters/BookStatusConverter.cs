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
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using Converters;

namespace Converters
{
    public class BookStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("Target type must be System.Windows.Media.ImageSource.");

            string oImageName = string.Empty;
            string oID = value.ToString();
            bool oCheckedIn = (oID == "-1") ? true : false;

            {

                if (oCheckedIn == true)
                    oImageName = App.availableBookBMP;
                else

                    oImageName = App.unavailableBookBMP;
            }

            return oImageName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
