using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Windows.Media;
using Converters.Models;
using System.Windows.Data;

namespace Converters
{
    class HistoryLineConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
 
            string formattedHistoryLine = "Invalid history";
            History historyLine = value as History;

            if (historyLine != null)
            {

                switch (historyLine.BookEventData.TheBookEventType)
                {
                    default:
                        formattedHistoryLine = "History unknown for book: " + historyLine.ID.ToString();
                        break;
                    case BookEvent.BookEventType.BookEventCheckIn:
                        formattedHistoryLine = historyLine.StudentID.ToString() + " Checked in book: " + historyLine.ID.ToString() +" on date " + historyLine.BookEventData.Date + ".\n\tSheet returned: " + historyLine.BookEventData.ReturnedSheet;
                        break;
                    case BookEvent.BookEventType.BookEventCheckOut:
                        formattedHistoryLine = historyLine.StudentID.ToString() +  " Checked in book: " + historyLine.ID.ToString() + " on date " + historyLine.BookEventData.Date + ".\n\tSheet returned: " + historyLine.BookEventData.ReturnedSheet;
                        break;
                }

                if (historyLine.BookEventData.Memo != string.Empty)
                {
                    formattedHistoryLine += ".\n\tMemo: " + historyLine.BookEventData.Memo;
                }
            }
            return formattedHistoryLine;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }
    }
}
