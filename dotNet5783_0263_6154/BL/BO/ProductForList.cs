using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductForList
    {
        //        מזהה(מוצר)
        //שם מוצר
        //מחיר מוצר
        //קטגוריה
        public override string ToString() => this.ToStringProperty();//ToString

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
        /// 
        /// </summary>

        public Enums.Category? Category { get; set; }
       
       
    }
}
