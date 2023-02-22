using PL.Cart;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            new ProductItemWindow(x.IdProduct, _myCart).ShowDialog();
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            new CartDisplayWindow(_myCart).ShowDialog();
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
    }
}




//קןבץ זאמל של הקטלוג מטלי ודבי
//< Window x: Class = "PL.openWindow"

//    xmlns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation"

//    xmlns: x = "http://schemas.microsoft.com/winfx/2006/xaml"

//    xmlns: d = "http://schemas.microsoft.com/expression/blend/2008"

//    xmlns: mc = "http://schemas.openxmlformats.org/markup-compatibility/2006"

//    xmlns: local = "clr-namespace:PL"

//    mc: Ignorable = "d"

//   WindowStartupLocation = "CenterScreen"

//    WindowStyle = "None" ResizeMode = "NoResize"

//    AllowsTransparency = "True" Background = "Transparent"

//    DataContext = "{Binding RelativeSource={RelativeSource self}}"

//  Title = "openWindow" Height = "700" Width = "1300" >

//  < Window.Resources >

//    < Style TargetType = "Label" >

//      < Style.Triggers >

//        < Trigger Property = "IsMouseOver" Value = "True" >

//          < Setter Property = "Foreground" Value = "#FFD62899" />

//        </ Trigger >

//      </ Style.Triggers >

//    </ Style >

//    < DataTemplate x: Key = "OneProductItemTemplate" DataType = "local:ProductItem" >

//      < Border

//        Background = "{Binding InStock, Mode=TwoWay, NotifyOnValidationError=false, ValidatesOnExceptions=true, Converter={StaticResource BooleanToColor}}" >

//        < Border.Effect >

//          < DropShadowEffect

//              ShadowDepth = "1" />

//        </ Border.Effect >

//        < Grid HorizontalAlignment = "Center" >

//          < Grid.RowDefinitions >

//            < RowDefinition Height = "*" />

//            < RowDefinition Height = "0.5*" />

//            < RowDefinition Height = "0.5*" />

//            < RowDefinition Height = "0.5*" />

//            < RowDefinition Height = "0.5*" />

//            < RowDefinition Height = "0.5*" />

//          </ Grid.RowDefinitions >

//          < Grid.ColumnDefinitions >

//            < ColumnDefinition Width = "*" />

//          </ Grid.ColumnDefinitions >

//          < Image Grid.Row = "0" HorizontalAlignment = "Center" Height = "200" VerticalAlignment = "Bottom" Width = "200" Stretch = "UniformToFill" Source = "{Binding picture ,Converter={StaticResource ImagePathToBitMap}}" ></ Image >

//          < TextBlock FontFamily = "Bauhaus 93" FontSize = "20" Name = "name_product" Grid.Row = "1" Text = "{Binding Path=Name}" Foreground = "#FFAE136A" />

//          < TextBlock FontFamily = "Bauhaus 93" FontSize = "20" Grid.Row = "2" Text = "{Binding Path=Price}" Foreground = "#FFAE136A" />

//          < Button x: Name = "show" Visibility = "{Binding InStock, Mode=TwoWay, NotifyOnValidationError=false, ValidatesOnExceptions=true, Converter={StaticResource ConvertBoolean}}" FontSize = "20" Tag = "{Binding Path=ID}" Grid.Row = "3" Content = "🍩" FontFamily = "Bauhaus 93" Foreground = "#FFAE136A" Background = "WhiteSmoke" Height = "30" Width = "200" Click = "show_Click" />

//          < Button x: Name = "add_cart" Visibility = "{Binding InStock, Mode=TwoWay, NotifyOnValidationError=false, ValidatesOnExceptions=true, Converter={StaticResource ConvertBoolean}}" FontSize = "22" Tag = "{Binding Path=ID}" Height = "30" Width = "200" Grid.Row = "4" Content = "🛍" FontFamily = "Bauhaus 93" Foreground = "#FFAE136A" Background = "WhiteSmoke" Click = "Button_Click" />


//        </ Grid >

//      </ Border >

//    </ DataTemplate >
//< Style x: Key = "StyleOfCategory" TargetType = "TextBlock" >

//      < Setter Property = "Foreground" Value = "Black" />

//      < Setter Property = "FontWeight" Value = "Bold" />

//      < Setter Property = "FontFamily" Value = "Bauhaus 93" />

//      < Setter Property = "FontSize" Value = "25" />

//      < Setter Property = "HorizontalAlignment" Value = "Center" />

//      < Setter Property = "VerticalAlignment" Value = "Center" />

//      < Setter Property = "Height" Value = "NaN" />

//      < Setter Property = "Width" Value = "NaN" />

//      < EventSetter Event = "MouseLeftButtonDown" Handler = "TextBlock_MouseLeftButtonDown" />

//      < Style.Triggers >

//        < Trigger Property = "IsMouseOver" Value = "True" >

//          < Setter Property = "Foreground" Value = "#FFAE136A" />

//          < Setter Property = "TextDecorations" Value = "Underline" />

//        </ Trigger >

//        < EventTrigger RoutedEvent = "MouseEnter" >

//          < EventTrigger.Actions >

//            < BeginStoryboard >

//              < Storyboard >

//                < DoubleAnimation Duration = "0:0:0.300" Storyboard.TargetProperty = "FontSize" To = "28" />

//              </ Storyboard >

//            </ BeginStoryboard >

//          </ EventTrigger.Actions >

//        </ EventTrigger >

//        < EventTrigger RoutedEvent = "MouseLeave" >

//          < EventTrigger.Actions >

//            < BeginStoryboard >

//              < Storyboard >

//                < DoubleAnimation Duration = "0:0:0.800" Storyboard.TargetProperty = "FontSize" To = "26" />

//              </ Storyboard >

//            </ BeginStoryboard >

//          </ EventTrigger.Actions >

//        </ EventTrigger >

//      </ Style.Triggers >

//    </ Style >
//</ Window.Resources >

//< Grid Name = "MainOpenGrid" AutomationProperties.Name = "mainopennig" Background = "WhiteSmoke" >

//  < Grid.ColumnDefinitions >

//    < ColumnDefinition Width = "31*" />

//    < ColumnDefinition Width = "19*" />

//  </ Grid.ColumnDefinitions >

//  < Grid.RowDefinitions >

//    < RowDefinition Height = "100*" />

//    < RowDefinition Height = "80*" />

//    < RowDefinition Height = "400*" />

//  </ Grid.RowDefinitions >

//    < StackPanel Grid.Row = "1" Grid.ColumnSpan = "2" >

//      < !--button to category -->
//            <StackPanel Orientation="Horizontal">
//                <Label Content="          "/>
//                <TextBlock  Text="miniDonuts"  Margin="0,0,0,0" Style="{StaticResource StyleOfCategory}"/>
//                <Label Content="          "/>
//                <TextBlock Text="belgianWaffles" Style="{StaticResource StyleOfCategory}"/>
//                <Label Content="          "/>
//                <TextBlock  Text="general" Style="{StaticResource StyleOfCategory}"/>
//                <Label Content="          "/>
//                <TextBlock  Text="bigDonuts" Style="{StaticResource StyleOfCategory}"/>
//                <Label Content="          "/>
//                <TextBlock  Text="specials" RenderTransformOrigin="6.514,0.29" Style="{StaticResource StyleOfCategory}"/>
//                <Label Content="          "/>
//                <TextBlock  Text="cupcakes" RenderTransformOrigin="8.346,-0.172" Style="{StaticResource StyleOfCategory}"/>
//                <Label Content="          "/>
//                <TextBlock  Text="desserts" Style="{StaticResource StyleOfCategory}"/>
//                <Label Content="          "/>
//                <TextBlock  Text="Popular"  Margin="0,0,0,0" Style="{StaticResource StyleOfCategory}"/>
//            </StackPanel>
//        </StackPanel>
//<Label x:Name = "userName" Foreground = "#FFAE136A" Content = "" HorizontalAlignment = "Left" Margin = "450,30,0,0" VerticalAlignment = "Top" Grid.Row = "0" />

//    < Button x: Name = "cart_btn" Background = "WhiteSmoke" Content = "🛒" FontSize = "35" Foreground = "Gray" HorizontalAlignment = "Left" Margin = "260,3,0,0" VerticalAlignment = "Top" Height = "70" Grid.Row = "0" Width = "70" Click = "cart_btn_Click" >

//    </ Button >

//    < Button x: Name = "search_btn" Background = "WhiteSmoke" Content = "🔎" FontSize = "35" Foreground = "Gray" HorizontalAlignment = "Left" Margin = "150,3,0,0" VerticalAlignment = "Top" Height = "70" Width = "70" Click = "search_btn_Click" >

//    </ Button >

//    < TextBox x: Name = "search_txt" Text = "{Binding searchText,Mode=TwoWay}" Grid.Row = "0" HorizontalAlignment = "Left" Margin = "36,20,0,0"
//VerticalAlignment = "Top" Height = "30" Width = "112" RenderTransformOrigin = "0.266,-1.095" />

//    < Label x: Name = "time" FontFamily = "Bauhaus 93" FontSize = "25" Foreground = "#FFAE136A" Content = "{Binding hourMain,Converter={StaticResource convertTime }}" Grid.Row = "0" HorizontalAlignment = "Left" Margin = "600,19,0,0" VerticalAlignment = "Top" Width = "400" />

//    < ListBox x: Name = "ProductsItemsListBox"  Grid.Row = "1" d: ItemsSource = "{d:SampleData ItemCount=5}"  ItemsSource = "{Binding products}" ItemTemplate = "{StaticResource OneProductItemTemplate}" Grid.ColumnSpan = "2" Margin = "0,94,0,0" Grid.RowSpan = "2" >

//      < ListBox.ItemsPanel >

//        < ItemsPanelTemplate >

//          < UniformGrid Columns = "5" />

//        </ ItemsPanelTemplate >

//      </ ListBox.ItemsPanel >

//    </ ListBox >


//    < Label x: Name = "username" FontFamily = "Bauhaus 93" FontSize = "25" Foreground = "#FFAE136A" Content = "{Binding NameUser}" HorizontalAlignment = "Left" Margin = "650,70,0,0" VerticalAlignment = "Top" Width = "165" RenderTransformOrigin = "-1.066,-0.527" Height = "165" Grid.ColumnSpan = "2" Grid.RowSpan = "3" />

//    < Menu VerticalAlignment = "Top" Background = "Transparent" Margin = "350,3,-373,0" Height = "101" RenderTransformOrigin = "0.585,2.04" Grid.ColumnSpan = "2" >

//      < MenuItem Header = "👦" FontSize = "35" Foreground = "Gray" Height = "70" Width = "70" FlowDirection = "RightToLeft" HorizontalContentAlignment = "Center" HorizontalAlignment = "Left" RenderTransformOrigin = "1.857,0.66" >

//        < MenuItem x: Name = "menuLogIn" FontFamily = "Consolas" Foreground = "#FFAE136A" FontSize = "18" BorderBrush = "Black" Background = "LightPink" Icon = "💌" Header = "Log in" FontWeight = "Bold" Click = "menuLogIn_Click" />

//        < MenuItem x: Name = "menuSignIn" FontFamily = "Consolas" Foreground = "#FFAE136A" FontSize = "18" BorderBrush = "Black" Background = "LightPink" Icon = "😃" Header = "Sign in" FontWeight = "Bold" Click = "menuSignIn_Click" />

//        < MenuItem x: Name = "menuSignOut" FontFamily = "Consolas" Foreground = "#FFAE136A" FontSize = "18" BorderBrush = "Black" Background = "LightPink" Icon = "😡" Header = "Sign out" FontWeight = "Bold" Click = "menuSignOut_Click" />

//        < MenuItem x: Name = "menuTrack" FontFamily = "Consolas" Foreground = "#FFAE136A" FontSize = "18" BorderBrush = "Black" Background = "LightPink" Icon = "🚛" Header = "track your order" FontWeight = "Bold" Click = "menuTrack_Click" />

//        < Separator />

//      </ MenuItem >

//    </ Menu >

//    < Menu VerticalAlignment = "Top" Background = "Transparent" Margin = "344,3,-373,0" Height = "101" RenderTransformOrigin = "0.585,2.04" Grid.Column = "1" >

//      < MenuItem Header = "💡" FontSize = "35" Foreground = "Gray" Height = "70" Width = "70" FlowDirection = "RightToLeft" HorizontalContentAlignment = "Center" HorizontalAlignment = "Left" >

//        < MenuItem x: Name = "menuOdot" FontFamily = "Consolas" Foreground = "#FFAE136A" FontSize = "16" BorderBrush = "Black" Background = "LightPink" Icon = "✌" Header = "About" FontWeight = "Bold" Click = "menuOdot_Click" />

//        < MenuItem x: Name = "menuContent" FontFamily = "Consolas" Foreground = "#FFAE136A" FontSize = "16" BorderBrush = "Black" Background = "LightPink" Icon = "☎" Header = "Contact Us" FontWeight = "Bold" Click = "menuContent_Click" />

//        < MenuItem x: Name = "menuBack" FontFamily = "Consolas" Foreground = "#FFAE136A" FontSize = "16" BorderBrush = "Black" Background = "LightPink" Icon = "↩" FontWeight = "Bold" Click = "menuBack_Click" />

//        < Separator />

//      </ MenuItem >

//    </ Menu >

//    < Label FontFamily = "Bauhaus 93" FontSize = "20" Foreground = "#FFAE136A" x: Name = "Amount_lbl" Content = "{Binding Amount}" HorizontalAlignment = "Left" Height = "30" Margin = "270,42,0,0" VerticalAlignment = "Top" Width = "30" RenderTransformOrigin = "-7.107,1.736" />

//  </ Grid >



//</ Window >
