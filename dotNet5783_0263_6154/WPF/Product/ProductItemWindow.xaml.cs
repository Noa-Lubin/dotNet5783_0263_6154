using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for ProductItemWindow.xaml
    /// </summary>
    public partial class ProductItemWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();


        public BO.ProductItem CurrentProductItem
        {
            get { return (BO.ProductItem)GetValue(CurrentProductItemProperty); }
            set { SetValue(CurrentProductItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentProductItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProductItemProperty =
            DependencyProperty.Register("CurrentProductItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));


        public ProductItemWindow(int idProduct, BO.Cart c)
        {
            InitializeComponent();
            //cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            //Imports the desired product and brings all its data into the textBox
            BO.ProductItem p = _myBl.Product.GetProductForList(idProduct, c);
            //txtId.Text = Convert.ToString(p?.IdProduct);
            //txtId.IsEnabled = false;
            //txtName.Text = p?.Name;
            //txtPrice.Text = Convert.ToString(p?.Price);
            //txtInStock.Text = Convert.ToString(p?.InStock);
            //BO.OrderItem? prod = c.Items!.FirstOrDefault(x => x?.IdProduct == idProduct);
            //txtAmountInCart.Text = Convert.ToString(prod?.AmountInCart);
            //cmbCategory.SelectedItem = p?.Category;
            CurrentProductItem = p;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
