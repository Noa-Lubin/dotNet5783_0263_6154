using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for SimulationWindow.xaml
    /// </summary>
    public partial class SimulationWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        BackgroundWorker updateStatus;
        bool flag = true;
        DateTime fakeTime = DateTime.Now;

        public List<BO.OrderForList?> SimulationOrders
        {
            get { return (List<BO.OrderForList?>)GetValue(SimulationOrdersProperty); }
            set { SetValue(SimulationOrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SimulationOrders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SimulationOrdersProperty =
            DependencyProperty.Register("SimulationOrders", typeof(List<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));


        public SimulationWindow()
        {
            InitializeComponent();
            SimulationOrders = new(_myBl.Order.GetAllOrders());

            updateStatus = new BackgroundWorker();
            updateStatus.DoWork += DoWork;
            updateStatus.ProgressChanged += ProgressChanged;
            updateStatus.RunWorkerCompleted += RunWorkerCompleted;
            updateStatus.WorkerReportsProgress = true;
            updateStatus.WorkerSupportsCancellation = true; 
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (updateStatus.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    fakeTime = fakeTime.AddHours(3);
                    if (updateStatus.WorkerReportsProgress == true)
                    {
                        updateStatus.ReportProgress(11117);
                    }
                }
                Thread.Sleep(2000);
            }
        }
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Random rn = new Random();
            List<BO.OrderForList?> temp = _myBl.Order.GetAllOrders().ToList();
            foreach (var item in SimulationOrders)
            {
                BO.Order order = _myBl.Order.GetOrder(item?.IdOrder ?? throw new NullReferenceException());
                if (fakeTime - order.OrderDate >= new TimeSpan(3, 0, 0, 0) && order.Status == BO.Enums.OrderStatus.approved)
                    _myBl.Order.ShippingUpdate(order.ID);
                if (fakeTime - order.OrderDate >= new TimeSpan(3, 0, 0, 0) && order.Status == BO.Enums.OrderStatus.sent)
                    _myBl.Order.OrderDeliveryUpdate(order.ID);
                SimulationOrders = _myBl.Order.GetAllOrders().ToList();

            }
            //לזמן את הפונקציה שמביאה את ההזמנה הישנה ביותר ולעדכן אותה
        }
        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (flag == true)
            {
                MessageBox.Show("finish😍");
            }
            else if (e.Cancelled == true)
            {
                MessageBox.Show("cancled");
            }
            this.Cursor = Cursors.Arrow;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (updateStatus.IsBusy != true)
                // Start the asynchronous operation. 
                updateStatus.RunWorkerAsync();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            if (updateStatus.WorkerSupportsCancellation == true)
                // Cancel the asynchronous operation.
                updateStatus.CancelAsync();
        }
        private void ToTracking_Click(object sender, RoutedEventArgs e)
        {
            string oId = ((sender as Button).Tag).ToString();
            int id = Convert.ToInt32(oId);
            MessageBox.Show(_myBl.Order.OrderOfTracking(id).ToString() + "📦");
        }
    }
}
