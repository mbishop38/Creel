   M S B A M L   `   `   `  ���� I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Converters.ViewsConverters.Views  'clr-namespace:Converters
Converters  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel "    Converters.Views.StudentView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     0viewsclr-namespace:Converters.Views     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .	converterclr-namespace:Converters     Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase       � 	Resources  )   #Converters.HasBooksToImageConverter   HasBooksToImageConverter,  &Converters.ShowingInfoToImageConverter  ShowingInfoToImageConverter3  -Converters.BoolToVisibilityCollapsedConverter & "BoolToVisibilityCollapsedConverter  StudentItemTemplate �� Orientation  NoChromeButton � ToolTip �� RelativeSource �� AncestorType �� Left
 �� Top �� Path �� 	Converter	 �� Mode
 ��Name��%W  &	        &	      &	 
     &	      7   7 �7        �� .R��� $ 
Horizontal=�.+��� #���  $$ Click on the book to check in.��$��0,0,0,0q�$��34��$��36������ * DataContext._BookButtonPressed+ �� $ ListBoxL����� .���� $��0,0,0,0q�.+��� $��	BookImage��$ -5��$	 -15��$��Center=�$��22��$��22��$��0,0,0,0q�$
��left=����� $ HasBooks�8   $	 TwoWay=��� $ -2��$ -7��$��	ExtraBold"�
�������$��10(����� $ CurrentBagID�$	 TwoWay=��� $
 StudentName��$
��Left=�$��Arial)�$��15(�$��CharacterEllipsis=�$��Center=�$	��150��$��50������ $ FullName�$	 OneTime=��� $��Right=�
������ $��0q�8�� y��� $ WeekOverdue�8  .���� $��/res/Exclamation.png��$��18��$��18��$; 5Student has a book checked out for over a weeks time.��.��� -    �Name$ 
LayoutRoot��
������� � ColumnDefinitions �� $��* � � RowDefinitions �� $��36 ��� $��* �.+��� $��0��$��2q�
���iii�._��� $ 
horizontal=�
�������.+���   CircleButton#��[ ���� *_AddNewStudentCommand+.���� $��/res/new_plus.png��$	��Top=�$��20��$��20��$��20��$��20��$��0,0,0,0q�$
��left=��� #��[ ���� *_RemoveStudentCommand+.���� $��/res/delete_x.png��$	��Top=�$��20��$��20��$��0,0,0,0q�$
��left=��� -    ��Name$ StudentsStackPanel�� J� 	AllowDrop . $��2��$��2��$
��Auto��#��[ 
�������$	��Top=����� *
Students+ �� SelectedItem �� $ SelectedStudent�$	 TwoWay=�����  �� 
TargetType" ��.���  �� Property � �� �� Value$ Italic$���  � ��$ 14(���  � ��$ 26��