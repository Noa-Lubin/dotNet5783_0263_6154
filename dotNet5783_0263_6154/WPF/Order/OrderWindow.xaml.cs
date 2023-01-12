using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using PL.Product;

namespace PL.Order
{

    public class OrderWindowData : DependencyObject
    {
        // Using a DependencyProperty as the backing store for .  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderCurrentProperty =
            DependencyProperty.Register("OrderCurrent", typeof(BO.Order), typeof(OrderWindowData));
        public BO.Order? OrderCurrent
        {
            get => (BO.Order?)GetValue(OrderCurrentProperty);
            set => SetValue(OrderCurrentProperty, value);
        }

        public Array? Status { get; set; }

        public Visibility IsVisible { get; set; }

    }

    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        readonly BlApi.IBl _myBl = BlApi.Factory.Get();

        public static readonly DependencyProperty DataDep = DependencyProperty.Register(nameof(Data), typeof(OrderWindowData), typeof(OrderWindow));
        public OrderWindowData Data { get => (OrderWindowData)GetValue(DataDep); set => SetValue(DataDep, value); }

        public OrderWindow(int idOrder, bool visible = true)
        {
            Data = new()
            {
                IsVisible = visible == true?Visibility.Visible : Visibility.Hidden,
                OrderCurrent = _myBl?.Order.GetOrder(idOrder),
                Status = Enum.GetValues(typeof(BO.Enums.OrderStatus)),
            };
            InitializeComponent();
            //lblIncorrectName.Visibility = Visibility.Hidden; //not now
            //cmbStatus.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
        }
        /// <summary>
        /// Enters the values ​​that the user has typed and creates a new order and adds to the list of products in the data layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
                _myBl.Order.UpdateOrder(Data.OrderCurrent!);
                MessageBox.Show("הזמנה התעדכנה בהצלחה");
                Close();
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
