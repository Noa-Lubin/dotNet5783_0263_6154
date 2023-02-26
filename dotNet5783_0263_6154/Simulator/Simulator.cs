using DO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public class myOrder:EventArgs
    {
        public BO.Order order;
        public int second;

        public myOrder(BO.Order o, int s)
        {
            order = o;
            second = s;
        }
    }

    public static class Simulator
    {
        private static volatile bool activate = false;
        private static BlApi.IBl _myBl = BlApi.Factory.Get();
        private static EventHandler<myOrder> myStartEvent;
        private static EventHandler myEndEvent;
        public static void ReportStart(EventHandler<myOrder> e)
        {
            myStartEvent += e;
        }
        public static void NotReportStart(EventHandler<myOrder> e)
        {
            myStartEvent -= e;
        }
        public static void ReportEnd(EventHandler e)
        {
            myEndEvent += e;
        }
        public static void NotReportEnd(EventHandler e)
        {
            myEndEvent -= e;
        }
        public static void Deactive() => activate = false;
        public static void Activate()
        {
            var rand = new Random();

            new Thread(() =>
            {
                activate = true;
                while (activate)
                {
                    if (_myBl.Order.GetOldest() != null)
                    {
                        int idOldest = (int)_myBl.Order.GetOldest();
                        if (idOldest != null)
                        {
                            BO.Order order = _myBl.Order.GetOrder(idOldest);
                            int delay = rand.Next(3, 11);
                            DateTime time = DateTime.Now + new TimeSpan(delay * 1000);
                            myOrder o = new myOrder(order, delay);
                            myStartEvent?.Invoke(null, o);
                            Thread.Sleep(delay * 1000);
                            if (order.Status == BO.Enums.OrderStatus.approved)
                                _myBl.Order.ShippingUpdate(order.ID);
                            else if (order.Status == BO.Enums.OrderStatus.sent)
                                _myBl.Order.OrderDeliveryUpdate(order.ID);
                        }
                    }
                    else
                    {
                        myEndEvent?.Invoke(null, EventArgs.Empty);
                    }
                    Thread.Sleep(1000);
                }
                myEndEvent?.Invoke(null, EventArgs.Empty);
            }).Start();
        }
    }
}
