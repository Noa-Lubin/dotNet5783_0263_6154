using System.Windows;

namespace PL.Order
{
    /// <summary>
    /// Interaction logic for DetailsCustomerWindow.xaml
    /// </summary>
    public partial class DetailsCustomerWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        public BO.Cart _myCart
        {
            get { return (BO.Cart)GetValue(_myCartProperty); }
            set { SetValue(_myCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _myCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _myCartProperty =
            DependencyProperty.Register("_myCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));



        public DetailsCustomerWindow(BO.Cart cart)
        {
            InitializeComponent();
            _myCart = cart;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            //בדיקות שהקלט מהמשתמש תקיןן
            if (txtMail.Text == "" || txtId.Text == "" || txtAddress.Text == "")
                MessageBox.Show("חסר פרטים, אנא הקש פרטי משתמש",
                    "InCorrect",
                    MessageBoxButton.OK,
MessageBoxImage.Warning);
            else if (!txtMail.Text.Contains('@'))
                MessageBox.Show("מייל שהוקש שגוי",
                    "InCorrect",
                    MessageBoxButton.OK,
MessageBoxImage.Warning);
            else
            {
                try
                {
                    int id = _myBl.Cart.MakeOrder(_myCart, _myCart.CustomerName, _myCart.CustomerEmail, _myCart.CustomerAdress);
                    new FinishOrderWindow(id).ShowDialog();
                }
                catch
                {
                    MessageBox.Show("ההזמנה לא הושלמה, נסה שוב");
                }
            }
        }
    }
}
