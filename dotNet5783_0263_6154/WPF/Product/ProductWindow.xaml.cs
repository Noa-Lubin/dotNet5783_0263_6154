using BlApi;
using BO;
using PL.Product;
using System;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace WPF.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl _myBl = new BlImplementation.Bl();
        private string state="";
        public ProductWindow()
        {
            InitializeComponent();
            state = "add";
            btnAddOrUpdate.Content = state;
            lblTitle.Content = state;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            
                lblTitle.Content = "הוספת מוצר";
            lblIncorrectId.Visibility = Visibility.Hidden;
            lblIncorrectName.Visibility = Visibility.Hidden;

        }

        public ProductWindow(int idProduct)
        {
            InitializeComponent();
            state = "update";
            btnAddOrUpdate.Content = state;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            //btnAdd.IsEnabled = false;
            BO.Product p = _myBl.Product.GetProduct(idProduct);
            txtId.Text = Convert.ToString(p?.ID);
            txtId.IsEnabled = false;
            txtName.Text = p?.Name;
            txtPrice.Text = Convert.ToString(p.Price);
            txtInStock.Text = Convert.ToString(p.InStock);
            cmbCategory.SelectedItem = p.Category;
                lblTitle.Content = "עדכון מוצר";
            lblIncorrectId.Visibility = Visibility.Hidden;

        }

        /// <summary>

        /// Enters the values ​​that the user has typed and creates a new order and adds to the list of products in the data layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
           if(state =="add")
            {
                if (txtId.Text == "" || txtName.Text == "" || txtPrice.Text == "" || txtInStock.Text == "" ||cmbCategory.SelectedItem==null)
                    MessageBox.Show("חסר נתונים");
                 if (Convert.ToInt32(txtId.Text) < 100000 || Convert.ToInt32(txtId.Text) > 999999)
                        MessageBox.Show("מזהה מוצר שהוקש לא תקין");
                else { 
                    //create object of Product by the values of the user
                    BO.Product p = new BO.Product()
                    {
                        ID = Convert.ToInt32(txtId.Text),
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
                
            }
            else if(state == "update")
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
        /// <summary>
        ///  creates a new order and update this product in the data layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        //{
            
        //}

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _myBl.Product.DeleteProduct(Convert.ToInt32(txtId.Text));
            MessageBox.Show("מוצר נמחק בהצלחה");
            Close();

        }

        //private void txtId_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        //{
        //    if(state == "update")
        //        txtId.IsEnabled = false;

        //}

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtId_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //if (txtId.Text.Contains('a') == true)
            //    lblIncorrectId.Visibility = Visibility.Visible;

            if(txtId.Text.Any(x => char.IsLetter(x))) lblIncorrectId.Visibility = Visibility.Visible;
           

        }

        private void txtName_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (txtName.Text.Any(x => char.IsNumber(x))) lblIncorrectName.Visibility = Visibility.Visible;

        }
    }
}
//if (txtId.Text != "" || txtName.Text != "" || txtPrice.Text != "" || txtInStock.Text != "")
//    btnAddOrUpdate.IsEnabled = false;