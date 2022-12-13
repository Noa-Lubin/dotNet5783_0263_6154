using BlApi;
using BO;
using PL.Product;
using System;
using System.Windows;
namespace WPF.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl _myBl = new BlImplementation.Bl();

        public ProductWindow()
        {
            InitializeComponent();
            cmbCategory.ItemsSource= Enum.GetValues(typeof(BO.Enums.Category));
            btnUpdateProduct.IsEnabled = false;

        }

        public ProductWindow(int idProduct)
        {
            InitializeComponent();
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            btnAdd.IsEnabled = false;
        }

        /// <summary>
        /// Enters the values ​​that the user has typed and creates a new order and adds to the list of products in the data layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //create object of Product by the values of the user
            BO.Product p = new BO.Product()
            {
                ID =Convert.ToInt32( txtId.Text),
                Price = Convert.ToDouble(txtPrice.Text),
                InStock = Convert.ToInt32(txtInStock.Text),
                Name = txtName.Text,
                Category = (BO.Enums.Category)(cmbCategory.SelectedItem)
            };
            // add a product to the list of products in the data layer
            
                _myBl.Product.AddProduct(p);
            MessageBox.Show("מוצר נוסף בהצלחה");
            Close();
        }
        /// <summary>
        ///  creates a new order and update this product in the data layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            //create object of Product by the values of the user
            BO.Product p = new BO.Product()
            {
                ID = Convert.ToInt32(txtId.Text),
                Price = Convert.ToDouble(txtPrice.Text),
                InStock = Convert.ToInt32(txtInStock.Text),
                Name = txtName.Text,
                Category = (BO.Enums.Category)(cmbCategory.SelectedItem)
            };
            _myBl.Product.UpdateProduct(p);
            MessageBox.Show("מוצר התעדכן בהצלחה");
            Close();
        }
    }
}
