using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderItem
    {
        public override string ToString() => this.ToStringProperty();//ToString


        //מזהה פריט הזמנה
        //מזהה מוצר
        //שם מוצר
        //מחיר מוצר
        //כמות פריטים של מוצר בסל\הזמנה
        //מחיר כולל של פריט(לפי מחיר מוצר וכמותו בהזמנה\סל)
        /// <summary>
        /// 
        /// </summary>
        public int OrderItemID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int IdProduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? Name { get; set; }
       
        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        ///         מחיר כולל של פריט(לפי מחיר מוצר וכמותו בהזמנה\סל)
        /// </summary>
        public double TotalPrice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AmountInCart { get; set; }

    }
}
