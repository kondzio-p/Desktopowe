   M S B A M L   `   `   `  ����C  ?WpfApp10, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:WpfApp10WpfApp10  5   	       WpfApp10.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:WpfApp10     5   	     � Title$<  6Wprowadzanie danych do paszportu wykonał: 00000000000��5   	   $	��291��6G   $	��642��6T   .��5	      � 
�����_�.+�5
   
   ��  �� Orientation$ 
Horizontal=�6   $��5q�6.   .+�5      �� $ Vertical=�6   $��5q�60   .+�5      �� $ 
Horizontal=�6   $��5q�66   .+�5      �� $ Vertical=�6!   $��5q�68   .+�5      �� $��Numer��6    $	��120��60   6>   �� 5      $��Imię��6    $	��120��6/   6=   �� 5      $��Nazwisko��6    $	��120��63   6A   5      �� 5      $ Vertical=�6!   $��5q�68   .+�5      �� -   5         ��Name$ txtNumer��5   "   $��5q�64   $�� ��6?   $	��120��6G   
�������5   V   �� 5      -   5        $ txtImie��5   "   $��5q�63   $�� ��6>   $	��120��6F   
�������6g   �� 5      -   5        $ txtNazwisko��5   "   $��5q�67   $�� ��6B   $	��120��6J   
�������6k   5      5      �� 5      $��
Kolor oczu��6   $��5q�6/   .��5      �� $ Vertical=�6!   $��5q�68   .+�5      	� -   5         	�Name$ rdNiebieskie��5   &   $��
niebieskie��6<    d� 	IsChecked$
 TrueY�6Q    	� 	GroupName$ kolory��6b   	� 5      -   5        $ 	rdZielone��5   &   $��zielone��69   $ kolory��6K   	� 5      -   5        $ rdPiwne��5   &   $��piwne��67   $ kolory��6G   5      5      5       �� 5!      $ Vertical=�6   $��5q�60   .+�5"      �� $ 
Horizontal=�6   $��5q�66   .+�5#      �� $��1q�6   
���   �$	��140��6E   $	��180��6Q   $��5q�6^   ._�5$      �� -   5         ��Name$ 
imgZdjecie��5$       $	��180��64   $	��140��6A   $��5q�6M   $
��Fill=�6X   5%      �� 5&      $��1q�6   
���   �$	��110��6E   $	��180��6Q   $��5q�6^   ._�5'      �� -   5        $ 	imgOdcisk��5'       $	��180��63   $	��110��6@   $��5q�6L   $
��Fill=�6W   5(      5)      �� 5*      -	   5         ��Name$ chckProporcje��5*      $��Uniform��62   $��5q�6D   $ FalseY�6O   6�   �� 5+      -
   5        $ chckPrzezroczystosc��5+      $��Ustaw przeźroczystość��68   $��5q�6X   $
 TrueY�6c   6�   �� 5-      -   5         ��Name$ btnOK��5-      $��OK��6(   $	��160��65   $��30��6A   $��5q�6M   
�������6k   5.      5/      50      51      