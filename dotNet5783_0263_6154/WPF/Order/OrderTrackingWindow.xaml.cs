﻿using BO;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.Product;

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


        public OrderTrackingWindow()
        {
            InitializeComponent();
            bool succeed = false;
            string input = Interaction.InputBox("הקש מספר הזמנה למעקב", "", "", 10, 10);            
            while (!succeed)
            {
                try
                {
                    _myBl.Order.GetOrder(Convert.ToInt32(input));
                    TrackingCurrent = _myBl?.Order.OrderOfTracking(Convert.ToInt32(input)) ?? throw new Exception("aaaaaa");
                    succeed = true;
                }
                catch
                {
                    MessageBox.Show("הזמנה לא קיימת");
                    input = Interaction.InputBox("הקש מספר הזמנה למעקב", "", "", 10, 10);
                }
            }

        }


        private void btnToOrder_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt32(txtIdOrder.Text);
            new OrderWindow(id, false).ShowDialog();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
