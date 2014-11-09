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
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Converters.ViewModels;
using System.Collections.Generic;
using Converters.Models;
using SimpleLogNS;

public interface IDatabaseReader
{

    bool SaveBagData(ObservableCollection<OneBookBagViewModel> bookBagsList);
    bool mbImportHistory(ObservableCollection<History> h);
    bool SaveStudents(ObservableCollection<AStudentViewModel> studentList, bool bCleared);
    void InitBasicInfo( MainWindowViewModel viewModel );
    bool Init();
    bool mbImportStudents(ObservableCollection<AStudentViewModel> s);
    bool mbImportBags(ObservableCollection<OneBookBagViewModel> b);
    bool SaveBasicInfo(MainWindowViewModel viewModel);
    bool SaveHistory(ObservableCollection<History> historyList, bool SaveToBackup);
}

namespace Converters.Models
{
    public class DatabaseReader : IDatabaseReader
    {
        private XDocument moXmlDoc;
        private XmlReader moXmlReader;
        private string moDocumentName = "database.xml";
        private string moDocumentNameBkup = "database-bk.xml";

        bool Connected = false;

        public DatabaseReader()  { }
        public bool Init()     { return mbConnectToDatabase();  }

        private bool mbConnectToDatabase()
        {
            if (Connected)
                return true;

            try
            {

                SimpleLog.Info("Connecting to database...");
                string sAppFolder = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                if (sAppFolder.EndsWith("\\") == false)
                    sAppFolder += "\\";

                string moXmlFullPath = sAppFolder + moDocumentName;
                SimpleLog.Info("App Folder: " + sAppFolder + " XML Path: " + moXmlFullPath);


                moXmlReader = XmlReader.Create(moXmlFullPath);
                moXmlDoc = XDocument.Load(moXmlReader);
                Connected = true;


                return true;
            }
            catch (SystemException e)
            {
                SimpleLog.Error("Error connecting to database: " + e.Message);

                return false;
            }
        }

        public void InitBasicInfo( MainWindowViewModel viewModel )
        {
            SimpleLog.Info("Database initializing basic information.");
            if (Connected == false)
            {
                MessageBox.Show("Invalid installation: file " + moDocumentName + " not found.  See your administrator for assistance.");
                throw new Exception("database not connected");
            }
            var classRoot = moXmlDoc.Element("Class").Element("teacher");

            if (classRoot != null)
            {

                viewModel.Teacher = classRoot.Attribute("name").Value;


                if (classRoot.Element("school") != null)
                    viewModel.School = classRoot.Element("school").Attribute("name").Value;

                if (classRoot.Element("gradelevel") != null)
                    viewModel.Grade = classRoot.Element("gradelevel").Value;
                if (classRoot.Element("bookbagset") != null)
                    viewModel.BookBagSet = classRoot.Element("bookbagset").Attribute("id").Value;
                if (classRoot.Element("bookbagset") != null)
                    viewModel.NumberOfBooks = (Convert.ToInt32(classRoot.Element("bookbagset").Attribute("BookCount").Value));
            }
       
            return;

        }
        public bool SaveHistory(ObservableCollection<History> historyList, bool SaveToBackup)
        {
            SimpleLog.Info("Saving history to database.");
            if (SaveToBackup == true)
            {
                moXmlDoc.Save(moDocumentNameBkup);
                moXmlReader.Close();

            } 
            
            try
            {
 
                XElement historyElem = moXmlDoc.Element("Class").Element("History");

                historyElem.Elements().Remove();
                int nCount = historyList.Count;
             
                foreach (History historyItem in historyList)
                {

                    historyElem.Add(new XElement("Bag",
                            new XAttribute("id", historyItem.ID),
                            new XAttribute("studentid", historyItem.StudentID),
                            new XAttribute(historyItem.BookEventData.TheBookEventType == BookEvent.BookEventType.BookEventCheckIn ? "in" : "out", historyItem.BookEventData.Date.ToString()),
                           new XAttribute("sharingsheet", historyItem.BookEventData.ReturnedSheet ? "true" : "false" ),
                           new XAttribute("memo", historyItem.BookEventData.Memo != null ? historyItem.BookEventData.Memo : "")));
                }
            }
            catch (SystemException e)
            {
                SimpleLog.Error("History can't be saved to database.xml. Reason: " + e.Message);
                return false;
            }

            moXmlDoc.Save(moDocumentName);
           return true;        
        }
        public bool SaveBasicInfo( MainWindowViewModel viewModel )
        {
            SimpleLog.Info("Saving basic information to database.");
            moXmlDoc.Save(moDocumentNameBkup);
            moXmlReader.Close();
            try
            {
                XElement classRoot = moXmlDoc.Element("Class");


                //teacher
                XElement teacher = classRoot.Element("teacher");
                if (teacher == null)
                {
                    classRoot.Add(new
                        XElement("teacher", new XAttribute("name", viewModel.Teacher)));
                    teacher = classRoot.Element("teacher");
                }
                else
                    teacher.SetAttributeValue("name", viewModel.Teacher);

                //grade
                XElement grade = teacher.Element("gradelevel");
                if (grade == null)
                {
                    teacher.Add(new
                        XElement("gradelevel", viewModel.Grade));
                }
                else
                    teacher.SetElementValue("gradelevel", viewModel.Grade);

                //school name
                XElement schoolName = teacher.Element("school");
                if (schoolName == null)
                {
                    teacher.Add(new
                        XElement("school", new XAttribute("name", viewModel.School)));
                }
                else
                    schoolName.SetAttributeValue("name", viewModel.School);


                //book bag set
                XElement bookBagSet = teacher.Element("bookbagset");
                if (bookBagSet == null)
                {
                    teacher.Add(new
                        XElement("bookbagset", new XAttribute("id", viewModel.BookBagSet), new XAttribute("BookCount", viewModel.NumberOfBooks)));
                }
                else
                {
                    bookBagSet.SetAttributeValue("id", viewModel.BookBagSet);
                    bookBagSet.SetAttributeValue("BookCount", viewModel.NumberOfBooks);
                }


            }
            catch (SystemException e)
            {
                SimpleLog.Error("Teacher and class can't be saved to database.xml. Reason: " + e.Message);
                return false;
            }

            moXmlDoc.Save(moDocumentName);
           return true;
        }
        public bool SaveStudents(ObservableCollection<AStudentViewModel> studentList, bool bCleared)
        {
            SimpleLog.Info("Saving students to database.");
            try
            {
                XElement teacherRoot = moXmlDoc.Element("Class").Element("teacher");


                //students
                XElement students = teacherRoot.Element("students");
                if (students == null)
                {
                    teacherRoot.Add(new
                        XElement("students"));

                    students = teacherRoot.Element("students");
                }

                 int lastId =  1;
                 if (studentList.Count > 0)
                 {
                     AStudentViewModel studLast = studentList.ElementAt(studentList.Count-1);
                     lastId = Convert.ToInt32(studLast.ID) + 1;
                 }

                 XElement lastIDElem = students.Element("lastID");
                if (lastIDElem == null)
                {
                    students.Add(new XElement("lastID",
                        new XAttribute("id", lastId)));
                    lastIDElem = students.Element("lastID");
                }

                string sOldLastID = (string)lastIDElem.Attribute("id");
                int oldLastID = Convert.ToInt32(sOldLastID);
                var query = from c in students.Descendants("student") select c;              
                List<string> idsToBeRemoved = new List<string>();

                foreach (XElement xe in query)
                {
                    string sFirstName = xe.Attribute("firstname").Value;
                    string sLastName = xe.Attribute("lastname").Value;

                    bool bFound = false;

                    foreach (AStudentViewModel stud in studentList)
                    {
                        if (string.Compare(stud.Student.FirstName, sFirstName, StringComparison.InvariantCultureIgnoreCase) == 0 &&
                            string.Compare(stud.Student.LastName, sLastName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            bFound = true;
                            break;
                        }
                    }


                    if (bFound == false)
                    {
                      idsToBeRemoved.Add((string)xe.Attribute("id"));                    
                    }
  
                }

                foreach (string id in idsToBeRemoved)
                {
                   IEnumerable<XElement> foundStudent =
                                (from s in students.Elements("student")
                                 where
                                 (String.Compare((string)s.Attribute("id").Value, id, StringComparison.InvariantCultureIgnoreCase) == 0)
                                              select s);
                    if (foundStudent != null)
                    {
                        foundStudent.Remove();
                    }

                }

                int nCurID = oldLastID;
                foreach (AStudentViewModel stud in studentList)
                {
                    IEnumerable<XElement> foundStudent =
                                ( from s in students.Elements("student")
                                  where
                                  (String.Compare((string)s.Attribute("firstname").Value, stud.Student.FirstName, StringComparison.InvariantCultureIgnoreCase) == 0) &&
                                  (String.Compare((string)s.Attribute("lastname").Value, stud.Student.LastName, StringComparison.InvariantCultureIgnoreCase) == 0)
                                         select s);

                    if (foundStudent == null || foundStudent.Count<XElement>() <= 0)
                    {
                        students.Add( new XElement("student", 
                            new XAttribute("firstname", stud.Student.FirstName), 
                            new XAttribute("lastname", stud.Student.LastName),
                            new XAttribute("id", ++nCurID)));
                       
                    }
                    else
                    {
                        Debug.WriteLine(stud.FullName + " already in xml/n");
                    }     
                }


                lastIDElem.SetAttributeValue("id", bCleared ? 0 : nCurID);
                Debug.WriteLine("Old Last id " + oldLastID + " new last id " + nCurID);
            }
            catch (SystemException e)
            {
                SimpleLog.Error("Student data can't be saved to database.xml. Reason: " + e.Message);
                return false;
            }

            moXmlDoc.Save(moDocumentName);
           return true;        
        }
        public bool SaveBagData( ObservableCollection<OneBookBagViewModel> bookBagsList)
        {
            SimpleLog.Info("Saving bag data to database.");
            try
            {
                XElement bags = moXmlDoc.Element("Class").Element("BagsData");

                int nCount = bookBagsList.Count;
                int nIndex = 1;

                foreach (OneBookBagViewModel bookBag in bookBagsList)
                {
                    IEnumerable<XElement> foundBag =
                                ( from b in bags.Elements("bag")
                                  where
                                  (String.Compare((string)b.Attribute("id").Value, bookBag.ID, StringComparison.InvariantCultureIgnoreCase) == 0)
                                         select b);

                    if (foundBag == null || foundBag.Count<XElement>() <= 0)
                    {
                        bags.Add( new XElement("bag", 
                            new XAttribute("id", nIndex.ToString()), 
                            new XAttribute("checkedoutstudent", bookBag.CheckedOutStudentID)));
                    }
                    else
                    {
                        //modify if necessary
                        foreach (XElement oneB in foundBag)
                        {
                            string id = oneB.Attribute("id").Value;
                            oneB.SetAttributeValue("checkedoutstudent", bookBag.CheckedOutStudentID);
                          //  MessageBox.Show(id);
                          //  Debug.Assert(String.Compare((string)oneS.Attribute("id").Value, (string)stud.ID, StringComparison.InvariantCultureIgnoreCase) == 0);
                        }

                    }
                
                }
            
  
            }
            catch (SystemException e)
            {
                SimpleLog.Error("Bag data can't be saved to database.xml. Reason: " + e.Message);
                return false;
            }

            moXmlDoc.Save(moDocumentName);
            return true;
        
        }
        public bool mbImportStudents( ObservableCollection<AStudentViewModel> s)
        {
            SimpleLog.Info("Intializing students list from database.");
            if (Connected == false)
                throw new Exception("database not connected");
  
            var query = moXmlDoc.Element("Class")
                           .Element("teacher")
                           .Element("students")
                           .Elements("student");

            // Add child elements to the parent StackPanel
            foreach (XElement result in query)
            {
                Student s1 = new Student(result.Attribute("id").Value != null ? result.Attribute("id").Value : "")
                {
                    FirstName = result.Attribute("firstname") != null ? result.Attribute("firstname").Value : "",
                    LastName = result.Attribute("lastname") != null ? result.Attribute("lastname").Value : "",
                };

                  string errorOut = "";
                if (!s1.Validate(ref errorOut))
                {
                    if (errorOut == "")
                        MessageBox.Show("Invalid student information in the database for " + s1.FullName + ".  Please edit the data.");
                    else
                        MessageBox.Show("Invalid " + errorOut + " in the database for " + s1.FullName + ".  Please edit the data.");
                }

                AStudentViewModel sViewModel = new AStudentViewModel(s1);
                s.Add(sViewModel);
 
            }

            return true;
        }

        public bool mbImportBags(ObservableCollection<OneBookBagViewModel> b)
        {
            SimpleLog.Info("Intializing bags list from database.");
            if (Connected == false)
                throw new Exception("database not connected");
  
            Random random = new Random();
            var query = moXmlDoc.Element("Class")
                           .Element("BagsData")
                           .Elements("bag");

            List<string> checkedOutStudents = new List<string>();

            // Add child elements to the parent StackPanel
            foreach (XElement result in query)
            {
                BookBag bb = new BookBag()
                {
                    ID = result.Attribute("id") != null ? result.Attribute("id").Value : "",
                    CheckedOutStudentID = result.Attribute("checkedoutstudent") != null ? result.Attribute("checkedoutstudent").Value : "",
                };



                bb.ImageStatus = (bb.CheckedOutStudentID == "" ? App.availableBookBMP : App.unavailableBookBMP);


                string errorOut = "";
                if (!bb.Validate(checkedOutStudents, ref errorOut))
                {
                    if (errorOut == "")
                        MessageBox.Show("Invalid bag information in the database for " + bb.ID + ".  Please edit the data.");
                    else
                        MessageBox.Show("Invalid " + errorOut + " in the database for " + bb.ID + ".  Please edit the data.");
                }
                checkedOutStudents.Add(bb.CheckedOutStudentID);

        
                b.Add(new OneBookBagViewModel(bb));

            }

            return true;
        }

        public bool mbImportHistory(ObservableCollection<History> h)
        {
            SimpleLog.Info("Intializing history from database.");
            if (Connected == false)
                throw new Exception("database not connected");

          
            var query = moXmlDoc.Element("Class")
                           .Element("History")
                           .Elements("Bag");
                         
           
            // Add child elements to the parent StackPanel
            foreach (XElement result in query)
            {
                string id = result.Attribute("id").Value != null ? result.Attribute("id").Value : "";

                bool bHasSharingSheet = result.Attribute("sharingsheet") != null ? (Convert.ToBoolean(result.Attribute("sharingsheet").Value)) : false;
                string sDateIn = result.Attribute("in") != null ? result.Attribute("in").Value : "";
                string dateOut = result.Attribute("out") != null ? result.Attribute("out").Value : ""; ;
                DateTime dateINOut;
                  dateINOut  = sDateIn != "" ? Convert.ToDateTime(sDateIn) : Convert.ToDateTime(dateOut);

                  History h1 = new History
                      (result.Attribute("id") != null ? result.Attribute("id").Value : "",
                        result.Attribute("studentid") != null ? result.Attribute("studentid").Value : "",
                        sDateIn != "" ? BookEvent.BookEventType.BookEventCheckIn : BookEvent.BookEventType.BookEventCheckOut,
                        dateINOut,
                        bHasSharingSheet,
                        result.Attribute("memo") != null ? result.Attribute("memo").Value : "");

               
                string errorOut = "";
                if (!h1.Validate(ref errorOut))
                {
                    if (errorOut == "")
                        MessageBox.Show("Invalid history information in the database.  Please edit the data.");
                    else
                        MessageBox.Show("Invalid " + errorOut + " in the database.  Please edit the data.");
                }

                h.Add(h1);

            }
       
            return true;
        }

    }
}
