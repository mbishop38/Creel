   M S B A M L   `   `   `  ���� I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Converters
Converters  "    Converters.Views.BookBagView   MtoolkitAhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .	converterclr-namespace:Converters       � 	Resources  $   Converters.BookStatusConverter   BookStatusConverter*  $Converters.BoolToVisibilityConverter  BoolToVisibilityConverter  BagListStyle �� 
TargetType �� Property �� Value �� Path �� 	Converter  BooksWrapPanel  oneBookTemplate �� ElementName �� Left
 �� Top��%�  &	        &	      &	 
     7  &	 b     &	 �         �� " ��.���  � �� �� " ��.���  � y� �� $ IsAvailable�8   �� BooksWrapPanel��.R�� �� .R��� $��canvas1��$��1��$��2��$��60��$
��Left=�$	��Top=�$��60��$��10q�.+��� $��2q����� $ canvas1��$ ActualHeight����� $ canvas1��$ ActualWidth����� $ ImageStatus��� $ 18��$ 24��
�������$��SemiBold"�$��18(����� $ ID�.��� -   	 �Name$	 
LayoutRoot��.+��� -   $��bookList��$��1��$��2��$
��Left=�$	��Top=�$
��Auto��$
��Auto��#��[ #��[ $��Disabled=�#��[   BorderBG#��[ ���� *AvailableBookBags+
 �� SelectedItem
 �� $ SelectedBookBag� �� Mode$ TwoWay=�