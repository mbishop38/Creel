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
using System.Diagnostics;

namespace Converters.Models
{
    public class BookEvent
    {
        public enum BookEventType
        {   
            BookEventCheckIn = 0,
            BookEventCheckOut = 1
        }

       
        public BookEventType TheBookEventType {get; set;}
        public bool ReturnedSheet {get;set;}
        public string Memo { get; set; }
        public DateTime Date { get; set; }


    }
    public class History
    {
        public History()
        {

        }


        public History(string inID, string inStudentID, BookEvent.BookEventType inType, DateTime inDate, bool inSharingSheet, string inMemo)
        {
            ID = inID;
            StudentID = inStudentID;
            BookEventData = new BookEvent()
                {
                    Memo = inMemo,
                    ReturnedSheet = inSharingSheet,
                    TheBookEventType = inType,
                    Date = inDate
                };
        }

        static public string FormattedBookEventType(BookEvent.BookEventType bookTypeIn)
        {
                switch (bookTypeIn)
                {
                    default:
                        return "unknown";
                     case BookEvent.BookEventType.BookEventCheckIn:
                       return "checked in";
                    case BookEvent.BookEventType.BookEventCheckOut:
                        return "checked out";
                 }
              
        }

        public string ID { get; set; }
        public string StudentID { get; set; }
        public BookEvent BookEventData;

        public bool Validate(ref string errorOut)
        {
            if (StudentID == "")
                errorOut = "invalid student ID";
            if (ID == "")
                errorOut = "invalid id";

            if (errorOut != "")
                return false;
            else return true;
        }

	}


}
