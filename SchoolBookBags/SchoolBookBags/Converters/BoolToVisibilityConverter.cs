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
using System.Text;
using Converters;
using Converters.ViewModels;

namespace Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            bool isVisible = (bool)value;

            Visibility  oVisible = Visibility.Hidden;

            if (isVisible == true)
                oVisible = Visibility.Visible;

            return oVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }

    class BoolToVisibilityCollapsedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            bool isVisible = (bool)value;

            Visibility oVisible = Visibility.Collapsed;

            if (isVisible == true)
                oVisible = Visibility.Visible;

            return oVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }



    class InputTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            InputStartupViewModel.BBInputType isVisible = (InputStartupViewModel.BBInputType)value;

            Visibility oVisible = Visibility.Hidden;

            if (isVisible == InputStartupViewModel.BBInputType.bbInputStudents)
                oVisible = Visibility.Visible;

            return oVisible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }

}
