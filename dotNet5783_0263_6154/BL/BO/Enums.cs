using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Enums
    {
        public enum Category { miniDonuts, general, belgianWaffles, bigDonuts, desserts, cupcakes, specials };
        public enum OrderStatus { approved, sent, provided };
        public enum Choice { Exit, Product, Order, Cart };
        public enum ChoiceOrder { GetAllOrders, GetOrder, OrderDeliveryUpdate, OrderOfTracking, ShippingUpdate, UpdateOrder };
        public enum ChoiceCart { AddProductToCart, MakeOrder, UpdateAmountOfProduct};
        public enum ChoiceProduct { AddProduct, DeleteProduct, GetAllProducts, GetProduct, GetProductForList,UpdateProduct };

    }
}
