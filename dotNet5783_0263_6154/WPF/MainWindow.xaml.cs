
using System;
using System.Windows;
using BlApi;
using PL.Product;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl myBl = new BlImplementation.Bl();

        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Redirects to the list of products window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToProductsListButton_Click(object sender, RoutedEventArgs e) => new ProductForListWIndow(myBl).Show();
    }

    
}
