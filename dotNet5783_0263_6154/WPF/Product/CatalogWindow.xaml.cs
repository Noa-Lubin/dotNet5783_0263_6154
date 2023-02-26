using PL.Cart;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF;

namespace PL.Product
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl _myBl = BlApi.Factory.Get();
        BO.Cart _myCart = new BO.Cart();
        public ObservableCollection<BO.ProductItem?> _productsItemList
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(_productsItemListProperty); }
            set { SetValue(_productsItemListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for _productsItemList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty _productsItemListProperty =
            DependencyProperty.Register("_productsItemList", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));



        public CatalogWindow(BlApi.IBl bl)
        {
            InitializeComponent();
            _myBl = bl;
            var temp = _myBl!.Product.GetCatalog();
            _productsItemList = temp == null ? new() : new(temp);
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));//Filter products by category
            CategorySelector.SelectedIndex = 7;//initialization to none
            _myCart!.CustomerName = "";
            _myCart.CustomerEmail = "";
            _myCart.CustomerAdress = "";
            _myCart.TotalPrice = 0;
            _myCart.Items = null;

        }

    

        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Filter products by category
            CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.Category));
            //Puts the selected filter value into a variable
            BO.Enums.Category categorySelect = (BO.Enums.Category)CategorySelector.SelectedItem;
            if ((BO.Enums.Category)(CategorySelector.SelectedItem) == BO.Enums.Category.none)
            {
                var temp = _myBl!.Product.GetCatalog();
                _productsItemList = temp == null ? new() : new(temp);
            }
            else
            {
                var temp = _myBl.Product.GetCatalog(x => x?.Category == (DO.Enums.Category)(categorySelect));
                _productsItemList = temp == null ? new() : new(temp);
            }
        }

        private void ProductItemsListview_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var x = ((BO.ProductItem)((System.Windows.Controls.ListBox)sender).SelectedItem);
            new ProductItemWindow(x.IdProduct, _myCart).Show();
            this.Close();
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            new CartDisplayWindow(_myCart).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int id = ((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).IdProduct;
            if (((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).InStock == false)
                MessageBox.Show("אזל מהמלאי",
                    "Sold Out",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            else
            {
                try
                {
                    _myCart = _myBl.Cart.AddAndUpdate(id, _myCart, ((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).Amount);

                }
                catch
                {
                    MessageBox.Show("המוצר לא נוסף לסל");
                }
            }
        }

        private void btnPlus_Click(object sender, RoutedEventArgs e)
        {
            _myBl.Cart.UpdateAmountOfProduct(((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).IdProduct, _myCart, 1);
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox text = sender as TextBox;
            if (text == null) return;
            if (e == null) return;
            //allow get out of the text box
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                return;
            //allow list of system keys (add other key here if you want to allow)
            if (e.Key == Key.Escape || e.Key == Key.Back || e.Key == Key.Delete ||
            e.Key == Key.CapsLock || e.Key == Key.LeftShift || e.Key == Key.Home
            || e.Key == Key.End || e.Key == Key.Insert || e.Key == Key.Down || e.Key == Key.Right ||
            e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || 
            e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || 
            e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                return;
            char c = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            //allow control system keys
            if (Char.IsControl(c)) return;
            //allow digits (without Shift or Alt)
            if (Char.IsDigit(c))
                if (!(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightAlt)))
                    return; //let this key be written inside the textbox
                            //forbid letters and signs (#,$, %, ...)
            e.Handled = true; //ignore this key. mark event as handled, will not be routed to other 
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
           new MainWindow().Show();
            this.Close();
        }
    }
}
