using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed public class DalList : IDal
    {
        public IProduct product => new DalProduct();

        public object Product => throw new NotImplementedException();

        public IOrder order => new DalOrder();

        public IOrderItem orderItem => new DalOrderItem();
    }
}
