using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi
{
    public interface IOrderItem
    {
//        מזהה פריט הזמנה
//מזהה מוצר
//שם מוצר
//מחיר מוצר
//כמות פריטים של מוצר בסל\הזמנה
//מחיר כולל של פריט(לפי מחיר מוצר וכמותו בהזמנה\סל)
public int IdOrderItem { get; set; }

        /// <summary>
        /// IdProduct
        /// </summary>
        public int IdProduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NameProduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double PriceProduct { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AmauntItemsInCart { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double TotalPriceProduct { get; set; }

    }
}
