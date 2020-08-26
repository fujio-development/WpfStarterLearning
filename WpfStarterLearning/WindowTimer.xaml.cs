using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Permissions;
using System.Windows.Threading;

namespace WpfStarterLearning
{
    /// <summary>WindowTimer.xaml の相互作用ロジック</summary>
    /// <remarks></remarks>
    public partial class WindowTimer : Window
    {
        private System.Windows.Threading.DispatcherTimer DispaTmr = new DispatcherTimer();
        private System.Timers.Timer TimersTmr = new System.Timers.Timer();
        private System.Threading.Timer ThreadingTmr;
        private int TimerCnt;
        private bool DoEventsType = true;
        public WindowTimer()
        {
            InitializeComponent();
        }

        private void WindowTimer1_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.NoResize;
            this.Title = "タイマー、メッセージキュー、スレッド関連";
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDispaTimer_Click(object sender, RoutedEventArgs e)
        {
            //DispatcherTimerセットアップ
            DispaTmr.Tick += new EventHandler(DispatcherTimer_Tick);
            DispaTmr.Interval = new TimeSpan(0, 0, 0, 1, 0);
            TimerCnt = 0;
            DispaTmr.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            //DispatcherTimerのイベントハンドラー内はUIスレッドで動作するので時間のかかる処理を行うとその間は他のイベントハンドラーは受け付けない。

            //System.Windows.Threading.DispatcherTimer.Tickハンドラ
            //現在の秒表示を更新しますCommandManagerで
            //InvalidateRequerySuggestedを強制的に使用します。
            //CanExecuteChangedイベントを発生させるCommand。

            TimerCnt += 1;
            //現在の秒を表示するラベルを更新する
            LabelTime.Content = "現在時刻 ：" + String.Format(DateTime.Now.ToString("HH:mm:ss"));
            LabelCount.Content = "秒カウント：" + TimerCnt;
            //LabelTime.Content = timec;

            //CommandManagerにRequerySuggestedイベントを発生させる
            CommandManager.InvalidateRequerySuggested();
        }

        private void ButtonDispaStop_Click(object sender, RoutedEventArgs e)
        {
            DispaTmr.Stop();
        }

        private void ButtonTimersTimer_Click(object sender, RoutedEventArgs e)
        {
            TimersTmr.Elapsed += new System.Timers.ElapsedEventHandler(TimersTimer_Tick);
            //TimersTmr.SynchronizingObject = this;
            TimersTmr.Interval = 1000;
            //TimersTmr.AutoReset = true;
            //TimersTmr.Enabled = true;
            TimerCnt = 0;
            TimersTmr.Start();
        }

        private void TimersTimer_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            //Timers.Timerのイベントハンドラーは別スレッドで実行されるので直接UIスレッドのコントロールにアクセスできない。(スレッドプールで実行される)
            //Invoke、BeginInvoke、EndInvoke
            Application.Current.Dispatcher.Invoke(() =>
            {
                TimerCnt += 1;
                LabelTime.Content = "現在時刻 ：" + String.Format(DateTime.Now.ToString("HH:mm:ss"));
                LabelCount.Content = "秒カウント：" + TimerCnt;
            });
        }

        private void ButtonTimersStop_Click(object sender, RoutedEventArgs e)
        {
            TimersTmr.Stop();
        }

        private void ButtonThreadingTimer_Click(object sender, RoutedEventArgs e)
        {
            //Threading.Timerのイベントハンドラーは別スレッドで実行されるので直接UIスレッドのコントロールにアクセスできない。(スレッドプールで実行される)
            System.Threading.TimerCallback ThreadingCallback = (state) => { object o = null; ThreadingTimer(o); };
            TimerCnt = 0;
            ThreadingTmr = new System.Threading.Timer(ThreadingCallback, null, 0, 1000);
        }

        private void ThreadingTimer(object sender)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TimerCnt += 1;
                LabelTime.Content = "現在時刻 ：" + String.Format(DateTime.Now.ToString("HH:mm:ss"));
                LabelCount.Content = "秒カウント：" + TimerCnt;
            });
        }

        private void ButtonThreadingStop_Click(object sender, RoutedEventArgs e)
        {
            ThreadingTmr.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        /// <summary>現在メッセージ待ち行列の中にある全てのUIメッセージを処理します。
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private void DoEvents()
        {
            DispatcherFrame Frame = new DispatcherFrame();
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new DispatcherOperationCallback(ExitFrame), Frame);
            Dispatcher.PushFrame(Frame);

        }

        public object ExitFrame(object f)
        {
            ((System.Windows.Threading.DispatcherFrame)f).Continue = false;

            return null;
        }

        private void DoEvents2()
        {
            //Application.Current.Dispatcher.Invoke(new Action(() >= {}), DispatcherPriority.Background);
        }

        private void ButtonHeavy_Click(object sender, RoutedEventArgs e)
        {
            var ts = System.Threading.Tasks.Task.Run(() =>
            {
                System.Threading.Thread.Sleep(5000);
                DoEvents();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    //別スレッドからUI操作
                    ButtonHeavy.Content = "別スレッド終了";
                });
            });

            //Awaitを入れるとこれ以降の処理は止まる(プロシージャーにAsync修飾子が必要)。しかし、UIは受け付ける。
            //Await Task.WhenAll(ts)
            ButtonHeavy.Content = "メイン終了";
        }

        private async void ButtonHeavyHi_Click(object sender, RoutedEventArgs e)
        {
            await Hidouki();
        }

        private async System.Threading.Tasks.Task Hidouki()
        {
            await System.Threading.Tasks.Task.Delay(5000);
        }

        private void ButtonMessageQueue_Click(object sender, RoutedEventArgs e)
        {
            if (DoEventsType)
            {
                DoEvents();
            }
            else
            {
                DoEvents2();
            }
        }
    }
}