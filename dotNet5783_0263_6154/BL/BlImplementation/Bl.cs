using BlApi;
using DO;
namespace BlImplementation
{
    public class Bl : IBl
    {
        public IProduct Product => new Product();

        public IOrder Order => new Order();

        public ICart Cart => new Cart();

        //public IOrderItem OrderItem => new OrderItem();
    }
}
