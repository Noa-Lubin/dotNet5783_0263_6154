using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public override string ToString() => this.ToStringProperty();//ToString
        //        מזהה(מוצר)
        //שם מוצר
        //מחיר מוצר
        //קטגוריה
        //זמינות(האם במלאי)
        //כמות בסל הקניות של הקונה

        /// <summary>
        /// 
        /// </summary>
        public int IdProduct { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        
        public Enums.Category Category { get; set; }
        /// <summary>
        /// זמינות(האם במלאי)
        /// </summary>
        public bool InStock { get; set; }
        /// <summary>
        /// כמות בסל הקניות של הקונה
        /// </summary>
        public int  Amount { get; set; }
    }

}
