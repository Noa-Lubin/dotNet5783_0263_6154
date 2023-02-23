namespace Dal;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection;

internal class Order : IOrder
{
    readonly string s_orders = "Order";

    /// <summary>
    /// The function add a new order
    /// </summary>
    /// <param name="entity"></param>
    /// <returns>id of the new order</returns>
    public int Add(DO.Order entity)
    {
        List<DO.Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        entity.ID = Config.NextOrderNumber();
        lstOrd.Add(entity);
        XMLTools.SaveListToXMLSerializer<DO.Order>(lstOrd, s_orders);
        Config.SaveNextOrderNumber(entity.ID + 1);
        return entity.ID;
}


    /// <summary>
    /// The function delete order
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DalIdDoNotExistException"></exception>
    public void Delete(int id)
    {
        List<DO.Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        DO.Order? o = lstOrd.FirstOrDefault(order => order?.ID == id) ?? throw new DalIdDoNotExistException(id, "order");
        int orderIndex = lstOrd.FindIndex(order => order?.ID == id);
        lstOrd.RemoveAt(orderIndex);
        XMLTools.SaveListToXMLSerializer<DO.Order>(lstOrd, s_orders);
    }


    /// <summary>
    /// The function return an order
    /// </summary>
    /// <param name="id"></param>
    /// <returns>order</returns>
    /// <exception cref="DalIdDoNotExistException"></exception>
    public DO.Order Get(int id)
    {
        List<DO.Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return lstOrd.FirstOrDefault(order => order?.ID == id) ?? throw new DalIdDoNotExistException(id, "order");
    }


    /// <summary>
    ///  The function return all the orders
    /// </summary>
    /// <param name="func"></param>
    /// <returns>All the orders</returns>
    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? func = null)
    {
        List<DO.Order?> lstOrder = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (func != null)
            return lstOrder.Where(item => func(item));
        return lstOrder.Select(item => item);
    }


    /// <summary>
    /// The function return an order by predicat
    /// </summary>
    /// <param name="func"></param>
    /// <returns>order by predicat</returns>
    /// <exception cref="NotFound"></exception>
    public DO.Order GetByPredicat(Func<DO.Order?, bool> func)
    {
        List<DO.Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        return lstOrd.Where(item => func(item)).FirstOrDefault() ?? throw new NotFound("No suitable product found");
    }


    /// <summary>
    /// The function update an order
    /// </summary>
    /// <param name="entity">order</param>
    /// <exception cref="NotFound"></exception>
    public void Update(DO.Order entity)
    {
        List<DO.Order?> lstOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        DO.Order? o = lstOrd.FirstOrDefault(order => order?.ID == entity.ID) ?? throw new NotFound(" this Product is not exist");
        int orderIndex = lstOrd.FindIndex(order => order?.ID == entity.ID);
        //lstOrd.RemoveAt(orderIndex);
        //lstOrd.Add(entity);
        lstOrd[orderIndex] = entity;
        XMLTools.SaveListToXMLSerializer<DO.Order>(lstOrd, s_orders);
    }
}

