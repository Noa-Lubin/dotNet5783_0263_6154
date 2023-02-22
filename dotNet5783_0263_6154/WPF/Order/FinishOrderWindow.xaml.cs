using System.Windows;
using WPF;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for FinishOrderWindow.xaml
    /// </summary>
    public partial class FinishOrderWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();


        public int idOrder
        {
            get { return (int)GetValue(idOrderProperty); }
            set { SetValue(idOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for idOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty idOrderProperty =
            DependencyProperty.Register("idOrder", typeof(int), typeof(Window), new PropertyMetadata(0));


        public FinishOrderWindow(int id)
        {
            InitializeComponent();
            idOrder = id;
        }

        private void btnToTracking_Click(object sender, RoutedEventArgs e)
        {
                   new OrderTrackingWindow(idOrder).Show();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new MainWindow().ShowDialog();
        }
    }
}
