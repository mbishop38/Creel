   M S B A M L   `   `   `  ����I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Converters.CommonConverters.Common  5      'clr-namespace:Converters
Converters  5   	       Converters.InputStartup   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0comnclr-namespace:Converters.Common     5      .	converterclr-namespace:Converters     5   	   -   5          �Name$  InputStartupWindow��5   	    � Title$. (Please input all class information below��5   	   $	��360��6:   $	��522��6G    � 
ResizeMode$ NoResize=�6S    � WindowStartupLocation$ CenterOwner=�6i    � WindowStyle$
 None=�6�   $��5q�6�   ���	#FFC2CCDB � ShowInTaskbar .   � AllowsTransparency . ���	#FFF0F0F0 � 	Resources 5	         imageButton �� 
TargetType	 �� Triggers
 P� Property P� Value �� Property �� Value*  $Converters.BoolToVisibilityConverter  BoolToVisibilityConverter  oneStudentInputTemplate �� Orientation �� Path �� UpdateSourceTrigger J� InputBindings
 �� Key �� Command��%�  &	        &	 �     &	 �     7   �� 5   
   " ��6$   	 5      P� 5      
 � {� .  . �5      ��  �@J�Opacity$ 0Q�60   5      5        5   
   6.   �� 5      .R�5   
   �� $ 
Horizontal=�6   .+�5      �� $��4,4,0,0q�6[   ��6   �� $ Index�$ PropertyChanged=�6o   �� 5      .��6   Student First Name:62   �� 5      -   5        $��2q�5   _   $��20��6j   $	��100��6v   $��Right=�6�   ��6   �� $ 	FirstName�$ PropertyChanged=�6�   �� 5      .��6   Student Last Name:6)   �� 5      $��LastNameTextBox��6   $��20��61   $��2q�6�   $	��120��6�   $��Right=�6�   ��6=   �� $ LastName�$ PropertyChanged=�.��6�    5      �� 5      $ Enter��6M    6%   �� *_SaveStudentCommand+6M   .��5   -   5      �� 5      8��  $��20��6f   ��6>   �� *_SaveStudentCommand+.��5      �� $��/res/Check.png��6   $��2q�64   6A   5       5!      5"      5#      .��5%      � -   5         �Name$ MainInputStartupGrid��5%       � UseLayoutRounding .  � RowDefinitions 5&   
   �� 5'      6   �� 5(      $��4* �6   6*   �� 5)      6   .+�5+   
   �� $��0��6     BorderBG#��[ 6   
�������$��1q�6Y   ._�5,   
   �� -   5        $��
textBlock1��5,   j   $
��Wrap=�6   $��	ExtraBold"�6(   $��14(�6@   $
��Left=�6O   $��42��6|    �� Padding$
 10,0q�6�   ��6�   �� *
HeaderText+6�   5-      � 5.   
   $��1��6   y�6   �� $ ShowBasicInput� �� Mode$ OneWay=� � ColumnDefinitions 5/      �� 50      6%   �� 51      $
��2.5* �6#   62    53      �� 54      6"   �� 55      6#   �� 56      6#   �� 57      6#   �� 58      .+�5:      �� $��0��6   $��0��6!   $��10q�61   .��6>   Teacher Name:6M   �� 5;      $��1��6    �� TextWrapping$
 Wrap=�6&   $��24��6:   $��0,0,6,0q�6F   ��6W   �� *TeacherName+$ PropertyChanged=�6�   �� 5<      $��1��6   $��0��6!   $��10q�61   .��6=   Grade:6E   �� 5=      $��1��6   $��1��6$   $��24��64   $��0,0,6,0q�6@     GradeListString#��[ 5>       �� DisplayMemberPath$ ValueString��5?       �� SelectedValuePath$ ValueString��5@       �� SelectedValue 5A      �� *Grade+�� 5B      $��2��6   $��0��6!   $��10q�61   .��6=   Number Of Students:6R   �� 5C      -   5        $��2��5C      $��1��6#   $��24��64   $
 Wrap=�6m   $��0,0,6,0q�6�    �� 	MaxLength$ 2��6�   ��6�   �� *NumStudents+$ PropertyChanged=�6�   5D      � 5E   
   $��1��6   y�6   �� $ ShowStudents�$ OneWay=� 5F      �� 5G      6%    5I      �� 5J      6"   .+�5M      �� $
��Auto=�6   $
��Auto=�6>   .��5N      �� $��0��6    $��0��6-   #��[ 5O      ��5P      �� $ InputtedStudents�$ TwoWay=�5Q      5R      5S      � 5U   
   $��1��6   y�6   �� $ 
ShowFinish�$ OneWay=� 5V      �� 5W      6%    5Y      �� 5Z      6"   .+�5]      �� $��0��6   $��0��6#   $��Expanded&�63   $
 Wrap=�6J   .��6^   ��Congratulations! All student and teacher data has been saved in the book bag program. Select finish to start managing your students bags.5`      5a      �� 5c   
   $��2��6   $��100,10, 100,10q�6   $��2q�6X    �� 	IsDefault . ��67   �� *_NextCommand+��6d   �� $ 
ButtonText�6�   5e      5f      