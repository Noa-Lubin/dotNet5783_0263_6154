using DalApi;
using DO;
namespace Dal;
internal class DalOrder : IOrder
{
    //this function add new order to array

    public int Add(Order order)
    {
        order.ID = DataSource.config._nextOrderItem;
        //insert new order to list
        DataSource.orderList.Add(order);

        return order.ID;
    }
    //delete this order from array
    public void Delete(int id)
    {
        foreach (var item in DataSource.orderList)
        {
            if (item.ID == id)
            {
                DataSource.orderList.Remove(item);
                return;
            }
        }

        //if this order does not exist in array
        throw new Exception("this order does not exist");
    }



    
    /// <summary>
    /// this function return an order by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>order</returns>
    /// <exception cref="Exception"></exception>
    public Order Get(int id)
    {
        //List<Product> product = DataSource.productList;
        foreach (var item in DataSource.orderList)
        {
            if (item.ID == id)
                return item;
        }

        //if this product does not exist in array
        throw new Exception("this order does not exist");

    }


    /// <summary>
    /// this function return array of orders
    /// </summary>
    /// <returns>array of orders</returns>
    public IEnumerable<Order> GetAll()

    {
        //        List<Product> product = DataSource._productList;
        List<Order> newOrders = new List<Order>();
        foreach (var item in DataSource.orderList)
        {
            newOrders.Add(item);
        }
        return newOrders;

    }

    /// <summary>
    /// this function update an order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order order)
    {
        foreach (var item in DataSource.orderList)
        {
            if (order.ID == item.ID)
            {
                DataSource.orderList.Remove(item);
                DataSource.orderList.Add(order);
                return;
            }
        }
        //if this order does not exist in array
        throw new Exception("this order does not exist");

    }

}