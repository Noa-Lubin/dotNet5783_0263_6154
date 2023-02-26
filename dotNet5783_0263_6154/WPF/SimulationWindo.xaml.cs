using Simulator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulationWindo.xaml
    /// </summary>
    public partial class SimulationWindo : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        BackgroundWorker updateStatus;
        bool flag = true;

        public BO.Order OrderCurrent
        {
            get { return (BO.Order)GetValue(OrderCurrentProperty); }
            set { SetValue(OrderCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderCurrentProperty =
            DependencyProperty.Register("OrderCurrent", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

  
        public SimulationWindo()
        {
            InitializeComponent();
            updateStatus = new BackgroundWorker();
            updateStatus.DoWork += DoWork;
            updateStatus.ProgressChanged += ProgressChanged;
            updateStatus.RunWorkerCompleted += RunWorkerCompleted;
            updateStatus.WorkerReportsProgress = true;
            updateStatus.WorkerSupportsCancellation = true;
            updateStatus.RunWorkerAsync();

            //watch
            DispatcherTimer timer = new DispatcherTimer();
            timer.Start();

        
        }

        private void start(object sender, myOrder e)
        {
            updateStatus.ReportProgress(1, e);
        }

        private void stop(object sender, EventArgs e)
        {
            //updateStatus.WorkerSupportsCancellation = false;
            updateStatus.CancelAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.ReportStart(start);
            Simulator.Simulator.ReportEnd(stop);

            Simulator.Simulator.Activate();
            while (true)
            {
                if (updateStatus.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    if (updateStatus.WorkerReportsProgress == true)
                    {
                        updateStatus.ReportProgress(0);
                    }
                }
                Thread.Sleep(1000);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //שעוןן
            //פקדיםם
            //מקבל ארוע שקשור חעדכון הפקדים 

            if (e.ProgressPercentage == 0)
            {
                timer.Text = DateTime.Now.ToString("h:mm:ss tt");
            }
            else
            {
                OrderCurrent = ((myOrder)e.UserState).order;
                int delay = ((myOrder)e.UserState).second;
                if (OrderCurrent.Status==BO.Enums.OrderStatus.approved)
                    statusAfter.Text= BO.Enums.OrderStatus.sent.ToString();
                else if(OrderCurrent.Status == BO.Enums.OrderStatus.sent)
                    statusAfter.Text = BO.Enums.OrderStatus.provided.ToString();
                TimeStart.Text = timer.Text;
                TimeEnd.Text= DateTime.Now.AddSeconds(delay).ToString("h:mm:ss tt");
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Simulator.Simulator.NotReportStart(start);
            Simulator.Simulator.NotReportEnd(stop);

            if (flag == true)
            {
                MessageBox.Show("finish😍");
            }
            else if (e.Cancelled == true)
            {
                MessageBox.Show("cancled");
            }
            this.Cursor = Cursors.Arrow;
            this.Close();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.Deactive();
        }
    }
}
