using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public override string ToString() => this.ToStringProperty();//ToString

        //        מזהה(הזמנה)
        //מצב הזמנה
        //רשימה של צמדים(תאריך, תיאור התקדמות חבילה)
        /// <summary>
        /// 
        /// </summary>
        public int IdOrder { get; set; }
       
        /// <summary>
        /// רשימה של צמדים(תאריך, תיאור התקדמות חבילה
        /// </summary>

        public IEnumerable<Tuple<DateTime?, string>?>? Tracking { get; set; }

        public Enums.OrderStatus? Status { get; set; }

    }
}
