using DO;
namespace Dal;
using DalApi;

internal class DalOrderItem : IOrderItem
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
                DataSource.orderItemArr[i] = DataSource.orderItemArr[DataSource.config.NumOfOrderItems - 1];
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
        OrderItem[] oItem = DataSource.orderItemArr;
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (id == oItem[i].ID)
            {

                return oItem[i];
            }
        }
        //if this id does not exist in array
        throw new Exception("this id of orderItem does not exist");
    }


    //return a list/array of all the orderItems that in stock
    public static OrderItem[] GetOrderItems()
    {
        OrderItem[] oItems = DataSource.orderItemArr;
        OrderItem[] newOrderItems = new OrderItem[DataSource.config.NumOfOrderItems];
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            newOrderItems[i] = oItems[i];
        }
        return newOrderItems;
    }


    //Updates the orderItem with new data 
    public static void Update(OrderItem orderItem)
    {
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (orderItem.ID == DataSource.orderItemArr[i].ID)
            {
                DataSource.orderItemArr[i] = orderItem;
                return;
            }
        }
        //if this orderItem does not exist in array
        throw new Exception("this orderItem does not exist");

    }
    //return object of orderItem by idProuct and idOrder
    public static OrderItem GetItemByIds(int idProuct, int idOrder)
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
        OrderItem[] orderItems = DataSource.orderItemArr;
        OrderItem[] productsArr = new OrderItem[DataSource.config.NumOfOrderItems];
        for (int i = 0; i < DataSource.config.NumOfOrderItems; i++)
        {
            if (idOrder == orderItems[i].OrderID)
            {
                productsArr[index++] = orderItems[i];
            }
        }
        OrderItem[] newProductsArr = new OrderItem[index];
        for (int i = 0; i < index; i++)
        {
            newProductsArr[i] = productsArr[i];
        }
        if (productsArr.Length > 0)
            return newProductsArr;
        //if this id does not exist in array
        throw new Exception("this id of orderItem does not exist");
    }
}