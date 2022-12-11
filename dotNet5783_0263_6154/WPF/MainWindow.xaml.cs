
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
        static IBl myBl = new BlImplementation.Bl();

        public MainWindow()
        {
            //InitializeComponent();
        }

        //private void InitializeComponent()
        //{
        //    throw new NotImplementedException();
        //}
        private void ToProductsListButton_Click(object sender, RoutedEventArgs e) => new ProductForListWIndow(myBl).Show();
    }

    
}
