   M S B A M L   `   `   `  ����I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Converters.ViewsConverters.Views  5      'clr-namespace:Converters
Converters  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5	      "    Converters.Views.StudentView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      0viewsclr-namespace:Converters.Views     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .	converterclr-namespace:Converters     5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase     5	        � 	Resources  5      )   #Converters.HasBooksToImageConverter   HasBooksToImageConverter,  &Converters.ShowingInfoToImageConverter  ShowingInfoToImageConverter3  -Converters.BoolToVisibilityCollapsedConverter & "BoolToVisibilityCollapsedConverter  StudentItemTemplate �� Orientation  NoChromeButton � ToolTip �� RelativeSource �� AncestorType �� Left
 �� Top �� Path �� 	Converter	 �� Mode
 ��Name��%�  &	        &	      &	 &     &	 9     7   7 �7    5   
   6-     5   
   60     5   
   67   �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� #���  6   $$ Click on the book to check in.��6�   $��0,0,0,0q�6�   $��34��6  $��36��6  ��6D   �� * DataContext._BookButtonPressed+ �� $ ListBoxL���6�   �� .��5      �� $��0,0,0,0q�6   .+�5      �� $��	BookImage��6    $ -5��6�   $	 -15��6�   $��Center=�6�   $��22��6�   $��22��6�   $��0,0,0,0q�6�   $
��left=�6�   ��61   �� $ HasBooks�8   $	 TwoWay=�6�   �� 5      $ -2��6T   $	 -10��6e   $��	ExtraBold"�5      
�������$��12(�6H   ��5   $   �� $ CurrentBagID�$	 TwoWay=�5   H   5      5      �� 5      $
 StudentName��6   $
��Left=�6^   $��Arial)�6y   $��15(�6�   $��CharacterEllipsis=�6�   $��Center=�6�   $	��150��6�   $��50��6�   ��61   �� $ FullName�$	 OneTime=�6�   �� 5      $��Right=�6   
������ $��0q�6�   8�� y�67   �� $ WeekOverdue�8  .��5      �� $��/res/Exclamation.png��6   $��18��6:   $��18��6E   $; 5Student has a book checked out for over a weeks time.��6Q   6�   5      5      5      5       .��5!   
   � -   5         �Name$ 
LayoutRoot��5!      
������� � ColumnDefinitions 5"   
   �� 5#      $��* �6    � RowDefinitions 5%   
   �� 5&      $��36 �6   �� 5'      $��* �6   .+�5*   
   �� $��0��6   $��2q�6   
���iii�._�5+      �� $ 
horizontal=�6   
�������.+�5-      ��   CircleButton#��[ 6@   ��6   �� *_AddNewStudentCommand+.��5.      �� $��/res/new_plus.png��6   $	��Top=�6/   $��20��6H   $��20��6S   $��20��6`   $��20��6n   $��0,0,0,0q�6}   $
��left=�6�   5/      �� 50      #��[ 6   ��6;   �� *_RemoveStudentCommand+.��52      �� $��/res/delete_x.png��6   $	��Top=�67   $��20��6P   $��20��6[   $��0,0,0,0q�6g   $
��left=�6x   53      54      55      �� 56   
   -   5         ��Name$ StudentsStackPanel��56       J� 	AllowDrop . $��2��57   3   $��2��6@   $
��Auto��6U   #��[ 58      
�������$	��Top=�59   N   ��57      �� *
Students+ �� SelectedItem 59      �� $ SelectedStudent�$	 TwoWay=���5:      �� 5;       �� 
TargetType" ��6   .�5=      ��  �� Property � �� �� Value$ Italic$�62   �� 5>       � ��$ 14(�61   �� 5?       � ��$ 26��62   5@      5B      5M      5N      