   M S B A M L   `   `   `  ����H  DwatkiCzytanie, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:watkiCzytaniewatkiCzytanie  5   	       watkiCzytanie.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:watkiCzytanie     5   	     � Title$  Wątki w czytaniu plików��5   	   $	��446��6)   $	��444��66   .��5	      � $��0,0,4,6q�6   .+�5
   
   ��  �� Orientation$ Vertical=�6   $��10q�6,   .+�5      �� $ 
Horizontal=�6   $��10q�62   $��Center=�6>   .+�5      �� -   5        $
��txt1��5      $��10q�6'   $	��120��63   $	��180��6@   �� 5      -   5        $
��txt2��5      $��10q�6'   $	��120��63   $	��180��6@   5      � 5      -   5         �Name$ progressBar1��5      $��15��60   $��10q�6<    � Minimum$ 0Q�6H    � Maximum$	 100Q�6T   �� 5      $ 
Horizontal=�6   $��10q�62   $��Center=�6>   .+�5      �� -   5        $
��btn1��5      $��Czytnie pliku 1��6%   $��10q�6?   $	��120��6^   �� 5      -   5        $��
btn1_pause��5      $��Pause��6+   $��10q�6P   $	��120��6\   �� 5      -   5        $��	btn1_stop��5      $
��Stop��6*   $��10q�6M   $	��120��6Y   5      � 5      -   5        $ progressBar2��5      $��15��60   $��10q�6<   $ 0Q�6H   $	 100Q�6T   �� 5      $ 
Horizontal=�6   $��10q�62   $��Center=�6>   .+�5      �� -   5        $
��btn2��5      $��Czytnie pliku 2��6%   $��10q�6?   $	��120��6^   �� 5      -	   5        $��
btn2_pause��5      $��Pause��6+   $��10q�6P   $	��120��6\   �� 5      -
   5        $��	btn2_stop��5      $
��Stop��6*   $��10q�6M   $	��120��6Y   5      5      5      5      