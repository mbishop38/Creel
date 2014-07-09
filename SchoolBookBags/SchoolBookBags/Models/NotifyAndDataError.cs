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
using System.ComponentModel;
using System.Text;
using System.Reflection;
using Converters.Common;



namespace Converters.Models
{
    public class NotifyAndDataError : INotifyPropertyChanged, IDataErrorInfo
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                StringBuilder error = new StringBuilder();

                foreach (PropertyInfo propertyInfo in GetType().GetProperties())
                {
                    foreach (Attribute attribute in propertyInfo.GetCustomAttributes(true))
                    {
                        if (attribute is RequiredAttribute)
                        {
                            object value = GetType().GetProperty(propertyInfo.Name).GetValue(this, null);

                            if (value == null)
                            {
                                error.Append(propertyInfo.Name);
                                error.AppendLine(" must not be null.");
                            }
                        }
                    }
                }

                return error.ToString();
            }
        }

        public string this[string columnName]
        {
            get
            {
                StringBuilder error = new StringBuilder();

                foreach (Attribute attribute in GetType().GetProperty(columnName).GetCustomAttributes(true))
                {
                    if (attribute is RequiredAttribute)
                    {
                        object value = GetType().GetProperty(columnName).GetValue(this, null);

                        if (value == null)
                        {
                            error.Append(columnName);
                            error.AppendLine(" must not be null.");
                        }
                    }
                }

                return error.ToString();
            }
        }

        #endregion

 
    }
}
