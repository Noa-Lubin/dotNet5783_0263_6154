using System;
using System.Linq;
using System.Windows;

namespace PL.Product;



/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    readonly BlApi.IBl _myBl = BlApi.Factory.Get();

    public BO.Product ProdCurrent
    {
        get { return (BO.Product)GetValue(ProdCurrentProperty); }
        set { SetValue(ProdCurrentProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ProdCurrent.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProdCurrentProperty =
        DependencyProperty.Register("ProdCurrent", typeof(BO.Product), typeof(ProductWindow), new PropertyMetadata(null));

    public int Id
    {
        get { return (int)GetValue(IdProperty); }
        set { SetValue(IdProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Id.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty IdProperty =
        DependencyProperty.Register("Id", typeof(int), typeof(ProductWindow), new PropertyMetadata(0));

    public Array? Categories { get; set; }

    /// <summary>
    /// ctor with parameter for update
    /// </summary>
    /// <param name="idProduct"></param>
    public ProductWindow(int idProduct = 0)
    {
        Categories = Enum.GetValues(typeof(BO.Enums.Category));
         ProdCurrent = idProduct == 0 ? new() : _myBl!.Product.GetProduct(idProduct);
        Id = idProduct;
        InitializeComponent();   
    }

    /// <summary>
    /// Enters the values ​​that the user has typed and creates a new order and adds to the list of products in the data layer
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnAddOrUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (Id == 0) //if add
        {
            if (txtId.Text == "" || txtName.Text == "" || Convert.ToInt32(txtPrice.Text) == 0 || cmbCategory.SelectedItem == null) //checking if all data is full
                MessageBox.Show("חסר נתונים");
            else if (Convert.ToInt32(txtId.Text) <= 99999 || Convert.ToInt32(txtId.Text) > 999999) //checing if ID is correct
                MessageBox.Show("מזהה מוצר שהוקש לא תקין");
            else if (cmbCategory.SelectedIndex == 7) //checking if the selected category is not none
                MessageBox.Show("לא נבחרה קטגוריה למוצר");
            else
            {
                bool succeed = true;
                try
                {
                    _myBl.Product.AddProduct(ProdCurrent);
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
        else//if update
        {
            if (cmbCategory.SelectedIndex == 7)
                MessageBox.Show("לא נבחרה קטגוריה");
            else
            {                
                _myBl.Product.UpdateProduct(ProdCurrent!);
                MessageBox.Show("מוצר התעדכן בהצלחה");
                Close();             
            }
        }
    }

    /// <summary>
    /// for close this window if the user regreted
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void btnCancel_Click(object sender, RoutedEventArgs e)
    {
        new ProductForListWIndow(_myBl!).Show();
       this.Close();
    }



}



