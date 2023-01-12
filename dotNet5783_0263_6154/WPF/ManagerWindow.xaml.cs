using System;
using System.Collections.Generic;
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
using PL;
using PL.Order;
using PL.Product;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        private BlApi.IBl? myBl = BlApi.Factory.Get();

        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void ToProductsListButton_Click(object sender, RoutedEventArgs e) => new ProductForListWIndow(myBl!).Show();

        private void ToOrdersListButton_Click(object sender, RoutedEventArgs e) => new OrderForListWindow(myBl!).Show();

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
