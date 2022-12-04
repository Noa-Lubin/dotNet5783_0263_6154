using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public override string ToString() => this.ToStringProperty();//ToString
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
        /// 
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<OrderItem> Items { get; set; }
    }
}
