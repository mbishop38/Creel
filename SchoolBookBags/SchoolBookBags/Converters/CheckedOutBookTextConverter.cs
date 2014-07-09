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

namespace Converters
{
    class CheckedOutBookTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                throw new InvalidOperationException("Target type must be System.Windows.Media.ImageSource.");

            string oReturnedText = string.Empty;
            string oCurBagID = value.ToString();
            bool oHasBag = (oCurBagID == "") ? false : true;
            if (oHasBag == true) 
                oReturnedText = "Has book bag #: " + oCurBagID;
            else
              oReturnedText = "No bag";
           
            return oReturnedText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
