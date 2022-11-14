using DO;
namespace Dal;
public  class DalOrder
{ //this function add new order to array

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
                DataSource.orderArr[i] = DataSource.orderArr[DataSource.config.NumOfOrders];
                //update the stock of order
                DataSource.config.NumOfOrders--;
                return;
            }
        }
        //if this order does not exist in array
        throw new Exception("this order does not exist");
    }
    //Returns a order by ID number
    public static Order GetOrder(int id)
    {
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            if (id == DataSource.orderArr[i].ID)
            {

                return DataSource.orderArr[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("this id of order does not exist");
    }
    //return a list/array of all the orders that in stock
    public static Order[] GetOrders()

    {
        Order[] newOrders = new Order[DataSource.config.NumOfOrders];
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            newOrders[i] = DataSource.orderArr[i];
        }
        return newOrders;
    }
    //Updates the order with new data 
    public static int Update(Order order)
    {
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            if (order.ID == DataSource.orderArr[i].ID)
            {
                DataSource.orderArr[i] = order;
            }
        }
        //if this order does not exist in array
        throw new Exception("this order does not exist");

    }
}