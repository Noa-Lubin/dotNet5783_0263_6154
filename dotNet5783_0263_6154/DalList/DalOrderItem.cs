using DO;
namespace Dal;
using DalApi;
using System;

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
            if (item?.ID == id)
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
        List<OrderItem?> orderItems = DataSource.orderItemList;
        return DataSource.orderItemList.FirstOrDefault(oI => oI?.ID == id) ??

        //if this OrderItem does not exist in array
        throw new Exception("this OrderItem does not exist");

    }


    //return a list/array of all the orderItems that in stock

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool> func = null)
    {
        return func is null ? DataSource.orderItemList.Select(oI => oI) :
            DataSource.orderItemList.Where(func);
    }

    //Updates the orderItem with new data 
    public void Update(OrderItem orderItem)
    {
        foreach (var item in DataSource.orderItemList)
        {
            if (orderItem.ID == item?.ID)
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
        List<OrderItem?> orderItems = DataSource.orderItemList;
        return DataSource.orderItemList.FirstOrDefault(oI => oI?.ProductID == idProuct && oI?.OrderID == idOrder) ??
        //if this id does not exist in array
        throw new Exception("this id of orderItem does not exist");
    }

    //return array of products by idOrder

    public IEnumerable<OrderItem?> AllProductsOfOrder(int idOrder)
    {
        List<OrderItem?> orderItems = DataSource.orderItemList;
        return DataSource.orderItemList.Where(oI => oI?.ID == idOrder) ??

            throw new Exception("This order is empty");
    }

    
}