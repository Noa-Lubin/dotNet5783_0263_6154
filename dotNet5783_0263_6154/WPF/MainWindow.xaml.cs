
using System;
using System.Windows;
using Microsoft.VisualBasic;
using PL;
using PL.Order;
using PL.Product;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BlApi.IBl? myBl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Redirects to the list of products window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>


        private void ToCatalog_Click(object sender, RoutedEventArgs e) => new CatalogWindow(myBl!).Show();

        private void ToTracking_Click(object sender, RoutedEventArgs e)
        {
            bool succeed = false;
            string input = Interaction.InputBox("הקש מספר הזמנה למעקב", "", "", 10, 10);
            while (!succeed && input != "")
            {
                try
                {
                    myBl!.Order.GetOrder(Convert.ToInt32(input));
                    succeed = true;
                    new OrderTrackingWindow(Convert.ToInt32( input)).Show();
                }
                catch
                {
                    MessageBox.Show("הזמנה לא קיימת");
                    input = Interaction.InputBox("הקש מספר הזמנה למעקב", "", "", 10, 10);
                }
            }
        }

        private void Manager_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();
    }

    
}
