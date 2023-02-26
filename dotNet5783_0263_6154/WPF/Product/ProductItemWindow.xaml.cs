using System.Windows;


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
            BO.ProductItem p = _myBl.Product.GetProductForList(idProduct, c);
            CurrentProductItem = p;
        }
  
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }
    }
}
