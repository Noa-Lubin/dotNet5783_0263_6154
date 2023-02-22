using System.Collections.ObjectModel;
using System.Windows;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderForListWindow.xaml
    /// </summary>
    public partial class OrderForListWindow : Window
    {

        BlApi.IBl _myBl = BlApi.Factory.Get();

        public ObservableCollection<BO.OrderForList?> _ordersForList
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(_ordersForListProperty); }
            set { SetValue(_ordersForListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _ordersForList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _ordersForListProperty =
            DependencyProperty.Register("_ordersForList", typeof(ObservableCollection<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));

        public OrderForListWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            _myBl = bl;
            var temp = _myBl!.Order.GetAllOrders();
            _ordersForList = temp == null ? new() : new(temp);         
        }

        private void OrderssListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((BO.OrderForList)((System.Windows.Controls.ListBox)sender).SelectedItem).IdOrder;
            new OrderWindow(id).ShowDialog();
            var temp = _myBl!.Order.GetAllOrders();
            _ordersForList = temp == null ? new() : new(temp);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
            this.Close();
        }
    }
}
