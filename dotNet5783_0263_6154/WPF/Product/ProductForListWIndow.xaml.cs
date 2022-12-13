using BlApi;
using BlImplementation;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private IBl _myBl = new BlImplementation.Bl();
        private IBl _bl = new BlImplementation.Bl();
        private IEnumerable<BO.ProductForList> _productsForList;
        private IEnumerable<BO.ProductForList> productsByCategory;

        /// <summary>
        /// Ctor - initializes the display of the list of all products
        /// </summary>
        /// <param name="bl"></param>
        public ProductForListWIndow(IBl bl)
        {
            InitializeComponent();
            _myBl = bl;

            _productsForList = _myBl.Product.GetAllProducts();//Inserts the list of all products into a variable 
            ProductsListview.ItemsSource = _productsForList; //Displays the returned list
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//Filter products by category

        }

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Filter products by category
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            //Puts the selected filter value into a variable
            Enums.Category categorySelect = (Enums.Category)CategorySelector.SelectedItem;
            //Calls the appropriate function and sends a delegate that it selects by category
            productsByCategory = _myBl.Product.GetAllProducts(c => c.Value.Category == categorySelect);
            //Displaying the list of returned products
            ProductsListview.ItemsSource = productsByCategory;
        }

        /// <summary>
        /// Redirects to the add product window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProduct_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            _productsForList = _myBl.Product.GetAllProducts();//Inserts the list of all products into a variable 
            ProductsListview.ItemsSource = _productsForList; //Displays the returned list
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e) => new ProductWindow().Show();
    }
}
