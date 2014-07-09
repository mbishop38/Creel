using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Data;
using System.Windows;


namespace Converters
{
    class UIElementShowingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string value1 = value.ToString();
            string param1 = (string)parameter;
          

            if (value1 != param1)
                return Visibility.Hidden;
            else
                return Visibility.Visible;

       }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
