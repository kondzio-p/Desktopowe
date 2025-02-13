using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace watkiCzytanie
{
    public partial class MainWindow : Window
    {
        private BackgroundWorker worker1;
        private BackgroundWorker worker2;
        private ManualResetEventSlim pauseEvent1 = new ManualResetEventSlim(true);
        private ManualResetEventSlim pauseEvent2 = new ManualResetEventSlim(true);
        private CancellationTokenSource cts1 = new CancellationTokenSource();
        private CancellationTokenSource cts2 = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
            InitializeWorkers();
        }

        private void InitializeWorkers()
        {
            worker1 = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker1.DoWork += Worker1_DoWork;
            worker1.ProgressChanged += Worker1_ProgressChanged;

            worker2 = new BackgroundWorker { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker2.DoWork += Worker2_DoWork;
            worker2.ProgressChanged += Worker2_ProgressChanged;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (!worker1.IsBusy)
            {
                cts1 = new CancellationTokenSource();
                progressBar1.Value = 0;
                worker1.RunWorkerAsync(cts1.Token);
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (!worker2.IsBusy)
            {
                cts2 = new CancellationTokenSource();
                progressBar2.Value = 0;
                worker2.RunWorkerAsync(cts2.Token);
            }
        }

        private void Pause1_click(object sender, RoutedEventArgs e)
        {
            if (pauseEvent1.IsSet)
                pauseEvent1.Reset();
            else
                pauseEvent1.Set();
        }

        private void Pause2_click(object sender, RoutedEventArgs e)
        {
            if (pauseEvent2.IsSet)
                pauseEvent2.Reset();
            else
                pauseEvent2.Set();
        }

        private void Stop1_click(object sender, RoutedEventArgs e)
        {
            cts1.Cancel();
        }

        private void Stop2_click(object sender, RoutedEventArgs e)
        {
            cts2.Cancel();
        }

        private void Worker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Zmień ścieżkę do pliku
            ReadFile("C:\\Users\\t4\\Documents\\kody\\Desktopowe\\zapisodczyt\\duzyplik1.txt", txt1, progressBar1, pauseEvent1, (CancellationToken)e.Argument, worker1);
        }

        private void Worker2_DoWork(object sender, DoWorkEventArgs e)
        {
            // Zmień ścieżkę do pliku
            ReadFile("C:\\Users\\t4\\Documents\\kody\\Desktopowe\\zapisodczyt\\duzyplik1.txt", txt2, progressBar2, pauseEvent2, (CancellationToken)e.Argument, worker2);
        }

        private void ReadFile(string filePath, ListView listView, ProgressBar progressBar, ManualResetEventSlim pauseEvent, CancellationToken cancellationToken, BackgroundWorker worker)
        {
            if (!File.Exists(filePath)) return;

            var lines = File.ReadLines(filePath).ToList();
            int totalLines = lines.Count;
            int currentLine = 0;

            foreach (var line in lines)
            {
                pauseEvent.Wait(cancellationToken);
                if (cancellationToken.IsCancellationRequested) break;

                worker.ReportProgress((int)((double)currentLine / totalLines * 100), line);

                currentLine++;
                Thread.Sleep(2);
            }
        }

        private void Worker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            txt1.Items.Add(e.UserState as string);
        }

        private void Worker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar2.Value = e.ProgressPercentage;
            txt2.Items.Add(e.UserState as string);
        }
    }
}
