using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using DO;
namespace Dal;


    sealed internal class DalXml : IDal
    {
        private DalXml() { }//private ctor of class
        public static IDal Instance { get; } = new DalXml();
        public IProduct product => new Product();
        public IOrder order => new Order();
        public IOrderItem orderItem => new OrderItem();
    }


