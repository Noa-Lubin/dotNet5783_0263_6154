using BlApi;
using BlImplementation;
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
    /// Interaction logic for ProductForListWIndow.xaml
    /// </summary>
    public partial class ProductForListWIndow : Window
    {
        private IBl _myBl = new BlImplementation.Bl();
        private IEnumerable<BO.ProductForList> _productsForList;
       

        public ProductForListWIndow(IBl bl)
        {
            InitializeComponent();
           _myBl = bl;
           
            _productsForList = _myBl.Product.GetAllProducts();
            ProductsListview.ItemsSource = _productsForList;
        }
    }
}
