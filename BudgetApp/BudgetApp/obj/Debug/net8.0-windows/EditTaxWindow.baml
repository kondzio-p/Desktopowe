   M S B A M L   `   `   `  ����  	BudgetApp    BudgetApp.EditTaxWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=5.2.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Edytuj Podatek��5   	   $	��300��6    $	��400��6-   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ TaxName��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Nazwa podatku��5      $��0��5      $
��0 10q�6   �� 5   
   -   5        $ 	TaxAmount��5      $ Kwota��5      $��1��5      $
��0 10q�6   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ 
TaxDueDate��5      $ Termin płatności��5      $��2��5      $
��0 10q�6"   �� 5   
   -   5        $��Zapisz��5      $��3��5      $��Right=�6   $
��0 10q�6:   5      5      5      