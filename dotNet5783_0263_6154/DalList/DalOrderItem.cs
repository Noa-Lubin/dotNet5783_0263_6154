using DO;
namespace Dal;
using DalApi;

public class DalOrderItem : IOrderItem
{
    //this function add new orderItem to array

    public int Add(OrderItem orderItem)
    {
        orderItem.ID = DataSource.config._nextOrderItem;
        //insert new orderItem to list
        DataSource.orderItemList.Add(orderItem);
        return orderItem.ID;
    }
    //delete this orderItem from array
    public void Delete(int id)
    {
        foreach (var item in DataSource.orderItemList)
        {
            if (item.ID == id)
            {
                DataSource.orderItemList.Remove(item);
                return;
            }
        }


        //if this orderItem does not exist in array
        throw new Exception("this orderItem does not exist");
    }
    //Returns a orderItem by ID number
    public OrderItem Get(int id)
    {
        //List<Product> product = DataSource.productList;
        foreach (var item in DataSource.orderItemList)
        {
            if (item.ID == id)
                return item;
        }

        //if this OrderItem does not exist in array
        throw new Exception("this OrderItem does not exist");

    }


    //return a list/array of all the orderItems that in stock
    public IEnumerable<OrderItem> GetAll()
    {
        //        List<Product> product = DataSource._productList;
        List<OrderItem> newOrderItems = new List<OrderItem>();
        foreach (var item in DataSource.orderItemList)
        {
            newOrderItems.Add(item);
        }
        return newOrderItems;
    }


    //Updates the orderItem with new data 
    public void Update(OrderItem orderItem)
    {
        foreach (var item in DataSource.orderItemList)
        {
            if (orderItem.ID == item.ID)
            {
                DataSource.orderItemList.Remove(item);
                DataSource.orderItemList.Add(orderItem);
                return;
            }
        }
        // if this orderItem does not exist in array
        throw new Exception("this orderItem does not exist");

    }
    //return object of orderItem by idProuct and idOrder
    public OrderItem GetItemByIds(int idProuct, int idOrder)
    {
        foreach (var item in DataSource.orderItemList)
        {
            if (idProuct == item.ProductID && idOrder == item.OrderID)
            {
                return item;
            }
        }
        //if this id does not exist in array
        throw new Exception("this id of orderItem does not exist");
    }

    //return array of products by idOrder
    public IEnumerable<OrderItem> AllProductsOfOrder(int idOrder)
    {
        List<OrderItem> productsInOrder = new List<OrderItem>();
        foreach (var item in DataSource.orderItemList)
        {
            if (idOrder == item.OrderID)
            {
                productsInOrder.Add(item);
            }
        }
        //if (productsInOrder.Count() > 0)
            return productsInOrder;
        //if this id does not exist in array
        //throw new Exception("this id of orderItem does not exist");
    }


}