   M S B A M L   `   `   `  ���� M  IWpfStarterLearning, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:WpfStarterLearningWpfStarterLearning  $    WpfStarterLearning.WindowTimer   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:WpfStarterLearning     -     �Name$  WindowTimer1�� � Title$ WindowTimer��$	��450��$	��600��.���  � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* � � RowDefinitions �� $��2* ��� $��* �.+��� -    ��Name$ 	LabelTime��$��時刻表示��$
��Left=�$��
30,157,0,0q�$	��Top=�$	��166��$��36��$��2��$��1�����	#FFB8D4E4
���  ��$��16(�$
��Bold"��� -   $ 
LabelCount��$��秒カウント表示��$
��Left=�$��
30,202,0,0q�$	��Top=�$	��166��$��36��$��2��$��1�����	#FFB8D4E4
���  ��$��16(�$
��Bold"��� -    ��Name$ ButtonDispaTimer��$!��DispatcherTimerスタート��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��128��$��53��$��1���� -   $ ButtonDispaStop��$!��DispatcherTimerストップ��$
��Left=�$��	10,81,0,0q�$	��Top=�$	��128��$��49��$��1���� -   $ ButtonTimersTimer��$��Timers.Timerスタート��$��1��$
��Left=�$��	10,10,0,0q�$��1��$	��Top=�$	��128��$��53���� -   $ ButtonTimersStop��$��Timers.Timerストップ��$��1��$
��Left=�$��	10,81,0,0q�$��1��$	��Top=�$	��128��$��49���� -   $ ButtonThreadingTimer��$!��Threading.Timerスタート��$��2��$
��Left=�$��	10,10,0,0q�$��1��$	��Top=�$	��128��$��53���� -	   $ ButtonThreadingStop��$!��Threading.Timerストップ��$��2��$
��Left=�$��	10,81,0,0q�$��1��$	��Top=�$	��128��$��49���� -
   $ ButtonMessageQueue��$*��$メッセージキュー取り出し��$
��Left=�$��	10,81,0,0q�$	��Top=�$	��128��$��49��$��3��$��1���� -   $ ButtonHeavy��$��重い��$��3��$
��Left=�$��	10,10,0,0q�$��1��$	��Top=�$��57��$��53���� -   $ ButtonHeavyHi��$��重い2��$��3��$
��Left=�$��	83,10,0,0q�$��1��$	��Top=�$��55��$��53���� -   $ 
ButtonExit��$��	閉じる��$
��Left=�$��
10,183,0,0q�$	��Top=�$	��120��$��71��$��3��