   M S B A M L   `   `   `  ����I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Converters
Converters  5      "    Converters.Views.HistoryView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .	converterclr-namespace:Converters     5        � 	Resources  5
      ,  &Converters.StringToVisibilityConverter   StringToVisibilityConverter2  ,Converters.BoolToForegroundColorRedConverter % !BoolToForegroundColorRedConverter%   Converters.ErrorStatusConverter  ErrorStatusConverter$   Converters.MemoStatusConverter  MemoStatusConverter   Converters.BoolInverter  BoolInverter)   #Converters.MemoStatusEmptyConverter  MemoStatusEmptyConverter  noHeaderList �� 
TargetType �� Property �� Value  myHistoryLineTemplate � RowDefinitions � ColumnDefinitions �� Padding �� Path �� Orientation	 � ToolTip
 �� 	Converter �� Mode��%@  &	        &	      &	 &     &	 9     &	 L     &	 _     &	 r     &	 �     7  7    5   
   60     5   
   66     5   
   6)     5   
   6(     5   
   6!     5   
   6-   �� 5   
   " ��6%   .�5      ��  � y�$ 	Collapsed=�6+   5      �� 5   
   .R�5      �  5      �� 5       5      �� 5      $
��.30* �6'   �� 5      $
��.30* �6'   �� 5      $
��.40* �6'   .+�5#      �� $��0��6   $��0��6)   $��14(�6[   $	 2,2q�6i   ��69   �� $ DateOccured�6i   �� 5'      $ 
Horizontal=�6   $��0��67   $��1��6D   .+�5(      �� $	 6,2q�6    $��14(�6N   ��6.   �� $ 	EventType�6N   �� 5)      $��16��6�   $��16��6�   $	 No sharing sheet returned��6�   ��6   �� $ SharingSheetBool�8
   $ OneWay=�6�   �� 5*      $��16��6z   $��16��6�   ��6   �� $ MemoStr�8
  $ OneWay=�	 6�   �� $ MemoStr�6�   5+      �� 5,      $��0��6   $��2��6)   $��14(�6\   $	 2,2q�6j   ��69   �� $ formattedBag�6j   5-      5/      51      .��53      � -   5         �Name$ 
LayoutRoot��53       54   
   �� 55      $��3* �6   �� 56      $	��20* �6   �� 57      $��2* �6   .+�5:   
   �� $��0��6   $��0��6#   $ 
Horizontal=�63   $��
5,5,5.2,10q�6L   .+�5;      �� $ 
Horizontal=�6   $��0,0,0,3q�62   .+�5<      �� $	��Top=�6   .��60   	Filter:69   �� 5=      $	��160��6   $	��Top=�6'    �� SelectedIndex 6?   �� *
FilterType+$ TwoWay=�.@�5>      �� $��0,2,0,2q�6#   .��65   All History6B   �� 5?      $��0,2,0,2q�6#   .��65   Outstanding Books6H   �� 5@      $��0,2,0,2q�6#   .��65   Sharing Sheet Not Returned6Q   �� 5A      $��0,2,0,2q�6#   .��65   Memos6<   5B      5C      �� 5D      $	��Top=�6   $��Right=�6/   $��30,5,0,0q�6K   $ ��Show selected student only��6�   $	��198��6�    d� 	IsChecked 6^   �� *ShowSelectedStudentOnly+$ TwoWay=�6�   5E      � 5G   
   $��1��6   $��2q�6   $��0��6'   
������� 5H      �� 5I      6"    5K      �� 5L      $	��51* �6#   61   �� 5M      $	��59* �6#   .+�5O      �� $��2��6   .��5P      �� -   5        $��listViewOfStudents��5P   '   $��0��6   $��Stretch=�6�   $��2��6�   $
��Auto=�6�   ��6A   �� $ MappedHistoryToStudent�y�6u   �� $ ShowList���5Q      �� 5R      .R�5S      ��  �� CornerRadius$ 2��6!   $��2q�62   
�������$	��3,3q�6]   ._�5T      �� .+�5U   "   �� $
��10,2q�6-   .+�5V   *   �� $��SemiBold"�6Z   $��18(�6p   ���	#FF0B9ABC��64   �� *formattedStudentName+6~   �� 5W   &   $��15(�6Y   ��60   �� *formattedNumberOfBooks+6Y   5X   #   �� 5Y   &   
�������$��0,2,0,0q�6G   $��10,10q�6b   #��[ 6�   $��Stretch=�6�   ��6q   �� $ historyListWithFilter�6�   5Z      5[      5\      5^      5_      �� 5a      $��0��6   $0��*No history found for the selected criteria��6&   $	��254��6�   $��Center=�6�   $��2��6�   $��Center=�6�   y�6X   �� $ ShowMessage�6�   5c      �� 5d   
   -   5        $��2��5d      $��0��6   .��6C   Print Report6Q   5e      5f      