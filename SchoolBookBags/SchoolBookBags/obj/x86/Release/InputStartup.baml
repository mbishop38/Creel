   M S B A M L   `   `   `  ���� I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Converters.CommonConverters.Common  'clr-namespace:Converters
Converters      Converters.InputStartup   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     0comnclr-namespace:Converters.Common     .	converterclr-namespace:Converters     -     �Name$  InputStartupWindow�� � Title$. (Please input all class information below��$	��360��$	��522�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterOwner=� � WindowStyle$
 None=�$��5q����	#FFC2CCDB � ShowInTaskbar .   � AllowsTransparency . ���	#FFF0F0F0 � 	Resources    imageButton �� 
TargetType	 �� Triggers
 P� Property P� Value �� Property �� Value*  $Converters.BoolToVisibilityConverter  BoolToVisibilityConverter  oneStudentInputTemplate �� Orientation �� Path �� UpdateSourceTrigger J� InputBindings
 �� Key �� Command��%h  &	        &	 B     &	 G     7   �� " ��	 P� 
 � {� .  . ���  �@J�Opacity$ 0Q�  �� .R��� $ 
Horizontal=�.+��� $��4,4,0,0q����� $ Index�$ PropertyChanged=��� .��Student First Name:�� -   $��2q�$��20��$	��100��$��Right=����� $ 	FirstName�$ PropertyChanged=��� .��Student Last Name:�� $��LastNameTextBox��$��20��$��2q�$	��120��$��Right=����� $ LastName�$ PropertyChanged=�.�� �� $ Enter�� �� *_SaveStudentCommand+.���� 8��  $��20������ *_SaveStudentCommand+.���� $��/res/Check.png��$��2q�.��� -    �Name$ MainInputStartupGrid�� � UseLayoutRounding .  � RowDefinitions �� �� $��4* ��� .+��� $��0��  BorderBG#��[ 
�������$��1q�._��� -   $��
textBlock1��$
��Wrap=�$��	ExtraBold"�$��14(�$
��Left=�$��42�� �� Padding$
 10,0q����� *
HeaderText+� $��1��y��� $ ShowBasicInput� �� Mode$ OneWay=� � ColumnDefinitions �� �� $
��2.5* � �� �� �� �� �� .+��� $��0��$��0��$��10q�.��Teacher Name:�� $��1�� �� TextWrapping$
 Wrap=�$��24��$��0,0,6,0q����� *TeacherName+$ PropertyChanged=��� $��1��$��0��$��10q�.��Grade:�� $��1��$��1��$��24��$��0,0,6,0q�  GradeListString#��[  �� DisplayMemberPath$ ValueString�� �� SelectedValuePath$ ValueString�� �� SelectedValue �� *Grade+�� $��2��$��0��$��10q�.��Number Of Students:�� -   $��2��$��1��$��24��$
 Wrap=�$��0,0,6,0q� �� 	MaxLength$ 2������ *NumStudents+$ PropertyChanged=�� $��1��y��� $ ShowStudents�$ OneWay=� ��  �� .+��� $
��Auto=�$
��Auto=�.���� $��0��$��0��#��[ ���� $ InputtedStudents�$ TwoWay=�� $��1��y��� $ 
ShowFinish�$ OneWay=� ��  �� .+��� $��0��$��0��$��Expanded&�$
 Wrap=�.����Congratulations! All student and teacher data has been saved in the book bag program. Select finish to start managing your students bags.�� $��2��$��100,10, 100,10q�$��2q� �� 	IsDefault . ���� *_NextCommand+���� $ 
ButtonText�