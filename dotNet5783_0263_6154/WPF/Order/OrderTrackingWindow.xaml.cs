using System;
using System.Windows;
using WPF;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    public partial class OrderTrackingWindow : Window
    {

        BlApi.IBl _myBl = BlApi.Factory.Get();

        public BO.OrderTracking TrackingCurrent
        {
            get { return (BO.OrderTracking)GetValue(TrackingCurrentProperty); }
            set { SetValue(TrackingCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrackingCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackingCurrentProperty =
            DependencyProperty.Register("TrackingCurrent", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));

        public OrderTrackingWindow(int id)
        {
            InitializeComponent();
            TrackingCurrent = _myBl?.Order.OrderOfTracking(Convert.ToInt32(id)) ?? throw new Exception("aaaaaa");
        }

        private void btnToOrder_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtIdOrder.Text);
            new OrderWindow(id, false).ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
