
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
using System.Diagnostics;
using Converters.ViewModels;
using System.Collections.ObjectModel;
using Converters.Models;

namespace Converters.Views
{
   
    public partial class BookBagView : UserControl
    {

        private Point startPoint;
        public BookBagView()
        {
            InitializeComponent();

        }

       public event EventHandler GetBookContextMenuItemsMethod;
        public event EventHandler CheckOutBookMethod;

  
        private void BookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
                        {
                             ListBox listBox = sender as ListBox;
                            if (listBox != null && e.AddedItems.Count == 1 )
                            {
                                //    OneBookBagViewModel targetItem = listBox.SelectedItems[0] as OneBookBagViewModel;

                                Converters.ViewModels.OneBookBagViewModel targetItem = e.AddedItems[0] as Converters.ViewModels.OneBookBagViewModel;
                                {
                                   if (GetBookContextMenuItemsMethod != null)
                                        GetBookContextMenuItemsMethod(targetItem, EventArgs.Empty);
                                }
                            }
          
                        }
 
        }
        private void bookList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);
       }

        private void bookList_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed && e.LeftButton != MouseButtonState.Released &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListBox listBox = sender as ListBox;

                if (listBox != null)
                {
                    ListBoxItem listItem =
                        FindAncestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                    if (listItem != null && listItem.HasContent)
                    {
                        Converters.ViewModels.OneBookBagViewModel vm = listItem.Content as Converters.ViewModels.OneBookBagViewModel;
                        if (vm != null && vm.CheckedOutStudentID == "" && vm.ID != "")
                        {
                            // Initialize the drag & drop operation
                            DataObject dragData = new DataObject("draggingBag", vm);
                            DragDrop.DoDragDrop(listItem, dragData, DragDropEffects.Move);
                        }
                    }
                }
             } 
        }

        // Helper to search up the VisualTree
        private static T FindAncestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void bookList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the  ListItem
            ListBox listBox = sender as ListBox;

            if (listBox != null)
            {
                ListBoxItem listItem =
                      FindAncestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                if (listItem != null && listItem.HasContent)
                {                  
                    Converters.ViewModels.OneBookBagViewModel vm = listItem.Content as Converters.ViewModels.OneBookBagViewModel;
         
                    //not checked out already
                    if (vm != null && vm.CheckedOutStudentID == "" && vm.ID != "")
                    {
                        //send to main page for check out.
                        CheckOutData coData = new CheckOutData();
                        coData.bag = vm;
                        coData.student = null;
                        if (CheckOutBookMethod != null)
                            CheckOutBookMethod(coData, EventArgs.Empty);
                    }
                }
             } 
        }
       
    }
}
