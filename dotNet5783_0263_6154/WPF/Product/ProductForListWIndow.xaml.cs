﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductForListWIndow.xaml
    /// </summary>
    public partial class ProductForListWIndow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        public ObservableCollection<BO.ProductForList?> _productsForList
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(_productsForListProperty); }
            set { SetValue(_productsForListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _productsForList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _productsForListProperty =
            DependencyProperty.Register("_productsForList", typeof(ObservableCollection<BO.ProductForList?>), typeof(Window), new PropertyMetadata(null));

        /// <summary>
        /// Ctor - initializes the display of the list of all products
        /// </summary>
        /// <param name="bl"></param>
        public ProductForListWIndow(BlApi.IBl bl)
        {
            InitializeComponent();
            _myBl = bl;

            var temp = bl!.Product.GetAllProducts();
            _productsForList = temp == null ? new() : new(temp);
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//Filter products by category
            CategorySelector.SelectedIndex = 7;//initialization to none
        }

        /// <summary>
        /// Redirects to the add product window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().Show();
            this.Close();
            var temp = _myBl!.Product.GetAllProducts();
            _productsForList = temp == null ? new() : new(temp); //Inserts the list of all products into a variable 
        }


        /// <summary>
        /// open an update window for a specific product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((BO.ProductForList)((System.Windows.Controls.ListBox)sender).SelectedItem).IdProduct;
            new ProductWindow(id).ShowDialog();
            var temp = _myBl!.Product.GetAllProducts();
            _productsForList = temp == null ? new() : new(temp);
        }

        /// <summary>
        /// filter by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Filter products by category
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            //Puts the selected filter value into a variable
            BO.Enums.Category categorySelect = (BO.Enums.Category)CategorySelector.SelectedItem;
            if ((BO.Enums.Category)(CategorySelector.SelectedItem) == BO.Enums.Category.none)
            {
                var temp = _myBl!.Product.GetAllProducts();
                _productsForList = temp == null ? new() : new(temp);
            }
            else
            { //Calls the appropriate function and sends a delegate that it selects by category
                var temp = _myBl.Product.GetAllProducts(x => x?.Category == (DO.Enums.Category)(categorySelect));
                _productsForList = temp == null ? new() : new(temp);
            }
        }

        private void btnPopular_Click(object sender, RoutedEventArgs e)
        {
            var temp = _myBl!.Product.PopularItems();
            _productsForList = temp == null ? new() : new(temp);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
