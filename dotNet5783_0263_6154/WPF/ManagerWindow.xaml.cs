using System.Windows;
using PL.Order;
using PL.Product;
using WPF;

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

        private void ToProductsListButton_Click(object sender, RoutedEventArgs e)
        {
            new ProductForListWIndow(myBl!).Show();
            this.Close();
        }

        private void ToOrdersListButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderForListWindow(myBl!).Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
