using DalApi;
using DO;
namespace Dal;
internal class DalOrder:IOrder
{
    //this function add new order to array

    public static int Add(Order order)
    {
        order.ID = DataSource.config.NextOrderNumber;
        if (DataSource.orderArr.Length - 1 == DataSource.config.NumOfOrders)//if array is full
        {

            throw new Exception("there is no place in this array");
        }
        else
        {
            //insert new order to array
            DataSource.orderArr[DataSource.config.NumOfOrders++] = order;
        }
        return order.ID;
    }
    //delete this order from array
    public static void delete(int id)
    {
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            if (id == DataSource.orderArr[i].ID)
            {
                //delete order
                DataSource.orderArr[i] = DataSource.orderArr[DataSource.config.NumOfOrders-1];
                //update the stock of order
                DataSource.config.NumOfOrders--;
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
    public static Order GetOrder(int id)
    {
        Order[] o = DataSource.orderArr;
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            if (id == o[i].ID)
            {

                return o[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("this id of order does not exist");
    }


/// <summary>
/// this function return array of orders
/// </summary>
/// <returns>array of orders</returns>
    public static Order[] GetOrders()

    {
        Order[] o = DataSource.orderArr;
        Order[] newOrders = new Order[DataSource.config.NumOfOrders];
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            newOrders[i] = o[i];
        }
        return newOrders;
    }

    /// <summary>
    /// this function update an order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    public static void Update(Order order)
    {
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            if (order.ID == DataSource.orderArr[i].ID)
            {
                DataSource.orderArr[i] = order;
                return;
            }
        }
        //if this order does not exist in array
        throw new Exception("this order does not exist");

    }
}