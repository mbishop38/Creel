using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Converters.ViewModels;
using Converters.Models;
using System.Diagnostics;
using System.Windows.Controls.Primitives;

namespace Converters.Views
{
    public partial class StudentView : UserControl
    {
        public StudentView()
        {
            InitializeComponent();

 
        }
        public event EventHandler CheckOutBookMethod;
    
        private StackPanel GetParentStackPanel(Object element)
        {
            StackPanel returnValue = null;
            if (element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent != null)
                    returnValue = (StackPanel) (((FrameworkElement)element).Parent);
            }
            return returnValue;
        }
        private string GetParents(Object element, int parentLevel)
        {
            string returnValue = String.Format("[{0}] {1}", parentLevel, element.GetType());
            if (element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent != null)
                    returnValue += String.Format("{0}{1}",
                        Environment.NewLine, GetParents(((FrameworkElement)element).Parent, parentLevel + 1));
            }

       
            return returnValue;
        }

        private static object GetObjectDataFromPoint(ListBox source, Point point)
        {
            UIElement element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);
                    if (data == DependencyProperty.UnsetValue)
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    if (element == source)
                        return null;
                }
                if (data != DependencyProperty.UnsetValue)
                    return data;
            }

            return null;
        }

 


        private void StudentsStackPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("draggingBag"))
            {
                if (sender != null)
                {
                    OneBookBagViewModel selBag = e.Data.GetData("draggingBag") as OneBookBagViewModel;

                    ListBox listBox = sender as ListBox;
                    if (listBox != null && listBox.SelectedItem != null && selBag != null)
                    {
                        AStudentViewModel droppedStudent = GetObjectDataFromPoint(listBox, e.GetPosition(listBox)) as AStudentViewModel;

                        if (droppedStudent != null)
                        {

                            CheckOutData coData = new CheckOutData();
                            coData.student = droppedStudent;
                            coData.bag = selBag;

                            if (CheckOutBookMethod != null)
                                CheckOutBookMethod(coData, EventArgs.Empty);
                        }
                      
                    }
                }
            }
        }

        private void StudentsStackPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("draggingBag") ||
                sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

    
 
        public event EventHandler StudentSelectionChangedMethod;

        private void StudentsStackPanel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                ListBox listBox = sender as ListBox;

                if (listBox != null)
                {
                    if (StudentSelectionChangedMethod != null)
                        StudentSelectionChangedMethod(this, EventArgs.Empty);
              }

            }
        }

    }
}
