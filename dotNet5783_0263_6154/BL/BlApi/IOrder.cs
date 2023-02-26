using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IOrder
    {
        /// <summary>
        /// This function return IEnumerable of all orders.
        /// </summary>
        /// <param name="func"></param>
        /// <returns>IEnumerable of all orders</returns>
        public IEnumerable<OrderForList> GetAllOrders(Func<DO.Product?, bool> func = null);
        
        
        /// <summary>
        /// This function return a specific order.
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns>order</returns>
        public Order GetOrder(int idOrder);


        /// <summary>
        /// This function update the shipping date
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns>order</returns>
        public Order ShippingUpdate(int idOrder);


        /// <summary>
        /// This function update the delivery date.
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns>order</returns>
        public Order OrderDeliveryUpdate(int idOrder);


        /// <summary>
        /// A function that returns an order tracking instance - date and a verbal description of the order status.
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns>order tracking</returns>
        public OrderTracking OrderOfTracking(int idOrder);
 

        /// <summary>
        /// This function update an order.
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int? GetOldest();
    }
}
