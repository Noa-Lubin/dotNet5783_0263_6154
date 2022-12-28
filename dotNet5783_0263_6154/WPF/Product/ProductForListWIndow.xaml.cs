using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPF.Product;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductForListWIndow.xaml
    /// </summary>
    public partial class ProductForListWIndow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        private IEnumerable<BO.ProductForList?> _productsForList;
        private IEnumerable<BO.ProductForList?> productsByCategory;

        /// <summary>
        /// Ctor - initializes the display of the list of all products
        /// </summary>
        /// <param name="bl"></param>
        public ProductForListWIndow(BlApi.IBl bl)
        {
            InitializeComponent();
            _myBl = bl;

            _productsForList = _myBl.Product.GetAllProducts();//Inserts the list of all products into a variable 
            ProductsListview.ItemsSource = _productsForList; //Displays the returned list
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//Filter products by category
            CategorySelector.SelectedIndex = 7;//initialization to none
            productsByCategory = _myBl.Product.GetAllProducts();//Initialize
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
                productsByCategory = _myBl.Product.GetAllProducts();
            else
            { //Calls the appropriate function and sends a delegate that it selects by category
                productsByCategory = _myBl.Product.GetAllProducts(x => x?.Category == (DO.Enums.Category)(categorySelect));
            }           
            //Displaying the list of returned products
            ProductsListview.ItemsSource = productsByCategory;
        }
        

        /// <summary>
        /// Redirects to the add product window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            _productsForList = _myBl.Product.GetAllProducts(); //Inserts the list of all products into a variable 
            ProductsListview.ItemsSource = _productsForList; //Displays the returned list
        }


        /// <summary>
        /// This button cancels the filtering by category and displays all products 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clear_Click(object sender, RoutedEventArgs e)
        {
            _productsForList = _myBl.Product.GetAllProducts() ;//Inserts the list of all products into a variable 
            ProductsListview.ItemsSource = _productsForList; //Displays the returned list
        }


        /// <summary>
        /// open an update window for a specific product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int id = ((BO.ProductForList)((System.Windows.Controls.ListView)sender).SelectedItem).IdProduct;
            new ProductWindow(id).ShowDialog();
            _productsForList = _myBl.Product.GetAllProducts() ;//Inserts the list of all products into a variable 
            ProductsListview.ItemsSource = _productsForList; //Displays the returned list
        }
        
    }
    }
