using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace PL.Product;


//public class IntToStringConverter : IValueConverter
//{
//    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        // new NotImplementedException();
//        int val = (int)value;
//        return val == 0 ? "Add" : "Update";
//    }

//    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//    {
//        throw new NotImplementedException();
//    }
//}


/// <summary>
/// Interaction logic for ProductWindow.xaml
/// </summary>
public partial class ProductWindow : Window
{
    readonly BlApi.IBl _myBl = BlApi.Factory.Get();
    //private string state = "";

    //public static readonly DependencyProperty DataDep = DependencyProperty.Register(nameof(Data), typeof(ProductWindowData), typeof(ProductWindow));
    //public ProductWindowData Data { get => (ProductWindowData)GetValue(DataDep); set => SetValue(DataDep, value); }

    // Using a DependencyProperty as the backing store for products.  This enables animation, styling, binding, etc...
    //public static readonly DependencyProperty ProdCurrentProperty =
    //    DependencyProperty.Register("ProdCurrent", typeof(BO.Product), typeof(ProductWindow),new PropertyMetadata(null));
    //public BO.Product? ProdCurrent
    //{
    //    get => (BO.Product?)GetValue(ProdCurrentProperty);
    //    set => SetValue(ProdCurrentProperty, value);
    //}



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

    //public string? ButtonMode { get; set; }

   // public bool isReadOnlyID { get; set; }

    /* public BO.Product ProdCurrent
     {
         get { return (BO.Product)GetValue(ProdCurrentProperty); }
         set { SetValue(ProdCurrentProperty, value); }
     }
     // Using a DependencyProperty as the backing store for ProdCurrent.  This enables animation, styling, binding, etc...
     public static readonly DependencyProperty ProdCurrentProperty =
         DependencyProperty.Register("ProdCurrent", typeof(BO.Product), typeof(ProductWindow), new PropertyMetadata(null));


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
        ProdCurrent = new BO.Product();
    }*/

    /// <summary>
    /// ctor with parameter for update
    /// </summary>
    /// <param name="idProduct"></param>
    public ProductWindow(int idProduct = 0)
    {

        /*        state = "update";
                btnAddOrUpdate.Content = state; //id add or update
                //btnDelete.Visibility = Visibility.Hidden;//not now
                lblIncorrectName.Visibility = Visibility.Hidden; //not now
                cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
                ProdCurrent = _myBl.Product.GetProduct(idProduct);
                txtId.IsEnabled = false;
                lblTitle.Content = "עדכון מוצר"; //for the title
                lblIncorrectId.Visibility = Visibility.Hidden; //if the ID is incorrect so put a warrning
        */
        //Data = new()
        //{
        //    isReadOnlyID = idProduct == 0 ? false : true,
        //ProdCurrent = idProduct == 0 ? new() : _myBl?.Product.GetProduct(idProduct);
        Categories = Enum.GetValues(typeof(BO.Enums.Category));
        //    ButtonMode = idProduct == 0 ? "ADD" : "UPDATE"
        //};+		InnerException	{"Cannot find resource named 'IntToStringConverter'. Resource names are case sensitive."}	System.Exception
         ProdCurrent = idProduct == 0 ? new() : _myBl!.Product.GetProduct(idProduct);

        // state = idProduct == 0 ? "add" : "update";
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



