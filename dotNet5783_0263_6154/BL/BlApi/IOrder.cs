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
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrderForList> GetAllOrders();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public Order GetOrder(int idOrder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public Order ShippingUpdate(int idOrder);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public Order OrderDeliveryUpdate(int idOrder);
       /// <summary>
       /// 
       /// </summary>
       /// <param name="idOrder"></param>
       /// <returns></returns>
        public OrderTracking OrderOfTracking(int idOrder);
 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        public void UpdateOrder(Order order);
    }
}
