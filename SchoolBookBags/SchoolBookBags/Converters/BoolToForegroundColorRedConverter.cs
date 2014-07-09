using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Converters;

namespace Converters
{
//    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    class BoolToForegroundColorRedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool oSharingSheetReturned = (bool)value;
            if (oSharingSheetReturned == true)
                return new SolidColorBrush(Colors.Black);
            else
                return new SolidColorBrush(Colors.Crimson);

          }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
