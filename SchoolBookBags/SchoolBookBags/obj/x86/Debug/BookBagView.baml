   M S B A M L   `   `   `  ����I  ESchoolBookBags, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Converters
Converters  5      "    Converters.Views.BookBagView   5      MtoolkitAhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit  5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      .	converterclr-namespace:Converters     5        � 	Resources  5      $   Converters.BookStatusConverter   BookStatusConverter*  $Converters.BoolToVisibilityConverter  BoolToVisibilityConverter  BagListStyle �� 
TargetType �� Property �� Value �� Path �� 	Converter  BooksWrapPanel  oneBookTemplate �� ElementName �� Left
 �� Top��%  &	        &	      &	 &     7  &	 �     &	       5   
   6(     5   
   6.   �� 5      " ��6"   .�5      ��  � �� 5      �� 5      " ��6   .�5      ��  � y� 67   �� $ IsAvailable�8   5      5      5      �� 5   
   BooksWrapPanel��.R�5      � 5      �� 5   
   .R�5      �� $��canvas1��6Y   $��1��6   $��2��6%   $��60��62   $
��Left=�6>   $	��Top=�6h   $��60��6�   $��10q�6�   .+�5      �� $��2q�5    >   ��5      �� $ canvas1��$ ActualHeight���6S   �� $ canvas1��$ ActualWidth���5       �� $ ImageStatus�6>   �� 5!      $ 18��6   $ 24��6-   
�������$��SemiBold"�6p   $��18(�6�   ��6=   �� $ ID�6�   5#      5$      5&      .��5)      � -   5        	 �Name$	 
LayoutRoot��5)      .+�5*   
   �� -   5        $��bookList��5*   J   $��1��6   $��2��6"   $
��Left=�6/   $	��Top=�6Z   $
��Auto��6r   $
��Auto��6�   #��[ 5+      #��[ 6@   $��Disabled=�5-      #��[ 5.   8     BorderBG#��[ 6^   ��5+   p   �� *AvailableBookBags+
 �� SelectedItem
 5,      �� $ SelectedBookBag� �� Mode$ TwoWay=�50      51      52      