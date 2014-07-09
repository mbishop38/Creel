using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    class ShowingInfoToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return string.Empty;

            bool showingImage = (bool)value;

            string oImageName = string.Empty;

            if (showingImage== true)
                oImageName = "/SchoolBookBags;component/res/arrowleftsmall.png";
            else
                oImageName = "/SchoolBookBags;component/res/arrowrightsmall.png";

            return oImageName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}

