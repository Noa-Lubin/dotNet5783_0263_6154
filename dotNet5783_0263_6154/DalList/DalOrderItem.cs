using DO;
namespace Dal;
public class DalOrderItem
{
    //this function add new orderItem to array

    public static int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.config.NextOrderItem;
        if (DataSource.orderItemArr.Length - 1 == DataSource.config.NumOfOrderItems)
        {
            throw new Exception("there is no place in this array");
        }
        else
        {
            DataSource.orderItemArr[DataSource.config.NumOfOrderItems++] = orderItem;
        }
        return orderItem.ID;
    }
    //delete this orderItem from array
    public static void delete(int id)
    {
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (id == DataSource.orderItemArr[i].ID)
            {
                //delete orderItem
                DataSource.orderItemArr[i] = DataSource.orderItemArr[DataSource.config.NumOfOrderItems];
                //update the stock of orderItem
                DataSource.config.NumOfOrderItems--;
                return;
            }
        }
        //if this orderItem does not exist in array
        throw new Exception("this orderItem does not exist");
    }
    //Returns a orderItem by ID number
    public static OrderItem GetOrderItem(int id)
    {
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (id == DataSource.orderItemArr[i].ID)
            {

                return DataSource.orderItemArr[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("this id of orderItem does not exist");
    }
    //return a list/array of all the orderItems that in stock
    public static OrderItem[] GetOrderItems()

    {
        OrderItem[] newOrderItems = new OrderItem[DataSource.config.NumOfOrderItems];
        for (int i = 0; i < DataSource.config.NumOfOrders; i++)
        {
            newOrderItems[i] = DataSource.orderItemArr[i];
        }
        return newOrderItems;
    }
    //Updates the orderItem with new data 
    public static int Update(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (orderItem.ID == DataSource.orderItemArr[i].ID)
            {
                DataSource.orderItemArr[i] = orderItem;
            }
        }
        //if this orderItem does not exist in array
        throw new Exception("this orderItem does not exist");

    }
    //return object of orderItem by idProuct and idOrder
    public static OrderItem GetItemById(int idProuct, int idOrder)
    {
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (idProuct == DataSource.orderItemArr[i].ProductID && idOrder == DataSource.orderItemArr[i].OrderID)
            {
                return DataSource.orderItemArr[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("this id of orderItem does not exist");
    }

    //return array of products by idOrder
    public static OrderItem[] AllProductsOfOrder(int idOrder)
    {
        int index = 0;
        OrderItem[] productsArr = new OrderItem[4]; 
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (idOrder == DataSource.orderItemArr[i].OrderID)
            {
                productsArr[index++] = DataSource.orderItemArr[i];
            }
        }
        if(productsArr.Length > 0)
            return productsArr;
        //if this id does not exist in array
        throw new Exception("this id of orderItem does not exist");
    }
}