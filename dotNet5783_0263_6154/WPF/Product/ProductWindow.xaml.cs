using BO;
using PL.Product;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml.Linq;
namespace WPF.Product
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        //private IBl _myBl = new BlImplementation.Bl();
        BlApi.IBl? _myBl = BlApi.Factory.Get();

        private string state = "";


        /// <summary>
        /// ctor for add
        /// </summary>
        public ProductWindow()
        {
            InitializeComponent();
            state = "add";
            btnAddOrUpdate.Content = state;
            lblTitle.Content = state;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            cmbCategory.SelectedIndex = 7;
            lblTitle.Content = "הוספת מוצר";
            lblIncorrectId.Visibility = Visibility.Hidden;
            lblIncorrectName.Visibility = Visibility.Hidden;

        }


        /// <summary>
        /// ctor with parameter for update
        /// </summary>
        /// <param name="idProduct"></param>
        public ProductWindow(int idProduct)
        {
            InitializeComponent();
            state = "update";
            //btnDelete.Visibility = Visibility.Hidden;//not now
            lblIncorrectName.Visibility = Visibility.Hidden; //not now
            btnAddOrUpdate.Content = state; //id add or update
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            //Imports the desired product and brings all its data into the textBox
            BO.Product p = _myBl.Product.GetProduct(idProduct);
            txtId.Text = Convert.ToString(p?.ID);
            txtId.IsEnabled = false;
            txtName.Text = p?.Name;
            txtPrice.Text = Convert.ToString(p?.Price);
            txtInStock.Text = Convert.ToString(p?.InStock);
            cmbCategory.SelectedItem = p?.Category;
            lblTitle.Content = "עדכון מוצר"; //for the title
            lblIncorrectId.Visibility = Visibility.Hidden; //if the ID is incorrect so put a warrning
        }


        /// <summary>
        /// Enters the values ​​that the user has typed and creates a new order and adds to the list of products in the data layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (state == "add") //if add
            { 
                if (txtId.Text == "" || txtName.Text == "" || Convert.ToInt32(txtPrice.Text) == 0 || Convert.ToInt32(txtInStock.Text) == 0 || cmbCategory.SelectedItem == null) //checking if all data is full
                    MessageBox.Show("חסר נתונים");
                else if (Convert.ToInt32(txtId.Text) <= 99999 || Convert.ToInt32(txtId.Text) > 999999) //checing if ID is correct
                    MessageBox.Show("מזהה מוצר שהוקש לא תקין");
                else if (cmbCategory.SelectedIndex == 7) //checking if the selected category is not none
                    MessageBox.Show("לא נבחרה קטגוריה למוצר");

                else
                {
                    bool succeed = true;
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
                    try
                    {
                        _myBl?.Product.AddProduct(p);
                        MessageBox.Show("מוצר נוסף בהצלחה");

                    }
                    catch
                    {
                        MessageBox.Show("הוספת מוצר נכשלה");
                        succeed = false;
                    }
                    if (succeed) // if product is added
                        Close();
                }

            }
            else if (state == "update")//if update
            {
                if (cmbCategory.SelectedIndex == 7)
                    MessageBox.Show("לא נבחרה קטגוריה");
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
                    _myBl?.Product.UpdateProduct(p);
                    MessageBox.Show("מוצר התעדכן בהצלחה");
                    Close();
                }
                
            }
        }

        //delete product
        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    _myBl.Product.DeleteProduct(Convert.ToInt32(txtId.Text));
        //    MessageBox.Show("מוצר נמחק בהצלחה");
        //    Close();
        //}

        /// <summary>
        /// for close this window if the user regreted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        /// <summary>
        /// warning if ID with any letter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtId_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (txtId.Text.Any(x => char.IsLetter(x))) lblIncorrectId.Visibility = Visibility.Visible;
        }

        private void txtName_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if (txtName.Text.Any(x => char.IsNumber(x))) lblIncorrectName.Visibility = Visibility.Visible;
        }
    }
}
