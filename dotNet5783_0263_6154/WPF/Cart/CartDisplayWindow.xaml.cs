using PL.Order;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            if (_myCart.Items == null || _myCart.Items.Count() == 0)
            {
                btnCheckOut.Visibility = Visibility.Hidden;
                lblEmpty.Visibility = Visibility.Visible;
            }
            var temp = _myCart.Items;
            _ItemsInCart = temp == null ? new() : new(temp);
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            var myItem = (BO.OrderItem)((Button)sender).DataContext;
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
            if (_myCart.Items== null || _myCart.Items.Count() == 0)
                MessageBox.Show("הסל ריק");
            else
            {
                new DetailsCustomerWindow(_myCart).ShowDialog();
            }
        }
    }
}



