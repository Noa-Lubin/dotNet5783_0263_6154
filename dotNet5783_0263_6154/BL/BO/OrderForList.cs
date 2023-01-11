using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderForList
    {
        //        מזהה(הזמנה)
        //שם קונה
        //מצב הזמנה
        //כמות פריטים
        //מחיר כולל
        public override string ToString() => this.ToStringProperty();//ToString

        /// <summary>
        /// 
        /// </summary>
        public int IdOrder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
       
        /// <summary>
        /// 
        /// </summary>

        public Enums.OrderStatus? status { get; set; }
       
        /// <summary>
        /// כמות פריטים
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double TotalPrice { get; set; }
    }
}
