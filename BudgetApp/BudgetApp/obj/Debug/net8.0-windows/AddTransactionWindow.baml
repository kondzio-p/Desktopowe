   M S B A M L   `   `   `  ����  	BudgetApp$    BudgetApp.AddTransactionWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Dodaj Transakcję��5   	   $	��300��6"   $	��400��6/   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ TransactionName��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Nazwa transakcji��5      $��0��5      $
��0 10q�6   �� 5   
   -   5        $ TransactionAmount��5      $ Kwota��5      $��1��5      $
��0 10q�6   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ TransactionDate��5      $ Data transakcji��5      $��2��5      $
��0 10q�6"   �� 5   
   -   5        $��Dodaj��5      $��3��5      $��Right=�6   $
��0 10q�6:   5      5      5      