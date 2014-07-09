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
using Converters.Models;

namespace Converters.Tests
{
    public static class UnitTestUtility
    {
        private static Random randomSeed = new Random();

        private static DateTime RandomDay() 
        { 
            DateTime start = new DateTime(1995, 1, 1);      
            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            return start.AddDays(randomSeed.Next(range)); 
        }
 

        public static void PopulateWithTestData(History populateThis)
        {
            populateThis.ID = (GetReasonablyUniqueInt(-1, 5)).ToString();
            populateThis.StudentID = (GetReasonablyUniqueInt(-1, 33)).ToString();
            populateThis.BookEventData = new BookEvent() 
                { Memo = GetReasonablyUniqueString("Memo"), 
                    ReturnedSheet= GetReasonablyRandomBool("Memo"), 
                    TheBookEventType = (GetReasonablyRandomBool("test")) ? BookEvent.BookEventType.BookEventCheckIn : BookEvent.BookEventType.BookEventCheckOut,
                    Date = RandomDay()
                };
                  
        }

        public static void PopulateWithTestData(Student populateThis)
        {
            populateThis.ID = (GetReasonablyUniqueInt("ID")).ToString();
            populateThis.HasBooks = GetReasonablyRandomBool("HasBooks");
            populateThis.FirstName = GetReasonablyUniqueString("FirstName");
            populateThis.LastName = GetReasonablyUniqueString("LastName");
          //  populateThis.CurrentBagID = (GetReasonablyUniqueInt(-1, 5)).ToString();
        }

        public static void PopulateWithTestData(BookBag populateThis)
        {
            populateThis.ID = (GetReasonablyUniqueInt(-1, 33)).ToString();
            populateThis.CheckedOutStudentID = (GetReasonablyUniqueInt(-1, 5)).ToString();
            populateThis.ImageStatus = App.availableBookBMP;
        }
    
   

   /*     public static void PopulateWithTestData(IStudentModelViewInterface populateThis)
        {

            populateThis.
            populateThis.Id.Value = GetReasonablyUniqueInt("Id");
            populateThis.EmailAddress.Value = GetReasonablyUniqueString("Email");
            populateThis.FirstName.Value = GetReasonablyUniqueString("FirstName");
            populateThis.LastName.Value = GetReasonablyUniqueString("LastName");
        }
        */


        public static int GetReasonablyUniqueInt(int min, int max)
        {
            return randomSeed.Next(min, max);
        }


        public static int GetReasonablyUniqueInt(string prefix)
        {
            var guidAsByteArray = Guid.NewGuid().ToByteArray();

            int total = 0;

            foreach (byte byteFromGuid in guidAsByteArray)
            {
                total += ((int)byteFromGuid);
            }

            return prefix.Length + total;
        }

        public static bool GetReasonablyRandomBool(string prefix)
        {
            return (randomSeed.NextDouble() > 0.5);
        }
        public static string GetReasonablyUniqueString(string prefix)
        {
            string fullValue = String.Format("{0}{1}", prefix, Guid.NewGuid());

            if (fullValue.Length > 20)
            {
                return fullValue.Substring(0, 20);
            }
            else
            {
                return fullValue;
            }
        }
/*
        public static void AssertFieldValue<T>(T expected, ViewModelField<T> fieldInstance, string fieldName)
        {
            Assert.IsNotNull(fieldInstance, "Field instance for '{0}' was null.", fieldName);

            var actual = fieldInstance.Value;

            Assert.AreEqual<T>(expected, actual, "Field value for '{0}' did not match.", fieldName);
        }*/
    }
}
