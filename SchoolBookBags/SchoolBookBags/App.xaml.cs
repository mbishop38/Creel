
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Converters
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public readonly static string currentBookBMP = "/res/RedBook.png";
        public readonly static string usedBookBMP = "/res/GreyBook.png";
        public readonly static string availableBookBMP = "/res/BlueBook.png";
        public readonly static string unavailableBookBMP = "/res/GreyBook.png";


        public readonly static string disabledSaveBMP = "/res/SaveDisabled2.png";
        public readonly static string enabledSaveBMP = "/res/SaveEnabled.png";
        

    }


}

/*3:03 PMBISHOP MandiHi Fred


3:03 PMDOLLEN FrederickHi Mandi


3:05 PMBISHOP MandiQuick question:  In my wpf program, I want to launch a setup screen which will take input for the first time for a empty database.  This willl not launch again until the data is cleared.  I thought I might us a popup for this but I am unsure.  I want it to be on top of my main window.  Any suggestions on an approach?


3:08 PMDOLLEN Frederickyou can't show a popup window until your main window has been created and shown,
this can be done by overriding the OnSourceInitialized() method of hte window class.  In there, you can check for this condition, and show a modal window
There's also a window "Loaded" event you can have a handler for.  One of those two ways will do the trick


3:10 PMBISHOP MandiOk.  I'll give it a shot.  do you think a popup is the way to go or does it need to be some other type of window?
 
 
 One cool way to do it is to have an overlay on the main window - where the non-dialog portion of the overlay is translucent.  I've seen it done but not sure how to do it
i would start with a popup - easier to manage
 * 
 * you can also have a popup control - which is really part of the main window, or a separate window.  The popup control has a hide/show method.  Popup control doesn't have a dialog/window frame, just a single line border
 * 
 * 
 * 
 * 
 *                 <Popup  Placement="Right" IsOpen="{Binding Path=ShowingInfo}" Width="400" Height="400" >
                    <views:SelectedStudentInformation x:Name="SelStudentInformation"  />
                </Popup>
 * DataContext="{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type ListBoxItem}}}"
 */
