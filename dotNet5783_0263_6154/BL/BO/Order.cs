using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Order
    {
        public override string ToString() => this.ToStringProperty();//ToString

        /// <summary>
        /// ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// CustomerName
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// CustomerEmail
        /// </summary>
        public string CustomerEmail { get; set; }

        /// <summary>
        /// CustomerAdress
        /// </summary>
        public string CustomerAdress { get; set; }

        /// <summary>
        /// OrderDate
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// ShipDate
        /// </summary>
        public DateTime ShipDate { get; set; }

        /// <summary>
        /// DeliveryrDate
        /// </summary>
        public DateTime DeliveryrDate { get; set; }

        /// <summary>
        /// status
        /// </summary>
        public Enums.OrderStatus Status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double TotalPrice     { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem> Items { get; set; }
    }
}
