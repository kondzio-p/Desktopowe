   M S B A M L   `   `   `  ����B  >WpfApp8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp8WpfApp8  5   	       WpfApp8.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp8     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � .+�5
   
   ��  �� Orientation$ Vertical=�6   $��Center=�6,   $
��Left=�6G   $	��359��6b   .+�5      �� $��Opcje dźwięku��6   �� 5      -   5        $��
btnWczytaj��5      $��Wczytaj dźwięk��6'   $	��150��5      $��30��6!   $��20q�6-   69   �� 5      -   5        $��btnPlay��5      $
��Play��6$   $	��150��5      $��30��6!   $��20q�6-   69   �� 5      -   5        $��btnPause��5      $��Pause��6%   $	��150��5      $��30��6!   $��20q�6-   69   �� 5      -   5        $��btnStop��5      $
��Stop��6$   $	��150��5      $��30��6!   $��20q�6-   69   5      r� 5   
   -   5         r�Name$ video��5       r� LoadedBehavior$ Manual=�6&    r� UnloadedBehavior$
 Stop=�5       r� Stretch$ Uniform=�6   $	��200��6-   $	��200��69   �� 5   
   $ 
Horizontal=�6   $��Bottom=�6.   .+�5      �� -   5         ��Name$
 btnW��5      $��Wczytaj��6#   $	��150��5      $��30��6   $��20q�6%   61   �� 5      -   5        $ 
btnPlayVid��5      $
��Play��6)   $	��150��5       $��30��6   $��20q�6%   61   �� 5"      $��Opcje filmu��6   �� 5$      -   5        $ btnPauseVid��5$      $��Pause��6*   $	��150��5%      $��30��6   $��20q�6%   61   �� 5&      -	   5        $ 
btnStopVid��5&      $
��Stop��6)   $	��150��5'      $��30��6   $��20q�6%   61   5(      5)      5*      