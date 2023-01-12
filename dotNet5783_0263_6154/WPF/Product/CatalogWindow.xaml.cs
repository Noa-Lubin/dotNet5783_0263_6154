using BO;
using DO;
using PL.Cart;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using PL.Product;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        BO.Cart _myCart = new BO.Cart();

        public ObservableCollection<BO.ProductItem?> _productsItemList
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(_productsItemListProperty); }
            set { SetValue(_productsItemListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _productsItemList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _productsItemListProperty =
            DependencyProperty.Register("_productsItemList", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));


        public CatalogWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            _myBl = bl;
            var temp = _myBl!.Product.GetCatalog();
            _productsItemList = temp == null ? new() : new(temp);
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//Filter products by category
            CategorySelector.SelectedIndex = 7;//initialization to none
            _myCart!.CustomerName = "aaa";
            _myCart.CustomerEmail = "@";
            _myCart.CustomerAdress = "aaa";
            _myCart.TotalPrice = 0;
            _myCart.Items = null;
        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Filter products by category
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            //Puts the selected filter value into a variable
            BO.Enums.Category categorySelect = (BO.Enums.Category)CategorySelector.SelectedItem;
            if ((BO.Enums.Category)(CategorySelector.SelectedItem) == BO.Enums.Category.none)
            {
                var temp = _myBl!.Product.GetCatalog();
                _productsItemList = temp == null ? new() : new(temp);
            }
            else
            {
                var temp = _myBl.Product.GetCatalog(x => x?.Category == (DO.Enums.Category)(categorySelect));
                _productsItemList = temp == null ? new() : new(temp);
            }
        }

        private void ProductItemsListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((BO.ProductItem)((System.Windows.Controls.ListBox)sender).SelectedItem).IdProduct;
            new ProductItemWindow(id, _myCart).ShowDialog();

        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            new CartDisplayWindow(_myCart).ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int id = ((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).IdProduct;
            if (((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).InStock == false)
                MessageBox.Show("אזל מהמלאי");
            else
            {
                try
                {
                    _myCart = _myBl.Cart.AddProductToCart(id, _myCart);

                }
                catch
                {

                }
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
