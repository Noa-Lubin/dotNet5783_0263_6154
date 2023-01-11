using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
namespace PL.Order
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
     
        public BO.Order OrderCurrent
        {
            get { return (BO.Order)GetValue(OrderCurrentProperty); }
            set { SetValue(OrderCurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrderCurrent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderCurrentProperty =
            DependencyProperty.Register("OrderCurrent", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));


        public OrderWindow(int idOrder, bool visible = true)
        {
            InitializeComponent();
            if (!visible)
            {
                btnUpdate.Visibility = Visibility.Hidden;
                btnUpdateStatus.Visibility = Visibility.Hidden;
            }
            lblIncorrectName.Visibility = Visibility.Hidden; //not now
            cmbStatus.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
            OrderCurrent = _myBl.Order.GetOrder(idOrder);
            txtId.IsEnabled = false;
        }
        /// <summary>
        /// Enters the values ​​that the user has typed and creates a new order and adds to the list of products in the data layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //if (Convert.ToDateTime(txtShipDate.Text) != default)
            //{
            //    //הודעה מתאימה שההזמנה כבר נשלחה
            //    MessageBox.Show("ההזמנה נשלחה, אין אפשרות לעדכן");
            //    Close();
            //}
            //else
            //{
                _myBl.Order.UpdateOrder(OrderCurrent);
                MessageBox.Show("הזמנה התעדכנה בהצלחה");
                Close();
            //}
        }

        //delete product
        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    _myBl.Product.DeleteProduct(Convert.ToInt32(txtId.Text));
        //    MessageBox.Show("מוצר נמחק בהצלחה");
        //    Close();
        //}


        /// <summary>
        /// for close this window if the user regreted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnUpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            if (cmbStatus.SelectedIndex == 0)//approved
            {
                _myBl.Order.ShippingUpdate(Convert.ToInt32(txtId.Text));
                MessageBox.Show("תאריך שילוח התעדכן בהצלחה");
                Close();
            }
            else if (cmbStatus.SelectedIndex == 1)//sent
            {
                _myBl.Order.OrderDeliveryUpdate(Convert.ToInt32(txtId.Text));
                MessageBox.Show("תאריך אספקה התעדכן בהצלחה");
                Close();
            }
            else //provided
            {
                MessageBox.Show("ההזמנה סופקה ללקוח");
                Close();
            }
        }

      
    }
}
