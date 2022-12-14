using DalApi;
using DO;
namespace Dal;
internal class DalOrder : IOrder
{
    /// <summary>
    /// this function add new order to array
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public int Add(Order order)
    {
        order.ID = DataSource.config._nextOrderItem;
        //insert new order to list
        DataSource.orderList.Add(order);
        return order.ID;
    }

    /// <summary>
    /// delete this order from array
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="Exception"></exception>
    public void Delete(int id)
    {
        Order ord = DataSource.orderList.FirstOrDefault(o => o?.ID == id) ??
        //  if this order does not exist in list
        throw new Exception("This order is not exist");
        DataSource.orderList.Remove(ord);
        //foreach (var item in DataSource.orderList)
        //{
        //    if (item?.ID == id)
        //    {
        //        DataSource.orderList.Remove(item);
        //        return;
        //    }
        //}
        ////if this order does not exist in array
        //throw new Exception("this order does not exist");
    }

    /// <summary>
    /// this function return an order by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>order</returns>
    /// <exception cref="Exception"></exception>
   
    public Order Get(int id)
    {
            return DataSource.orderList.FirstOrDefault(o => o?.ID == id) ??        
        //if this product does not exist in array
        throw new Exception("this order does not exist");
    }

 /// <summary>
 /// 
 /// </summary>
 /// <param name="func"></param>
 /// <returns></returns>
 /// <exception cref="Exception"></exception>
    public Order GetByPredicat(Func<Order?, bool> func)
    {
        return DataSource.orderList.FirstOrDefault(func) ??
    //if this product does not exist in array
    throw new Exception("this order does not exist");
    }


    /// <summary>
    /// this function return list of orders
    /// </summary>
    /// <returns>array of orders</returns>

    public IEnumerable<Order?> GetAll(Func<Order?, bool> func = null)
    {
        return func is null ? DataSource.orderList.Select(o => o) : 
            DataSource.orderList.Where(func);
    }

    /// <summary>
    /// this function update an order
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order order)
    {
        Order ord = DataSource.orderList.FirstOrDefault(o => o?.ID == order.ID) ??
        //  if this order does not exist in lis
        throw new Exception("This order is not exist");
        DataSource.orderList.Remove(ord);
        DataSource.orderList.Add(order);

        //foreach (var item in DataSource.orderList)
        //{
        //    if (order.ID == item?.ID)
        //    {
        //        DataSource.orderList.Remove(item);
        //        DataSource.orderList.Add(order);
        //        return;
        //    }
        //}
        ////if this order does not exist in array
        //throw new Exception("this order does not exist");

    }

    
}