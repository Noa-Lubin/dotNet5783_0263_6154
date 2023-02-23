using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;

namespace Dal
{
    sealed internal class DalList : IDal
    {
        private DalList() {}//private ctor of class
        public static IDal Instance { get; } = new DalList();
        public IProduct product => new DalProduct();

        //public object Product => throw new NotImplementedException();

        public IOrder order => new DalOrder();

        public IOrderItem orderItem => new DalOrderItem();
    }
}
