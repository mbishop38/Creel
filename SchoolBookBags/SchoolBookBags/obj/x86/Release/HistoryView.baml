   M S B A M L   `   `   `  ���� I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Converters
Converters  "    Converters.Views.HistoryView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     .	converterclr-namespace:Converters       � 	Resources  ,  &Converters.StringToVisibilityConverter   StringToVisibilityConverter2  ,Converters.BoolToForegroundColorRedConverter % !BoolToForegroundColorRedConverter%   Converters.ErrorStatusConverter  ErrorStatusConverter$   Converters.MemoStatusConverter  MemoStatusConverter   Converters.BoolInverter  BoolInverter)   #Converters.MemoStatusEmptyConverter  MemoStatusEmptyConverter  noHeaderList �� 
TargetType �� Property �� Value  myHistoryLineTemplate � RowDefinitions � ColumnDefinitions �� Padding �� Path �� Orientation	 � ToolTip
 �� 	Converter �� Mode d�Name J� IsHitTestVisible �� ElementName �Name � 	StaysOpen��%�  &	        &	      &	 
     &	      &	      &	      &	      &	 H     7  7  7  7              �� " ��.���  � y�$ 	Collapsed=��� .R��  ��  �� $
��.30* ��� $
��.30* ��� $
��.40* �.+��� $��0��$��0��$��14(�$	 2,2q����� $ DateOccured��� $ 
Horizontal=�$��0��$��1��.+��� $	 6,2q�$��14(����� $ 	EventType��� $��16��$��16��$	 No sharing sheet returned������ $ SharingSheetBool�8
   $ OneWay=�d� $	 Btn�� �� $ Popup��$ IsOpen�$ OneWay=�8
  y��� $ MemoStr�8
  .���� $��16��$��16������ $ MemoStr�8
  $ OneWay=�	 �� $ MemoStr�� $ Popup�� .  $��64��$��64������ *	IsChecked+$	 Btn��.���� 
���   �$��1q�
�������._��� $��This is a popup���� $��0��$��2��$��14(�$	 2,2q����� $ formattedBag�.��� -    �Name$ 
LayoutRoot�� �� $��3* ��� $	��20* ��� $��2* �.+��� $��0��$��0��$ 
Horizontal=�$��
5,5,5.2,10q�.+��� $ 
Horizontal=�$��0,0,0,3q�.+��� $	��Top=�.��	Filter:�� $	��160��$	��Top=� �� SelectedIndex �� *
FilterType+$ TwoWay=�.@��� $��0,2,0,2q�.��All History�� $��0,2,0,2q�.��Outstanding Books�� $��0,2,0,2q�.��Sharing Sheet Not Returned�� $��0,2,0,2q�.��Memos�� $	��Top=�$��Right=�$��30,5,0,0q�$ ��Show selected student only��$	��198�� d� 	IsChecked �� *ShowSelectedStudentOnly+$ TwoWay=�� $��1��$��2q�$��0��
������� ��  �� $	��51* ��� $	��59* �.+��� $��2��.���� -   $��listViewOfStudents��$��0��$��Stretch=�$��2��$
��Auto=����� $ MappedHistoryToStudent�y��� $ ShowList����� .R���  �� CornerRadius$ 2��$��2q�
�������$	��3,3q�._��� .+��� $
��10,2q�.+��� $��SemiBold"�$��18(����	#FF0B9ABC���� *formattedStudentName+�� $��15(����� *formattedNumberOfBooks+�� 
�������$��0,2,0,0q�$��10,10q�#��[ $��Stretch=����� $ historyListWithFilter��� $��0��$0��*No history found for the selected criteria��$	��254��$��Center=�$��2��$��Center=�y��� $ ShowMessage��� -   $��2��$��0��.��Print Report