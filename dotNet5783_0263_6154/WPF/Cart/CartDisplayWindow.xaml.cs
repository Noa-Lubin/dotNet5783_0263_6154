using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace PL.Cart
{
    /// <summary>
    /// Interaction logic for CartDisplayWindow.xaml
    /// </summary>
    public partial class CartDisplayWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        BO.Cart _myCart = new BO.Cart();

        public ObservableCollection<BO.OrderItem?> _ItemsInCart
        {
            get { return (ObservableCollection<BO.OrderItem?>)GetValue(_ItemsInCartProperty); }
            set { SetValue(_ItemsInCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _ItemsInCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _ItemsInCartProperty =
            DependencyProperty.Register("_ItemsInCart", typeof(ObservableCollection<BO.OrderItem?>), typeof(Window), new PropertyMetadata(null));


        public CartDisplayWindow(BO.Cart c)
        {
            InitializeComponent();
            _myCart = c;
            //CurrentCart = _myCart;
            //_ItemsInCart = c.Items!.Select(i => i);
            var temp = _myCart.Items;
            _ItemsInCart = temp == null ? new() : new(temp);
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            //  int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemID;
            var myItem = ((BO.OrderItem)((Button)sender).DataContext);
            try
            {
                _myCart = _myBl.Cart.UpdateAmountOfProduct(myItem!.IdProduct, _myCart, myItem!.AmountInCart + 1);
                var temp = _myCart.Items;
                _ItemsInCart = temp == null ? new() : new(temp);
            }
            catch
            {
                MessageBox.Show("חסר במלאי");
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
             var myItem = (BO.OrderItem)((Button)sender).DataContext;
            _myCart = _myBl.Cart.UpdateAmountOfProduct(myItem!.IdProduct, _myCart, myItem!.AmountInCart - 1);
            var temp = _myCart.Items;
            _ItemsInCart = temp == null ? new() : new(temp);

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var myItem = (BO.OrderItem)((Button)sender).DataContext;
            _myCart = _myBl.Cart.UpdateAmountOfProduct(myItem!.IdProduct, _myCart, 0);
            var temp = _myCart.Items;
            _ItemsInCart = temp == null ? new() : new(temp);
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
