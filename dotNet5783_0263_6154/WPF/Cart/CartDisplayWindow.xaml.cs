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
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemID;
            BO.OrderItem _myOrderItem = _myCart.Items?.FirstOrDefault(x => x?.OrderItemID == id) ?? throw new Exception("Your cart is empty");/*?????*/
            BO.Product p = _myBl.Product.GetProduct(_myOrderItem.IdProduct);
            if (p.InStock < 1)
                MessageBox.Show("חסר במלאי"); 
            else
                _myBl.Cart.UpdateAmountOfProduct(_myOrderItem!.IdProduct, _myCart, _myOrderItem!.AmountInCart + 1);
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemID;
            BO.OrderItem _myOrderItem = _myCart.Items?.FirstOrDefault(x => x?.OrderItemID == id) ?? throw new Exception("Your cart is empty");/*?????*/
            _myBl.Cart.UpdateAmountOfProduct(_myOrderItem!.IdProduct, _myCart, _myOrderItem!.AmountInCart - 1);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemID;
            BO.OrderItem _myOrderItem = _myCart.Items?.FirstOrDefault(x => x?.OrderItemID == id) ?? throw new Exception("Your cart is empty");/*?????*/
            _myBl.Cart.UpdateAmountOfProduct(_myOrderItem!.IdProduct, _myCart, 0);
            
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
